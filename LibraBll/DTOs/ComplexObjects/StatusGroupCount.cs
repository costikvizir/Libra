using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.DTOs.ComplexObjects
{
    public class StatusGroupCount
    {
        public int NewIssues { get; set; }
        public int AssignedIssues { get; set; }
        public int PendingIssues { get; set; }
        public int InProgressIssues { get; set; }
    }
}