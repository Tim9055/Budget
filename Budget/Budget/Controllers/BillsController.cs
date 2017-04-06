using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Budget.DAL;
using Budget.Entities;
using System.Globalization;
using System.Reflection;
using System.Web.Script.Serialization;
using Budget.Utilities;
using Newtonsoft.Json;

namespace Budget
{
    public class BillsController : Controller
    {
        private BudgetContext db = new BudgetContext();

        // GET: Bills
        public ActionResult Index(string searchMonthYear)
        {
            string monthYear = searchMonthYear == null
                ? $"{DateTime.Now.Month}/{DateTime.Now.Year}"
                : searchMonthYear;
            IEnumerable<Bill> model = db.Bills.Where(b=>b.MonthYear == monthYear).ToList();
            decimal totalDue = model.Sum(b => b.AmountDue);
            decimal totalPaid = model.Sum(b => b.AmountPaid);
            decimal totalOwed = model.Sum(b => b.AmountOwed);
            ViewBag.totalDue = totalDue;
            ViewBag.totalPaid = totalPaid;
            ViewBag.totalOwed = totalOwed;
            ViewBag.monthYear = DateTime.Parse(monthYear).ToString("MMMM yyyy");

            ViewBag.ddlSearchMonthYear = GetDdlSearchMonthYear(monthYear);

            return View(model);
        }

        private List<SelectListItem> GetDdlSearchMonthYear(string monthYear)
        {
            var billMonthYears = db.Bills.Select(b => b.MonthYear).Distinct().ToList();
            List<DateTime> dateMonthYears = new List<DateTime>();
            foreach (var item in billMonthYears)
            {
                dateMonthYears.Add(DateTime.Parse(item));
            }
            List<SelectListItem> ddlSearchMonthYear = new List<SelectListItem>();
            var firstMonthYear = dateMonthYears.OrderBy(d => d.Date).FirstOrDefault();
            var curYear = DateTime.Today.Year;
            var curMonth = DateTime.Today.Month;
            int year = firstMonthYear.Year;
            int month = firstMonthYear.Month;
            while (year < DateTime.Now.AddYears(1).Year)
            {
                while (month < 13)
                {
                    var item = new SelectListItem
                    {
                        Value = $"{month.ToString()}/{year.ToString()}",
                        Text = $"{Utilities.Get.MonthName(month)} {year.ToString()}"
                    };
                    ddlSearchMonthYear.Add(item);
                    month++;
                    if (month > curMonth + 3 && year == curYear)
                    {
                        break;
                    }
                }
                month = 1;
                year++;
            }
            ddlSearchMonthYear.First(s => s.Value == monthYear).Selected = true;
            return ddlSearchMonthYear;
        }

        // GET: Bills/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // GET: Bills/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BillId,AmountDue,AmountPaid,AmountOwed,DateDue,DatePaid,MonthYear,CompanyId")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Bills.Add(bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bill);
        }

        // GET: Bills/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BillId,AmountDue,AmountPaid,AmountOwed,DateDue,DatePaid,MonthYear")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bill);
        }

        // GET: Bills/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bill bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Update")]
        public ActionResult UpdateBillItem(int billId, string billItem, string billItemText)
        {
            string itemName = string.Empty;
            Bill bill = db.Bills.Find(billId);
            if (bill == null)
            {
                return HttpNotFound();
            }
            switch (billItem)
            {
                case "AmountDue":
                    bill.AmountDue = decimal.Parse(billItemText);
                    itemName = Reflection.GetPropertyDisplayName<Bill>(i => i.AmountDue);
                    break;
                case "AmountPaid":
                    bill.AmountPaid = decimal.Parse(billItemText);
                    itemName = Reflection.GetPropertyDisplayName<Bill>(i => i.AmountPaid);
                    break;
                case "AmountOwed":
                    bill.AmountOwed = decimal.Parse(billItemText);
                    itemName = Reflection.GetPropertyDisplayName<Bill>(i => i.AmountOwed);
                    break;
                case "DateDue":
                    bill.DateDue = DateTime.Parse(billItemText);
                    itemName = Reflection.GetPropertyDisplayName<Bill>(i => i.DateDue);
                    break;
                case "DatePaid":
                    bill.DatePaid = DateTime.Parse(billItemText);
                    itemName = Reflection.GetPropertyDisplayName<Bill>(i => i.DatePaid);
                    break;
                default:
                    return HttpNotFound();
            }
            db.Entry(bill).State = EntityState.Modified;
            db.SaveChanges();
            string monthYear = bill.MonthYear;
            IEnumerable<Bill> model = db.Bills.Where(b => b.MonthYear == monthYear).ToList();
            decimal totalDue = model.Sum(b => b.AmountDue);
            decimal totalPaid = model.Sum(b => b.AmountPaid);
            decimal totalOwed = model.Sum(b => b.AmountOwed);
            ViewBag.totalDue = totalDue;
            ViewBag.totalPaid = totalPaid;
            ViewBag.totalOwed = totalOwed;
            return PartialView("_BillsTableData", model);
        }

        [HttpPost, ActionName("PopulateBills")]
        public ActionResult PopulateBills(string monthYear)
        {
            if (string.IsNullOrEmpty(monthYear))
            {
                return HttpNotFound();
            }

            var dMonthYear = DateTime.Parse(monthYear);
            var pMonthYear = dMonthYear.AddMonths(-1).ToString("M/yyyy");

            List<Bill> pModel = db.Bills.Where(b => b.MonthYear == pMonthYear).ToList();
            foreach (var item in pModel)
            {
                Bill bill = new Bill();
                bill.AmountDue = item.AmountDue;
                bill.DateDue = item.DateDue.AddMonths(1);
                bill.CompanyId = item.CompanyId;
                bill.MonthYear = dMonthYear.ToString("M/yyyy");
                bill.DatePaid = DateTime.Parse("1/1/1900");
                bill.AmountOwed = 0;
                bill.AmountPaid = 0;
                db.Bills.Add(bill);
                db.SaveChanges();
            }
            

            IEnumerable<Bill> model = db.Bills.Where(b => b.MonthYear == monthYear).ToList();
            decimal totalDue = model.Sum(b => b.AmountDue);
            decimal totalPaid = model.Sum(b => b.AmountPaid);
            decimal totalOwed = model.Sum(b => b.AmountOwed);
            ViewBag.totalDue = totalDue;
            ViewBag.totalPaid = totalPaid;
            ViewBag.totalOwed = totalOwed;
            return PartialView("_BillsTableData", model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
