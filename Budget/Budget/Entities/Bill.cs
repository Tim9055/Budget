using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Budget.Entities
{
    public class Bill
    {
        public int BillId { get; set; }
        [DisplayName("Amount Due")]
        public decimal AmountDue { get; set; }
        [DisplayName("Amount Paid")]
        public decimal AmountPaid { get; set; }
        [DisplayName("Amount Owed")]
        public decimal AmountOwed { get; set; }
        [DisplayName("Date Due")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateDue { get; set; }
        [DisplayName("Date Paid")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}",ApplyFormatInEditMode = true)]
        public DateTime DatePaid { get; set; }
        [DisplayName("Bill For")]
        public string MonthYear { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; } 
    }
}