﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LibraBll.Common.DataTableModels
{
    [Serializable]
    [DataContract]
    public class DataTablesSearch
    {
        public DataTablesSearch()
        {
            Values = new List<string>();
        }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "values")]
        public ICollection<string> Values { get; set; }

        [DataMember(Name = "regex")]
        public string Regex { get; set; }
    }
}