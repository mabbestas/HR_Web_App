using HR_Plus.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Infrastructure.EntityTypeConfig
{
    class AppUserConfig : BaseEntityConfig<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(30);
            builder.Property(x => x.Surname).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.BirthDate).IsRequired(true);
            builder.HasIndex(x => x.Email).IsUnique(false);
            builder.HasIndex(x => x.NormalizedEmail).IsUnique(false);
            builder.HasIndex(x => x.UserName).IsUnique(false);
            builder.HasIndex(x => x.NormalizedUserName).IsUnique(false);
            builder.Property(x => x.HireDate).IsRequired(true);
            builder.Property(x => x.Gender).IsRequired(true);
            builder.Property(x => x.Address).IsRequired(true);

            base.Configure(builder);
        }
    }
}
