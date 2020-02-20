using PlexDL.Common.Caching;
using PlexDL.Common.Structures;
using PlexDL.UI;
using System.Collections.Generic;

namespace PlexDL.Common
{
    public class SVarController
    {
        public List<StringVariable> Variables { get; set; } = new List<StringVariable>();
        public string Input { get; set; } = "";

        public string YieldString()
        {
            string result = Input;
            foreach (StringVariable v in Variables)
            {
                if ((v.VariableName != null) && (v.VariableValue != null))
                {
                    result = result.Replace(v.VariableName, v.VariableValue.ToString());
                }
            }
            return result;
        }

        public List<StringVariable> BuildFromDlInfo(DownloadInfo stream)
        {
            DefaultStringVariables.ContentTitle.VariableValue = stream.ContentTitle;
            DefaultStringVariables.FileName.VariableValue = stream.Link;
            DefaultStringVariables.TokenHash.VariableValue = Helpers.CalculateMD5Hash(Home.settings.ConnectionInfo.PlexAccountToken);
            DefaultStringVariables.ServerPort.VariableValue = Home.settings.ConnectionInfo.PlexPort;
            DefaultStringVariables.ServerIP.VariableValue = Home.settings.ConnectionInfo.PlexAddress;
            DefaultStringVariables.ServerHash.VariableValue = Helpers.CalculateMD5Hash(Home.settings.ConnectionInfo.PlexAddress);
            return new List<StringVariable>()
                {
                    DefaultStringVariables.ContentTitle,
                    DefaultStringVariables.FileName,
                    DefaultStringVariables.TokenHash,
                    DefaultStringVariables.ServerPort,
                    DefaultStringVariables.ServerIP,
                    DefaultStringVariables.ServerHash
                };
        }
    }

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

    public static class DefaultStringVariables
    {
        public static StringVariable ContentTitle { get; set; } = new StringVariable("%TITLE%", null);
        public static StringVariable FileName { get; set; } = new StringVariable("%FILE%", null);
        public static StringVariable TokenHash { get; set; } = new StringVariable("%TOKEN_MD5%", null);
        public static StringVariable ServerHash { get; set; } = new StringVariable("%SERVER_MD5%", null);
        public static StringVariable ServerIP { get; set; } = new StringVariable("%SERVER_IP%", null);
        public static StringVariable ServerPort { get; set; } = new StringVariable("%SERVER_PORT%", null);
    }
}