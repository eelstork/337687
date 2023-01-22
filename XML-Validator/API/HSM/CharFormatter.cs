using System.Collections.Generic;
using System.Linq;

namespace Activ.Lang.HSM{
public class CharFormatter : Formatter<char>{

    override public string this[
        Processor<char> arg, Transition<char> transition]
    { get{
        var doc = arg as DocPositionTracker;
        var pos = doc.position.ToString();
        var path = FormatPath(arg.stack);
        var action = transition.action?.Method.Name;
        var op = transition.operation.ToString().ToLower();
        return $"{pos}\n    {path} {action} → {op}";
    }}

    override public string Unexpected(char c, Processor<char> arg){
        var doc = arg as DocPositionTracker;
        var ln = doc.position.lineNumber;
        var offset = doc.position.characterOffset;
        return $"Unexpected '{c}' at line {ln}, char {offset}";
    }

    override public string ModelUpdateError(
        char c, Processor<char> arg, Transition<char> transition
    ){
        var doc = arg as DocPositionTracker;
        var pos = doc.position;
        var path = FormatPath(arg.stack);
        var action = transition.action?.Method.Name;
        return $"{pos} Model update error in {path} {action}";
    }

}}
