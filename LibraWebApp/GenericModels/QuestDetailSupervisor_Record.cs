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
    public class QuestDetailSupervisor_Record {
        [DataMember]
        public int Id { get; set; }
        /// <summary>
        /// Link to SessQuestContSupervisor
        /// </summary>
        [DataMember]
        public int SessSuperContentId { get; set; }

        [DataMember]
        public int QuestionId { get; set; }

        [DataMember]
        public Supplier_Record Supplier { get; set; }

        /// <summary>
        /// Submitted answers
        /// </summary>
        public List<Answer_Record> Answers { get; set; }

        public QuestDetailSupervisor_Record() {
            this.Answers = new List<Answer_Record>();
        }
    }
}
