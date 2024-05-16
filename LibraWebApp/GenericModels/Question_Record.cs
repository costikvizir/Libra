using System;
using System.Runtime.Serialization;
using Ninject;
using System.Collections.Generic;

namespace GC.Models {
    /// <summary>
    /// Container class with info about Answer.
    /// To be used in MVC & Business
    /// </summary>
    [Serializable]
    [DataContract]
    public class Question_Record {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Question { get; set; }
        [DataMember]
        public bool MultipleAnswer { get; set; }

        [DataMember]
        public bool IsWAC { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int NrQuestion { get; set; }

        [DataMember]
        public bool ForSupervisor { get; set; }
        
        [DataMember]
        public List<Answer_Record> Answers { get; set; }

        public Question_Record()
        {
            Answers = new List<Answer_Record>();
        }

    }
}
