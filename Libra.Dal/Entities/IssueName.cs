using Libra.Dal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Entities
{
    public class IssueName : BaseEntity
    {
        public string Name { get; set; }
        public int IssueRank { get; set; }
        public int? ParentId { get; set; }
        public ICollection<IssueType> IssueTypes { get; set; }
    }
}