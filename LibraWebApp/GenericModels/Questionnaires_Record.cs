using System;
using System.Runtime.Serialization;
using Ninject;

namespace GC.Models {
   
    [Serializable]
    [DataContract]
    public class Questionnaires_Record {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? StopDate { get; set; }

        [DataMember]
        public bool IsEnabled { get; set; }
    }
}
