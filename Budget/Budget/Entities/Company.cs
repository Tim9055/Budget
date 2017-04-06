using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Budget.Entities
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        
    }
}