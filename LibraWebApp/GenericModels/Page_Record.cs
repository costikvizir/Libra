using System;
using System.Runtime.Serialization;
using Ninject;
using System.Collections.Generic;

namespace GC.Models {
    /// <summary>
    /// Container class with info about Page. 
    /// To be used in MVC & Business 
    /// </summary>
    [Serializable]
    [DataContract]
    public class Page_Record {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }     

        [DataMember]
        public int? QuestionnaireId { get; set; }

        /// <summary>
        /// Valid only for the case QuestionnaireId has value...
        /// </summary>
        [DataMember]
        public int? SortOrder { get; set; }   

        [DataMember]
        public int  NrPage { get; set; }

        [DataMember]
        public List<Question_Record> Questions { get; set; }

        public Page_Record()
        {
            Questions = new List<Question_Record>();
        }

        public override int GetHashCode() {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj) {
            return this.Id == (obj as Page_Record).Id;
        }
    }
}
