using System.Text;
using Activ.Lang; using Activ.Lang.Multiparser;
using static BasicXML.ControlCharacters;

namespace BasicXML.ViaMultiparser{
internal class TagReader : Validator{

    ValidationModel model;
    Validator stringReader;

    public TagReader(ValidationModel model){
        this.model = model;
        stringReader = new StringLiteralReader(model);
    }

    bool Validator.Enter(char c){
        if(c == LeftAngleBracket){
            model.BeginTag();
            return true;
        }
        else return false;
    }

    Validator Validator.Validate(char c){
        if(stringReader.Enter(c)){
            return stringReader;
        }
        switch(c){
            case RightAngleBracket:
                if(!model.isClosingTag.HasValue){
                    throw new ValidationException("Empty tag is not allowed");
                }
                model.EndTag();
                return null;
            case Slash:
                if(model.isClosingTag.HasValue){
                    throw new ValidationException($"Unexpected {c}");
                }else{
                    model.QualifyTag(isClosing: true);
                }
                return this;
            default:
                if(!model.isClosingTag.HasValue)
                    model.QualifyTag(isClosing: false);
                model.ExtendTag(c);
                return this;
        }
    }

}}
