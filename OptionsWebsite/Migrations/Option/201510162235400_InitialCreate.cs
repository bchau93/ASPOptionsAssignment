namespace OptionsWebsite.Migrations.Option
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Choices",
                c => new
                    {
                        ChoiceId = c.Int(nullable: false, identity: true),
                        YearTermId = c.Int(nullable: false),
                        StudentId = c.String(nullable: false),
                        StudentFirstName = c.String(nullable: false, maxLength: 40),
                        StudentLastName = c.String(nullable: false, maxLength: 40),
                        FirstChoiceOptionId = c.Int(),
                        SecondChoiceOptionId = c.Int(),
                        ThirdChoiceOptionId = c.Int(),
                        FourthChoiceOptionId = c.Int(),
                        SelectionDate = c.DateTime(nullable: false),
                    })

                .PrimaryKey(t => t.ChoiceId)
                .ForeignKey("dbo.Options", t => t.FirstChoiceOptionId)
                .ForeignKey("dbo.Options", t => t.FourthChoiceOptionId)
                .ForeignKey("dbo.Options", t => t.SecondChoiceOptionId)
                .ForeignKey("dbo.Options", t => t.ThirdChoiceOptionId)
                .ForeignKey("dbo.YearTerms", t => t.YearTermId, cascadeDelete: true)
                .Index(t => t.YearTermId)
                .Index(t => t.FirstChoiceOptionId)
                .Index(t => t.SecondChoiceOptionId)
                .Index(t => t.ThirdChoiceOptionId)
                .Index(t => t.FourthChoiceOptionId);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        OptionsId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OptionsId);
            
            CreateTable(
                "dbo.YearTerms",
                c => new
                    {
                        YearTermId = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Term = c.Int(nullable: false),
                        isDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.YearTermId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Choices", "YearTermId", "dbo.YearTerms");
            DropForeignKey("dbo.Choices", "ThirdChoiceOptionId", "dbo.Options");
            DropForeignKey("dbo.Choices", "SecondChoiceOptionId", "dbo.Options");
            DropForeignKey("dbo.Choices", "FourthChoiceOptionId", "dbo.Options");
            DropForeignKey("dbo.Choices", "FirstChoiceOptionId", "dbo.Options");
            DropIndex("dbo.Choices", new[] { "FourthChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "ThirdChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "SecondChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "FirstChoiceOptionId" });
            DropIndex("dbo.Choices", new[] { "YearTermId" });
            DropTable("dbo.YearTerms");
            DropTable("dbo.Options");
            DropTable("dbo.Choices");
        }
    }
}
