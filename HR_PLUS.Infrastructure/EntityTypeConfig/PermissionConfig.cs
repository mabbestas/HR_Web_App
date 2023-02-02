using HR_Plus.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Infrastructure.EntityTypeConfig
{
    class PermissionConfig : BaseEntityConfig<Permission>
    {
        public override void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(x => x.PermissionId);          
            builder.HasIndex(x => x.PermissionTypeId).IsUnique(false);
            builder.Property(x => x.PermissionDescription).IsRequired(true).HasMaxLength(250);
            builder.Property(x => x.PermissionStartDate).IsRequired(true);
            builder.Property(x => x.PermissionExpiryDate).IsRequired(true);

            base.Configure(builder);
        }
    }
}
