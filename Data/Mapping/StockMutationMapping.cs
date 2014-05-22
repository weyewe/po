﻿using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class StockMutationMapping : EntityTypeConfiguration<StockMutation>
    {

        public StockMutationMapping()
        {
            HasKey(sm => sm.Id);
            HasRequired(sm => sm.Item)
                .WithMany(i => i.StockMutations)
                .HasForeignKey(sm => sm.ItemId);
            Ignore(sm => sm.Errors);
        }
    }
}
