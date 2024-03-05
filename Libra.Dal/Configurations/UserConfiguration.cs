using Libra.Dal.Context;
using Libra.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Libra.Dal.Configurations
{
	public sealed class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(x => x.Id);

			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(50);

			builder.HasMany(e => e.Issues)
				.WithOne(e => e.User)
				.HasForeignKey(e => e.UserCreatedId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			builder.HasMany(e => e.Logs)
				.WithOne(e => e.User)
				.HasForeignKey(e => e.UserId)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired();

			builder.HasData(SeedData.UsersSeed);
		}
	}
}
