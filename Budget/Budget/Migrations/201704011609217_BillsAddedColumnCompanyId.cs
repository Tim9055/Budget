namespace Budget.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BillsAddedColumnCompanyId : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        BillId = c.Int(nullable: false, identity: true),
                        AmountDue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AmountOwed = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateDue = c.DateTime(nullable: false),
                        DatePaid = c.DateTime(nullable: false),
                        MonthYear = c.String(),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BillId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zipcode = c.String(),
                    })
                .PrimaryKey(t => t.CompanyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bills", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Bills", new[] { "CompanyId" });
            DropTable("dbo.Companies");
            DropTable("dbo.Bills");
        }
    }
}
