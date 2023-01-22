using System.Collections.Generic;

namespace Activ.Lang.Multiparser{
public abstract class ValidationTree{

    Stack<Validator> stack = new Stack<Validator>();

    public ValidationTree() => stack.Push(Init());

    public bool Validate(string arg){
        try{
            foreach(var c in arg) Validate(c);
            return ((Completeness)state).isComplete;
        } catch(ValidationException){ return false; }
    }

    protected abstract Validator Init();

    void Validate(char c){
        var previous = state;
        var next = state.Validate(c);
        if(next == previous) return;
        if(next == null){
            stack.Pop();
        }else{
            stack.Push(next);
        }
    }

    Validator state => stack.Peek();

}}
