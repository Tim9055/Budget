using Budget.DAL;
using Budget.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Budget.Migrations
{
    public class EntitiesContextInitializer : DbMigrationsConfiguration<BudgetContext>
    {
        public EntitiesContextInitializer()
        {
            AutomaticMigrationsEnabled = true;
        }


    }
}
