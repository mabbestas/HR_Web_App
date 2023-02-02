using HR_Plus.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Infrastructure.EntityTypeConfig
{
     class CompanyConfig :BaseEntityConfig<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.CompanyId);
            builder.Property(x => x.CompanyName).IsRequired(true).HasMaxLength(30);
            builder.HasIndex(x => x.CompanyName).IsUnique(true);
            builder.Property(x => x.Adress).IsRequired(true).HasMaxLength(200);

            base.Configure(builder);
        }
    }
}
