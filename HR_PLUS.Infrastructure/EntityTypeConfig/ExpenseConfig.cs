using HR_Plus.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_PLUS.Infrastructure.EntityTypeConfig
{
    class ExpenseConfig : BaseEntityConfig<Expense>
    {
        public override void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(x => x.ExpenseId);
            builder.Property(x => x.ExpenseDescription).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.ExpenseDate).IsRequired(true);
            builder.Property(x => x.Amount).IsRequired(true);
            builder.Property(x => x.ImagePath).IsRequired(false);

            base.Configure(builder);
        }
    }
}
