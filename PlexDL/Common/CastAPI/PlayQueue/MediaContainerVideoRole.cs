﻿using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PlexDL.Common.CastAPI.PlayQueue
{
    /// <remarks />
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class MediaContainerVideoRole
    {
        private string filterField;
        private uint idField;

        private string roleField;

        private string tagField;

        private string thumbField;

        /// <remarks />
        [XmlAttribute]
        public uint id
        {
            get => idField;
            set => idField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string filter
        {
            get => filterField;
            set => filterField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string tag
        {
            get => tagField;
            set => tagField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string role
        {
            get => roleField;
            set => roleField = value;
        }

        /// <remarks />
        [XmlAttribute]
        public string thumb
        {
            get => thumbField;
            set => thumbField = value;
        }
    }
}