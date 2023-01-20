
namespace BasicXML
{
    public partial class XMLValidator
    {

        public delegate State State(char c);

        State state;
        ValidationModel model;

        public XMLValidator()
        {
            model = new();
            state = Read;
        }

        public bool DetermineXML(string xml)
        {
            try
            {
                return Validate(xml);
            }
            catch (ValidationException)
            {
                return false;
            }
        }

        bool Validate(string arg)
        {
            foreach (var c in arg)
                state = state(c);
            return model.isComplete;
        }

    }
}
