using JobDataAccess.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobDataAccess.Fluents;

public sealed class Job : IEntityTypeConfiguration<Entities.Job>
{
    public void Configure(EntityTypeBuilder<Entities.Job> builder)
    {
        builder
            .ToTable("job");

        builder
             .HasKey(b => b.Id);

        builder
            .Property(b => b.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

        builder
             .Property(b => b.ObjectId)
             .IsRequired()
             // почему-то не находит функцию в схеме по умолчанию. Поэтому сделаю пока так
             .HasDefaultValueSql($"{DbConfig.SCHEMA_NAME}.uuid_generate_v4()");

        builder
             .Property(b => b.Name)
             .HasMaxLength(300)
             .IsRequired();

        builder
             .Property(b => b.MinSalary)
             .IsRequired(false);

        builder
             .Property(b => b.MaxSalary)
             .IsRequired(false);

        builder
             .Property(b => b.Responsibilities)
             .HasMaxLength(1000)
             .IsRequired();

        builder
             .Property(b => b.Requirements)
             .HasMaxLength(1000)
             .IsRequired();

        builder
             .Property(b => b.Conditions)
             .HasMaxLength(1000)
             .IsRequired();

        builder
             .Property(b => b.Deleted)
             .IsRequired()
             .HasDefaultValue(false);

        builder
             .Property(b => b.Created)
             .IsRequired()
             .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder
             .Property(b => b.CreateUserId)
             .IsRequired();

        // Indexes

        builder
            .HasIndex(b => b.ObjectId)
            .IsUnique();

        builder
            .HasIndex(b => b.Name);

        builder
            .HasIndex(b => b.Created);

        builder
            .HasIndex(b => b.CreateUserId);

        // FullText search support

        builder
            .HasGeneratedTsVectorColumn(
                p => p.ResponsibilitiesVector,
                DbConfig.FT_SEARCH_CONFIG,
                p => p.Responsibilities)
            .HasIndex(p => p.ResponsibilitiesVector)
            .HasMethod("GIN");

        builder
            .HasGeneratedTsVectorColumn(
                p => p.RequirementsVector,
                DbConfig.FT_SEARCH_CONFIG,
                p => p.Requirements)
            .HasIndex(p => p.RequirementsVector)
            .HasMethod("GIN");

        builder
            .HasGeneratedTsVectorColumn(
                p => p.ConditionsVector,
                DbConfig.FT_SEARCH_CONFIG,
                p => p.Conditions)
            .HasIndex(p => p.ConditionsVector)
            .HasMethod("GIN");
    }
}
