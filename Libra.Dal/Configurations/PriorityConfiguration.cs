using Libra.Dal.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Libra.Dal.Configurations
{
    public class PriorityConfiguration : EntityTypeConfiguration<Priority>
    {
        public PriorityConfiguration() 
        { 
            HasKey(p => p.Id);

            HasMany(p => p.Issues)
                .WithRequired(p => p.Priority)
                .HasForeignKey(p => p.PriorityId)
                .WillCascadeOnDelete(false);
        }
    }
}
