using System;
using System.Runtime.Serialization;
using Ninject;

namespace GC.Models {
    /// <summary>
    /// Container class with info about Supplier. 
    /// To be used in MVC & Business 
    /// </summary>
    [Serializable]
    [DataContract]
    public class Supplier_Record {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }     
        [DataMember]
        public string CodeIVA { get; set; }
        [DataMember]
        public string CodeSPA { get; set; }
        [DataMember]
        public string CodeFiscal { get; set; }
        [DataMember]
        public bool IsEnabled { get; set; }  
    }
}
