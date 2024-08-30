﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SSF.Core.OutBoxEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSF.EFCore.zamin.Events.Outbox.Dal.EF.Interceptors;

public class OutBoxEventItemConfig : IEntityTypeConfiguration<OutBoxEventItem>
{
    public void Configure(EntityTypeBuilder<OutBoxEventItem> builder)
    {
        builder.Property(c => c.AccuredByUserId).HasMaxLength(255);
        builder.Property(c => c.EventName).HasMaxLength(255);
        builder.Property(c => c.AggregateName).HasMaxLength(255);
        builder.Property(c => c.EventTypeName).HasMaxLength(500);
        builder.Property(c => c.AggregateTypeName).HasMaxLength(500);
        builder.Property(c => c.TraceId).HasMaxLength(100);
        builder.Property(c => c.SpanId).HasMaxLength(100);
        builder.ToTable("OutBoxEventItems", "zamin");
    }
}