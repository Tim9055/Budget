using Budget.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Budget.DAL
{
    public class BudgetContext : DbContext
    {
        public BudgetContext() : base("BudgetContext")
        {
            
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Bill> Bills { get; set; }
    }
}