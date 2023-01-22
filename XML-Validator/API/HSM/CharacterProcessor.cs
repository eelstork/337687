namespace Activ.Lang.HSM{
public class CharacterProcessor : Processor<char>, DocPositionTracker{

    Position _position = new ();

    public Position position => _position;

    public CharacterProcessor(){
        format = new CharFormatter();
    }

    override protected void Eval(char character){
        position.Update(character);
        base.Eval(character);
    }

}}
