using System;
using System.Runtime.Serialization;
using Ninject;

namespace GC.Models {
    /// <summary>
    /// Container class with info about Answer. 
    /// To be used in MVC & Business 
    /// </summary>
    [Serializable]
    [DataContract]
    public class Answer_Record {
        /// <summary>
        /// Nullable becaues a !IsWAC has no predefined answer and id in DB
        /// </summary>
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public int QuestionId { get; set; }

        [DataMember]
        public string Answer { get; set; }

        /// <summary>
        /// Nullable because is not valid for !IsMultipleAnswer
        /// </summary>
        [DataMember]
        public int? SortOrder { get; set; }

        [DataMember]
        public decimal? AnswerRate { get; set; }
    }
}
