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
            List<YearTerm> yearterms = new List<YearTerm>();
            yearterms.Add(new YearTerm()
            {
                Year = 2015,
                Term = 10,
                isDefault = false,

            });
            yearterms.Add(new YearTerm()
            {
                Year = 2015,
                Term = 20,
                isDefault = false,

            });
            yearterms.Add(new YearTerm()
            {
                Year = 2015,
                Term = 30,
                isDefault = false,

            });
            yearterms.Add(new YearTerm()
            {
                Year = 2016,
                Term = 10,
                isDefault = true,

            });


            context.YearTerms.AddRange(yearterms);
            context.SaveChanges();

            List<Options> options = new List<Options>();

            options.Add(new Options()
            {
                Title = "Data Communications",
                isActive = true,
            });
            options.Add(new Options()
            {
                Title = "Client Server",
                isActive = true,
            });
            options.Add(new Options()
            {
                Title = "Digital Processing",
                isActive = true,
            });
            options.Add(new Options()
            {
                Title = "Information Systems",
                isActive = true,
            });
            options.Add(new Options()
            {
                Title = "Database",
                isActive = false,
            });
            options.Add(new Options()
            {
                Title = "Web & Mobile",
                isActive = true,
            });
            options.Add(new Options()
            {
                Title = "Tech Pro",
                isActive = false,
            });


            context.Options.AddRange(options);
            context.SaveChanges();
        }
    }
}
