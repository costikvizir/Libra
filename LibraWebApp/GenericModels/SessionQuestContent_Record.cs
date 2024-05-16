using System;
using System.Runtime.Serialization;
using Ninject;
using System.Collections.Generic;

namespace GC.Models {

    [Serializable]
    [DataContract]
    public class SessionQuestContent_Record {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int SessionId { get; set; }

        [DataMember]
        public int QuestionnaireId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public int QuestState { get; set; }
            
    }
}
