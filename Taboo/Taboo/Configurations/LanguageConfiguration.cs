using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using Taboo.Entities;

namespace Taboo.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {

            builder.HasKey(model => model.Code);
            builder.Property(x => x.Code)
                  .IsRequired()
                  .HasMaxLength(2);
            builder
                .HasIndex(x => x.Name)
                .IsUnique();
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(x => x.Icon)
                .IsRequired()
                .HasMaxLength(128);
            builder
                .HasData(new Language
                {
                    Code = "az",
                    Name = "Azərbaycan",
                    Icon = "https://cdn-icons-png.flaticon.com/512/630/630657.png"
                },
            new Language
            {
                Code = "En",
                Name = "England",
                Icon = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTZc1DpoXk-JxZUHBpVSM2d_O9Ozy3zwDQuEw&s"
            });
        }
    }
}
