namespace OptionsWebsite.Migrations.Option
{
    using DiplomaDataModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DiplomaDataModel.OptionsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\Option";
        }

        protected override void Seed(DiplomaDataModel.OptionsContext context)
        {

            context.YearTerms.AddOrUpdate(
                  p => new { p.Year, p.Term },
                  new YearTerm { Year = 2015, Term = 10, isDefault = false },
                  new YearTerm { Year = 2015, Term = 20, isDefault = false },
                  new YearTerm { Year = 2015, Term = 30, isDefault = false },
                  new YearTerm { Year = 2016, Term = 10, isDefault = true,}
                );
            context.SaveChanges();


            context.Options.AddOrUpdate(
                 p => new { p.Title ,p.isActive},
                new Options
                {
                    Title = "Data Communications",
                    isActive = true,
                },
                new Options
                {
                    Title = "Client Server",
                    isActive = true,
                },
                new Options
                {
                    Title = "Digital Processing",
                    isActive = true,
                },
                new Options
                {
                    Title = "Information Systems",
                    isActive = true,
                },
                new Options
                {
                    Title = "Database",
                    isActive = false,
                },
                new Options
                {
                    Title = "Web & Mobile",
                    isActive = true,
                },
                new Options
                {
                    Title = "Tech Pro",
                    isActive = false,
                }
               );
           
            context.SaveChanges();
        }
    }
}
