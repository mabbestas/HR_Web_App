using HR_Plus.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Infrastructure.EntityTypeConfig
{
    class AdvancePaymentConfig : BaseEntityConfig<AdvancePayment>
    {
        public override void Configure(EntityTypeBuilder<AdvancePayment> builder)
        {
            builder.HasKey(x => x.AdvancePaymentId);
            builder.Property(x => x.AdvancePaymentDesc).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.Amount).IsRequired(true);

            base.Configure(builder);
        }
    }
}
