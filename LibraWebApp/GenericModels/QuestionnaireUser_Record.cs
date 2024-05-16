using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.Models
{
    /// <summary>
    /// info about user into an questionnaire
    /// </summary>
    public class QuestionnaireUser_Record
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool IsEnabled { get; set; }
        public int SessionContentId { get; set; }
        /// <summary>
        /// questions of an user into an questionnaire
        /// </summary>
        public List<Page_Record> Pages { get; set; }
    }
}
