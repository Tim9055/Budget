using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Budget.Entities;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Budget.DAL
{
    public class BudgetInitializer : DropCreateDatabaseIfModelChanges<BudgetContext>
    {
        protected override void Seed(BudgetContext context)
        {
            base.Seed(context);

            // proceed with the seed here
            var companies = new List<Company> {
                new Company { Name="Parking" },
                new Company { Name="Alarm" },
                new Company { Name="Cardinal Financial" },
                new Company { Name="Comcast" },
                new Company { Name="Cricket" },
                new Company { Name="Deffenbaugh" },
                new Company { Name="KCPL" },
                new Company { Name="LS Water" },
                new Company { Name="MGE" },
                new Company { Name="PayPal" }
            };

            companies.ForEach(c => context.Companies.Add(c));
            context.SaveChanges();

            var bills = new List<Bill>
            {
                new Bill { BillId=1, AmountDue=70.00M, AmountOwed=0.00M, AmountPaid=0.00M, DateDue=DateTime.Parse("9/2/2016"), DatePaid=DateTime.Parse("1/1/1900"),  CompanyId=1 },
                new Bill { BillId=2, AmountDue=30.94M, AmountOwed=0.00M, AmountPaid=0.00M, DateDue=DateTime.Parse("9/2/2016"), DatePaid=DateTime.Parse("1/1/1900"),  CompanyId=2  },
                new Bill { BillId=3, AmountDue=958.00M, AmountOwed=0.00M, AmountPaid=0.00M, DateDue=DateTime.Parse("9/2/2016"), DatePaid=DateTime.Parse("1/1/1900"), CompanyId=3  },
                new Bill { BillId=4, AmountDue=132.29M, AmountOwed=0.00M, AmountPaid=0.00M, DateDue=DateTime.Parse("9/2/2016"), DatePaid=DateTime.Parse("1/1/1900"), CompanyId=4  },
                new Bill { BillId=5, AmountDue=45.00M, AmountOwed=0.00M, AmountPaid=0.00M, DateDue=DateTime.Parse("9/2/2016"), DatePaid=DateTime.Parse("1/1/1900"),  CompanyId=5  },
                new Bill { BillId=6, AmountDue=97.57M, AmountOwed=0.00M, AmountPaid=0.00M, DateDue=DateTime.Parse("9/2/2016"), DatePaid=DateTime.Parse("1/1/1900"),  CompanyId=6  },
                new Bill { BillId=7, AmountDue=139.29M, AmountOwed=0.00M, AmountPaid=0.00M, DateDue=DateTime.Parse("9/2/2016"), DatePaid=DateTime.Parse("1/1/1900"), CompanyId=7 },
                new Bill { BillId=8, AmountDue=52.12M, AmountOwed=0.00M, AmountPaid=0.00M, DateDue=DateTime.Parse("9/2/2016"), DatePaid=DateTime.Parse("1/1/1900"),  CompanyId=8  },
                new Bill { BillId=9, AmountDue=63.02M, AmountOwed=0.00M, AmountPaid=0.00M, DateDue=DateTime.Parse("9/2/2016"), DatePaid=DateTime.Parse("1/1/1900"),  CompanyId=9  },
                new Bill { BillId=10, AmountDue=100.00M, AmountOwed=0.00M, AmountPaid=0.00M, DateDue=DateTime.Parse("9/2/2016"), DatePaid=DateTime.Parse("1/1/1900"), CompanyId=10 }

            };

            bills.ForEach(b => context.Bills.Add(b));
            context.SaveChanges();
        }
    }
}

