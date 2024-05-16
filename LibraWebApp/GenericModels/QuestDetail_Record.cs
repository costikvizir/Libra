using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace GC.Models {
    /// <summary>
    /// Container class with info about Answer. 
    /// To be used in MVC & Business 
    /// </summary>
    [Serializable]
    [DataContract]
    public class QuestDetail_Record {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int SessionContentId { get; set; }

        [DataMember]
        public int SessQuestContSupplierId { get; set; }
        
        [DataMember]
        public int QuestionId { get; set; }

        [DataMember]
        public Supplier_Record Supplier { get; set; }

        /// <summary>
        /// Submitted answers
        /// </summary>
        public List<Answer_Record> Answers { get; set; }

        public QuestDetail_Record() {
            this.Answers = new List<Answer_Record>();
        }
    }
}
