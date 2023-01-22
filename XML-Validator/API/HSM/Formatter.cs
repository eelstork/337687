using System.Collections.Generic;
using System.Linq;

namespace Activ.Lang.HSM{
public class Formatter<T>{

    public virtual string this[Processor<T> arg, Transition<T> tr]{
        get{
            var path = FormatPath(arg.stack);
            var action = tr.action?.Method.Name;
            var op = tr.operation.ToString().ToLower();
            return $"{path} {action} → {op}";
        }
    }

    public string FormatPath(Stack<State<T>> stack){
        var path = (from e in stack select e.Method.Name).Reverse();
        return $"( {string.Join("・", path)} )";
    }

    public virtual string Unexpected(T element, Processor<T> arg)
    => $"Unexpected '{element}'";

    public virtual string ModelUpdateError(
        T c, Processor<T> arg, Transition<T> transition
    ){
        var path = FormatPath(arg.stack);
        var action = transition.action?.Method.Name;
        return $"Model update error in {path} {action}";
    }

}}
