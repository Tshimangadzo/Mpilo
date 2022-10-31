using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mpilo1.Models;

namespace Mpilo1.Controllers
{
    public class CustomersController : Controller
    {
        private readonly MpiloContext _context;

        public CustomersController(MpiloContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> SearchCustomerAsync(string searchValue,string searchColunm) {
            var searchList = await _context.Customer.ToListAsync();
  
            if (searchColunm == "Name")
            {
                searchList = searchList.Where(customer => customer.Name == searchValue).ToList();
            }
            else if (searchColunm == "Surname")
            {

                searchList = searchList.Where(customer => customer.Surname == searchValue).ToList();
            }
            else if (searchColunm == "Email")
            {

                searchList = searchList.Where(customer => customer.Email == searchValue).ToList();
            }
            else if (searchColunm == "ZipCode")
            {
                searchList = searchList.Where(customer => customer.ZipCode.ToString() == searchValue).ToList();
            }
            else if (searchColunm == "PhoneNumber")
            {
                searchList = searchList.Where(customer => customer.PhoneNumber == searchValue).ToList();

            }
            else if (searchColunm == "City")
            {
                searchList = searchList.Where(customer => customer.City == searchValue).ToList();
            }
            else if(searchColunm == "id")
            {

               searchList = searchList.Where(customer => customer.Id.ToString() == searchValue).ToList();
            }
            return searchList;
        }


        public List<Customer> SortCustomer(string sortOrder)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            ViewBag.SurnameSortParm = sortOrder == "Surname" ? "Surname_desc" : "Surname";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.PhoneNumberSortParm = sortOrder == "PhoneNumber" ? "PhoneNumber_desc" : "PhoneNumber";
            ViewBag.ZipCodeSortParm = sortOrder == "ZipCode" ? "ZipCode_desc" : "ZipCode";
            ViewBag.CitySortParm = sortOrder == "City" ? "City_desc" : "City";

  
            var searchList = from s in _context.Customer
                             select s;

            switch (sortOrder)
            {
                case "Name_desc":
                    searchList = searchList.OrderByDescending(s => s.Name);
                    break;
                case "Surname":
                    searchList = searchList.OrderBy(s => s.Surname);
                    break;
                case "Surname_desc":
                    searchList = searchList.OrderByDescending(s => s.Surname);
                    break;
                case "Email":
                    searchList = searchList.OrderBy(s => s.Email);
                    break;
                case "Email_desc":
                    searchList = searchList.OrderByDescending(s => s.Surname);
                    break;
                case "PhoneNumber":
                    searchList = searchList.OrderBy(s => s.PhoneNumber);
                    break;
                case "PhoneNumber_desc":
                    searchList = searchList.OrderByDescending(s => s.PhoneNumber);
                    break;
                case "ZipCode":
                    searchList = searchList.OrderBy(s => s.ZipCode);
                    break;
                case "ZipCode_desc":
                    searchList = searchList.OrderByDescending(s => s.ZipCode);
                    break;
                case "City":
                    searchList = searchList.OrderBy(s => s.ZipCode);
                    break;
                case "City_desc":
                    searchList = searchList.OrderByDescending(s => s.ZipCode);
                    break;
                default:
                    searchList = searchList.OrderBy(s => s.Id);
                    break;
            }

            return searchList.ToList();
        }


        // GET: Customers
        public async Task<ViewResult> IndexAsync(string searchValue,string searchColunm,string sortOrder)
        {

            List<Customer> searchList = await SearchCustomerAsync(searchValue, searchColunm);

            if (searchValue != null && searchColunm != null) {
                return View(searchList);
            }

            searchList = SortCustomer(sortOrder);

            return View(searchList);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Create([Bind("Id,Name,Surname,Email,PhoneNumber,ZipCode,City")] Customer customer)
        {
            if (ModelState.IsValid)
            {
               // string searchValue,string searchColunm,string sortOrder

                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { searchValue= "", searchColunm="", sortOrder="" }) ;
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Email,PhoneNumber,ZipCode,City")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { searchValue = "", searchColunm = "", sortOrder = "" });
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { searchValue = "", searchColunm = "", sortOrder = "" });
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }
    }
}
