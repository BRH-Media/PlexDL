namespace PlexDL.Common
{
    public class StringVariable
    {
        public string VariableName { get; set; }
        public object VariableValue { get; set; }

        public StringVariable(string varName, object varValue)
        {
            VariableValue = varValue;
            VariableName = varName;
        }
    }
}