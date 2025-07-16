using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoMvc.Data;
using DemoMvc.Models;
using StudentEntity = DemoMvc.Models.Entities.Student;
using DemoMvc.Models.Process;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using X.PagedList;

namespace DemoMvc.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        private ExcelProcess _excelProcess = new ExcelProcess();
        // Bạn cần inject IWebHostEnvironment vào Controller của mình
        // để lấy đường dẫn tới thư mục wwwroot một cách đáng tin cậy.
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Student
        public IActionResult Index(int? page, int? PageSize)
        {
             ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="3", Text= "3" },
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
            };
            int pagesize = (PageSize ?? 3);
            ViewBag.psize = pagesize;
            var students = _context.Students.OrderBy(s => s.StudentID);
            var model = students.ToList().ToPagedList(page ?? 1, pagesize);
            return View(model);
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            AutoGenerateId autoGenerateId = new AutoGenerateId();
            //1. Lay ra ban ghi moi nhat cua Student
            var student = _context.Students.OrderByDescending(s => s.StudentID).FirstOrDefault();
            //2. Neu student == null thi gan StudentID = ST0
            var studentID = student == null ? "ST000" : student.StudentID;
            var newStudentID = autoGenerateId.GenerateId(studentID);
            var newStudent = new StudentEntity
            {
                StudentID = newStudentID,
                FullName = string.Empty
            };
            return View(newStudent);
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,FullName,Address")] StudentEntity student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentID,FullName,Address")] StudentEntity student)
        {
            if (id != student.StudentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Action GET để hiển thị form upload
        public IActionResult Upload()
        {
            return View("TestView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("file", "Please select a file to upload.");
                return View();
            }

            string fileExtension = Path.GetExtension(file.FileName);
            var allowedExtensions = new[] { ".xls", ".xlsx" };
            if (!allowedExtensions.Contains(fileExtension.ToLower()))
            {
                ModelState.AddModelError("file", "Invalid file type. Please upload an Excel file (.xls, .xlsx).");
                return View();
            }

            try
            {
                string uploadFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "excels");
                if (!Directory.Exists(uploadFolderPath))
                {
                    Directory.CreateDirectory(uploadFolderPath);
                }
                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = Path.Combine(uploadFolderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Đọc dữ liệu từ file Excel vào DataTable
                var dt = _excelProcess.ExcelToDataTable(filePath);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var student = new StudentEntity
                    {
                        StudentID = dt.Rows[i][0]?.ToString() ?? "",
                        FullName = dt.Rows[i][1]?.ToString() ?? "",
                        Address = dt.Rows[i][2]?.ToString() ?? ""
                    };
                    _context.Students.Add(student);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"An error occurred: {ex.Message}");
            }
            return View();
        }

        // Action Download để xuất danh sách Student ra file Excel
        public IActionResult Download()
        {
            // Đặt tên file khi tải về
            var fileName = "Students.xlsx";
            using (var excelPackage = new OfficeOpenXml.ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                // Thêm tiêu đề cột
                worksheet.Cells["A1"].Value = "StudentID";
                worksheet.Cells["B1"].Value = "FullName";
                worksheet.Cells["C1"].Value = "Address";

                // Lấy danh sách sinh viên
                var studentList = _context.Students.ToList();
                int row = 2;
                foreach (var student in studentList)
                {
                    worksheet.Cells[$"A{row}"].Value = student.StudentID;
                    worksheet.Cells[$"B{row}"].Value = student.FullName;
                    worksheet.Cells[$"C{row}"].Value = student.Address;
                    row++;
                }

                // Tạo stream để trả về file
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                stream.Position = 0;
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }
    }
}
