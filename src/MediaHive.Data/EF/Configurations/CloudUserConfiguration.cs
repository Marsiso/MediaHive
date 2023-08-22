using MediaHive.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediaHive.Data.EF.Configurations;

public class CloudUserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable(Tables.Users);

		builder.HasKey(cu => cu.UserID);
		
		builder.HasIndex(cu => cu.Email)
			.IsUnique();
		
		builder.HasIndex(cu => cu.GoogleCloudIdentityIdentifier)
			.IsUnique();

		builder.Property(cu => cu.UserID)
			.IsRequired()
			.ValueGeneratedOnAdd();
		
		builder.Property(cu => cu.FirstName)
			.IsRequired()
			.IsUnicode()
			.HasMaxLength(256);
		
		builder.Property(cu => cu.LastName)
			.IsRequired()
			.IsUnicode()
			.HasMaxLength(256);
		
		builder.Property(cu => cu.Email)
			.IsRequired()
			.HasMaxLength(256);
		
		builder.Property(cu => cu.GoogleCloudIdentityIdentifier)
			.IsRequired()
			.HasMaxLength(512);
		
		builder.Property(cu => cu.GoogleCloudIdentityIdentifier)
			.IsRequired()
			.HasMaxLength(4096);

		builder.Property(u => u.DateCreated)
			.HasDefaultValueSql("datetime('now', 'localtime')")
			.ValueGeneratedOnAdd();
		
		builder.Property(u => u.DateUpdated)
			.HasDefaultValueSql("datetime('now', 'localtime')")
			.ValueGeneratedOnAddOrUpdate();

		builder.HasOne(cu => cu.UserCreatedBy)
			.WithMany()
			.HasForeignKey(cu => cu.CreatedBy)
			.OnDelete(DeleteBehavior.NoAction);
		
		builder.HasOne(cu => cu.UserUpdatedBy)
			.WithMany()
			.HasForeignKey(cu => cu.UpdatedBy)
			.OnDelete(DeleteBehavior.NoAction);
	}
}