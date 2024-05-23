using Libra.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libra.Dal.Configurations
{
    public class IssueNameConfiguration : EntityTypeConfiguration<IssueName>
    {
        public IssueNameConfiguration()
        {
            HasKey(x => x.Id);

            HasMany(i => i.IssueTypes)
                .WithRequired(i => i.IssueName)
                .HasForeignKey(i => i.IssueNameId)
                .WillCascadeOnDelete(false);
        }
    }
}
