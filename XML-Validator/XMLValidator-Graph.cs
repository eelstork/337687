using System;
using static BasicXML.ControlCharacters;

namespace BasicXML
{
    public partial class XMLValidator
    {  // Graph

        State Read(char c)
        {
            switch (c)
            {
                case LeftAngleBracket:
                    model.BeginTag();
                    return ReadFirstInTag;
                case RightAngleBracket:
                    return Fail(c);
                default:
                    return Read;
            }
        }

        State ReadFirstInTag(char c)
        {
            switch (c)
            {
                case LeftAngleBracket:
                    return Fail(c);
                case RightAngleBracket:
                    return Fail(c);
                case Slash:
                    model.QualifyTag(isClosing: true);
                    return ReadTag;
                default:
                    model.QualifyTag(isClosing: false);
                    model.ExtendTag(c);
                    return ReadTag;
            }
        }

        State ReadTag(char c)
        {
            switch (c)
            {
                case LeftAngleBracket:
                    return Fail(c);
                case RightAngleBracket:
                    model.EndTag();
                    return Read;
                case Slash:
                    return Fail(c);
                case DoubleQuote:
                    model.ExtendTag(c);
                    return ReadStringLiteral;
                default:
                    model.ExtendTag(c);
                    return ReadTag;
            }
        }

        State ReadStringLiteral(char c)
        {
            model.ExtendTag(c);
            return c == DoubleQuote ? ReadTag : ReadStringLiteral;
        }

        State Fail(char c)
        => throw new ValidationException($"Unexpected '{c}'");

    }
}
