namespace Activ.Lang.Multiparser{
public interface Validator{

    bool Enter(char c);

    Validator Validate(char c);

}}
