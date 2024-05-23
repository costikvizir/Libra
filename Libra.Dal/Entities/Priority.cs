using Libra.Dal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Entities
{
    public class Priority : BaseEntity
    {
        public string IssuePriority { get; set; }
        public ICollection<Issue> Issues { get; set; }
    }
}
