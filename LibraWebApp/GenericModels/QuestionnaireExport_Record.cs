using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.Models
{
    public class QuestionnaireExport_Record
    {
        public string User { get; set; }
        public string Supplier { get; set; }
        public string Page { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int AnswerSortOrder { get; set; }
        public int QuestSortOrder { get; set; }
        public int PageSortOrder { get; set; }
    }
}
