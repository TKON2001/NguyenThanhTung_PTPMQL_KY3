namespace DemoMvc.Controllers
{
    using DemoMvc.Data;
    using DemoMvc.Models;
    using DemoMvc.Models.Entities;
    using DemoMvc.Models.Process;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using PersonEntity = DemoMvc.Models.Entities.Person;

    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            return View(await _context.Persons.ToListAsync());
        }

        //create a new person
        public IActionResult Create()
        {
            //1. Lay ra ban ghi moi nhat cua Person
            var person = _context.Persons.OrderByDescending(p => p.PersonID).FirstOrDefault();
            //2. Neu person == null thi gan PersonID = PS0
            var personID = person == null ? "PS0" : person.PersonID;
            //3. Goi toi phuong thuc sinh id tu dong
            var autoGenerateId = new AutoGenerateId();
            var newPersonID = autoGenerateId.GenerateId(personID);
            var newPerson = new PersonEntity
            {
                PersonID = newPersonID
            };
            return View(newPerson);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonID,FullName")] PersonEntity person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }
    }
}