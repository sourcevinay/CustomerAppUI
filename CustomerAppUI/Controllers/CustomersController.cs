using StudentCourseManagement.Lib;
using StudentCourseManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentCourseManagement.Controllers
{
    public class CustomersController : Controller
    {
        ApiRepository api;
        public CustomersController()
        {
            api = new ApiRepository();
        }
        // GET: Customers
        public async Task<ActionResult> Index()
        {
            var customers = await api.GetAll();
            ViewBag.Message = "Customers List";
            return View(customers);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var customers = await api.AddCustomers(customer);
                if (customers == 200)
                {
                    await new MailRepository().SendEmail(customer.Email, "Customer Registration", "Customer Registered Successfully!");
                    return RedirectToAction("Index");
                }
            }
            return View(customer);
        }

        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await api.FindCustomer(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var result = await api.UpdateCustomers(customer.Id, customer);
                if (result == 200)
                    return RedirectToAction("Index");
            }
            return View(customer);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = await api.FindCustomer(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var result = await api.DeleteCustomers(id);
            if (result == 200)
                return RedirectToAction("Index");
            else
                return View();
        }


    }
}