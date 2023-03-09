using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PlexDL.MyPlex
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class Container : PlexRest

    {
        private Server[] _serverField;

        private string _friendlyNameField;

        private string _identifierField;

        private string _machineIdentifierField;

        private string _sizeField;

        [XmlElement("Server", Form = XmlSchemaForm.Unqualified)]
        public Server[] Server
        {
            get => _serverField;
            set => _serverField = value;
        }

        [XmlAttribute]
        public string FriendlyName
        {
            get => _friendlyNameField;
            set => _friendlyNameField = value;
        }

        [XmlAttribute]
        public string Identifier
        {
            get => _identifierField;
            set => _identifierField = value;
        }

        [XmlAttribute]
        public string MachineIdentifier
        {
            get => _machineIdentifierField;
            set => _machineIdentifierField = value;
        }

        [XmlAttribute]
        public string Size
        {
            get => _sizeField;
            set => _sizeField = value;
        }
    }
}