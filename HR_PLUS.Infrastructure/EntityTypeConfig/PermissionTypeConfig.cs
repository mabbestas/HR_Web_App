using HR_Plus.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Infrastructure.EntityTypeConfig
{
    class PermissionTypeConfig : BaseEntityConfig<PermissionType>
    {
        public override void Configure(EntityTypeBuilder<PermissionType> builder)
        {
            builder.HasKey(x => x.PermissionTypeId);
            builder.Property(x => x.PermissionTypeId).IsRequired(true).HasMaxLength(30);

            base.Configure(builder);
        }
    }
}
