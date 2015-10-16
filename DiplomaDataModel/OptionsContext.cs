using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Data.Entity;

namespace DiplomaDataModel
{
    public class OptionsContext : DbContext
    {
        public OptionsContext() : base("DefaultConnection") { }

        public DbSet<YearTerm> YearTerms { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<Choice> Choices { get; set; }
    }
}