using System;
using System.Runtime.Serialization;
using Ninject;
using System.Collections.Generic;

namespace GC.Models {
   
    [Serializable]
    [DataContract]
    public class QuestSession_Record {
        [DataMember]
        public int Id { get; set; }
       
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        public bool IsOpened { get; set; }

        [DataMember]
        public List<Questionnaires_Record> SelectedRowList { get; set; }
    }
}
