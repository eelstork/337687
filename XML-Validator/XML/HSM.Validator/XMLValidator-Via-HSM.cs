using Activ.Lang; using Activ.Lang.HSM;

namespace BasicXML.ViaHSM{
public partial class XMLValidator : CharacterProcessor{

    ValidationModel model = new ();

    public XMLValidator() => Init(Read);

    public bool DetermineXML(string arg)
    => Parse(arg) && model.isComplete;

}}
