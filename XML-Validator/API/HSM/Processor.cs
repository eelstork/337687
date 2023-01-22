using System; using System.Collections.Generic;
using Ex = System.Exception;

namespace Activ.Lang.HSM{
public partial class Processor<T>{

    internal Stack<State<T>> stack = new ();
    protected Formatter<T> format = new Formatter<T>();
    public Action<object> logger;

    public bool Parse(IEnumerable<T> arg){
        try{
            foreach (var c in arg) Eval(c);
            return true;
        }catch (ValidationException e){
            Console.WriteLine(e);
            return false;
        }
    }

    protected void Init(State<T> arg) => stack.Push(arg);

    protected Transition<T> Fail(T c)
    => throw new ValidationException(format.Unexpected(c, this));

    protected virtual void Eval(T element){
        var transition = state(element);
        logger?.Invoke(format[this, transition]);
        try{
            transition.action?.Invoke(element);
        }catch(ValidationException){
            throw;
        }catch(Ex e){
            throw new ValidationError(
                format.ModelUpdateError(element, this, transition), e
            );
        }
        switch(transition.operation){
            case Transition<T>.Op.Cont:
                // NOTE: re-iterate current state
                break;
            case Transition<T>.Op.Enter:
                stack.Push(transition.next); break;
            case Transition<T>.Op.Exit:
                stack.Pop(); break;
            case Transition<T>.Op.Chain:
                stack.Pop(); stack.Push(transition.next); break;
        }
    }

    State<T> state => stack.Peek();

}}
