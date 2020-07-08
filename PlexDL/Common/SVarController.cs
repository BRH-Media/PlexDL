using PlexDL.Common.Globals;
using PlexDL.Common.Structures;
using System.Collections.Generic;
using PlexDL.Common.Globals.Providers;

namespace PlexDL.Common
{
    public class SVarController
    {
        public List<StringVariable> Variables { get; set; } = new List<StringVariable>();
        public string Input { get; set; } = "";

        public string YieldString()
        {
            var result = Input;
            foreach (var v in Variables)
                if (v.VariableName != null && v.VariableValue != null)
                    result = result.Replace(v.VariableName, v.VariableValue.ToString());

            return result;
        }

        public List<StringVariable> BuildFromDlInfo(DownloadInfo stream)
        {
            DefaultStringVariables.ContentTitle.VariableValue = stream.ContentTitle;
            DefaultStringVariables.FileName.VariableValue = stream.Link;
            DefaultStringVariables.TokenHash.VariableValue = MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAccountToken);
            DefaultStringVariables.ServerPort.VariableValue = ObjectProvider.Settings.ConnectionInfo.PlexPort;
            DefaultStringVariables.ServerIp.VariableValue = ObjectProvider.Settings.ConnectionInfo.PlexAddress;
            DefaultStringVariables.ServerHash.VariableValue = MD5Helper.CalculateMd5Hash(ObjectProvider.Settings.ConnectionInfo.PlexAddress);
            return new List<StringVariable>
            {
                DefaultStringVariables.ContentTitle,
                DefaultStringVariables.FileName,
                DefaultStringVariables.TokenHash,
                DefaultStringVariables.ServerPort,
                DefaultStringVariables.ServerIp,
                DefaultStringVariables.ServerHash
            };
        }
    }
}