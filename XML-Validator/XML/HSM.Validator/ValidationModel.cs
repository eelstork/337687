using System.Collections.Generic;
using System.Text;
using BasicXML;
using Activ.Lang;

namespace BasicXML.ViaHSM{
internal class ValidationModel{

    Stack<string> path = new Stack<string>();
    StringBuilder currentTag;
    bool? isClosingTag;

    public bool isComplete
    => path.Count == 0 && currentTag == null;

    public void BeginTag(char ignored)
    => currentTag = new StringBuilder();

    public void EndTag(char ignored)
    {
        if (!isClosingTag.Value)
        {
            path.Push(currentTag.ToString());
        }
        else
        {
            var expected = path.Peek();
            var found = currentTag.ToString();
            if (found != expected)
            {
                throw new ValidationException(
                    $"Expected </{expected}>, found </{found}>"
                );
            }
            else
            {
                path.Pop();
            }
        }
        currentTag = null;
        isClosingTag = null;
    }

    public void ExtendTag(char c)
    => currentTag.Append(c);

    public void AsClosingTag(char ignored)
    => isClosingTag = true;

    public void AsOpeningTag(char c){
        currentTag.Append(c);
        isClosingTag = false;
    }

    internal StringBuilder _currentTag => currentTag;
    internal bool? _isClosingTag => isClosingTag;

}}
