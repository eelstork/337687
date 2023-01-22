using Activ.Lang.Multiparser;

namespace BasicXML.ViaMultiparser{
public class XMLValidator : ValidationTree{

    override protected Validator Init() => new DocumentValidator();

    public bool DetermineXML(string arg) => Validate(arg);

}}
