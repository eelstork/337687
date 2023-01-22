using System.Collections.Generic;
using Activ.Lang; using Activ.Lang.Multiparser;

namespace BasicXML.ViaMultiparser{
internal class DocumentValidator : Validator, Completeness{

    ValidationModel model;
    Validator tagReader;

    public DocumentValidator(){
        model = new ValidationModel();
        tagReader = new TagReader(model);
    }

    bool Validator.Enter(char c) => true;

    Validator Validator.Validate(char c){
        if(tagReader.Enter(c)){
            return tagReader;
        }else{
            return this;
        }
    }

    bool Completeness.isComplete => model.isComplete;

    void Log(object arg) => System.Console.WriteLine(arg);

}}
