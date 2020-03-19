namespace PlexDL.Common
{
    public class StringVariable
    {
        public string VariableName { get; set; } = null;
        public object VariableValue { get; set; } = null;

        public StringVariable(string varName, object varValue)
        {
            VariableValue = varValue;
            VariableName = varName;
        }
    }
}