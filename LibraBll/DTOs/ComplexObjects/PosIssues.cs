using LibraBll.DTOs.Issue;
using LibraBll.DTOs.Pos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.DTOs.ComplexObjects
{
    public class PosIssues
    {
        public PosGetDTO PosGet { get; set; }
        public List<IssueDTO> Issues { get; set; }
    }
}
