using DemoMvc.Data;
using DemoMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoMvc.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Employee/Index/
        public async Task<IActionResult> Index()
        {
            var model = await _context.Employee.ToListAsync();
            return View(model);
        }

        // GET: /Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Employee.Add(emp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }

        // GET: /Employee/Edit/{id}
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();
            var emp = await _context.Employee.FindAsync(id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        // POST: /Employee/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Employee emp)
        {
            if (id != emp.EmployeeId) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(emp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emp);
        }

        // GET: /Employee/Delete/{id}
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            var emp = await _context.Employee.FindAsync(id);
            if (emp == null) return NotFound();
            return View(emp);
        }

        // POST: /Employee/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var emp = await _context.Employee.FindAsync(id);
            if (emp != null)
            {
                _context.Employee.Remove(emp);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
