using System.Text;
using Activ.Lang.Multiparser;
using static BasicXML.ControlCharacters;

namespace BasicXML.ViaMultiparser{
internal class StringLiteralReader : Validator{

    ValidationModel model;

    public StringLiteralReader(ValidationModel model){
        this.model = model;
    }

    bool Validator.Enter(char c){
        if(c == DoubleQuote){
            model.ExtendTag(c);
            return true;
        }else{
            return false;
        }
    }

    Validator Validator.Validate(char c){
        model.ExtendTag(c);
        if(c == DoubleQuote){
            return null;
        }else{
            return this;
        }
    }

}}
