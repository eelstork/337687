using System;
using Activ.Lang; using Activ.Lang.HSM;
using static Activ.Lang.HSM.Transition<char>.Op;
using static BasicXML.ControlCharacters;

namespace BasicXML.ViaHSM{
public partial class XMLValidator{  // The control graph

    Transition<char> Read(char c){
        switch (c){
            case LeftAngleBracket:
                return new Transition<char>(
                    model.BeginTag,
                    Transition<char>.Op.Enter,
                    ReadFirstInTag);
            case RightAngleBracket:
                return Fail(c);
            default:
                return new (Cont);
        }
    }

    Transition<char> ReadFirstInTag(char c){
        switch(c){
            case LeftAngleBracket:
                return Fail(c);
            case RightAngleBracket:
                return Fail(c);
            case Slash:
                return new (model.AsClosingTag, Chain, ReadTag);
            default:
                return new (model.AsOpeningTag, Chain, ReadTag);
        }
    }

    Transition<char> ReadTag(char c){
        switch (c){
            case LeftAngleBracket:
                return Fail(c);
            case RightAngleBracket:
                return new (model.EndTag, Exit);
            case Slash:
                return Fail(c);
            case DoubleQuote:
                return new (model.ExtendTag, Enter, ReadStringLiteral);
            default:
                return new (model.ExtendTag);
        }
    }

    Transition<char> ReadStringLiteral(char c){
        switch (c){
            case DoubleQuote:
                return new(model.ExtendTag, Exit);
            default:
                return new(model.ExtendTag);
        }
    }

    void Log(object arg) => Console.WriteLine(arg);

}}
