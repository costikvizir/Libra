using System;
using System.Runtime.Serialization;
using Ninject;

namespace GC.Models {
    /// <summary>
    /// Container class with info about Role. 
    /// To be used in MVC & Business 
    /// </summary>
    [Serializable]
    [DataContract]
    public class Role_Record {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }        
    }
}
