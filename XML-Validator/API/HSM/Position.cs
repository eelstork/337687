// Tracks the line number, character offset and line content
public class Position{

    char previousCharacter = '0';
    const char LineFeed = '\n', CarriageReturn = '\r';

    public int lineNumber{ get; private set; }
    public int characterOffset{ get; private set; }
    string lineSoFar = null;

    public Position(){
        lineNumber = 1;
        characterOffset = 1;
    }

    public void Update(char c){
        switch(c){
            case LineFeed:
                if(previousCharacter == CarriageReturn){
                    // Do nothing; already incremented
                }else{
                    lineNumber ++; characterOffset = 1;
                    lineSoFar = null;
                }
                break;
            case CarriageReturn:
                lineNumber ++; characterOffset = 1;
                lineSoFar = null;
                break;
            default:
                characterOffset++;
                lineSoFar += c;
                break;
        }
        previousCharacter = c;
    }

    public string Format(int charCount)
    => $"[{lineNumber}:{characterOffset}] {lineSoFar}...".PadRight(charCount);

    override public string ToString()
    => $"[{lineNumber}:{characterOffset}] {lineSoFar}...";

}
