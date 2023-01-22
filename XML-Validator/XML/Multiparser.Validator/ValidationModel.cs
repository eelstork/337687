using System.Collections.Generic; using System.Text;
using Activ.Lang;

namespace BasicXML.ViaMultiparser
{
    internal class ValidationModel
    {

        Stack<string> path = new Stack<string>();
        StringBuilder currentTag;
        public bool? isClosingTag { get; private set; }

        public bool isComplete
        => path.Count == 0 && currentTag == null;

        public void BeginTag()
        => currentTag = new StringBuilder();

        public void EndTag()
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

        public void ExtendTag(char c) => currentTag.Append(c);

        public void QualifyTag(bool isClosing) => isClosingTag = isClosing;

        internal StringBuilder _currentTag => currentTag;
        internal bool? _isClosingTag => isClosingTag;

    }
}
