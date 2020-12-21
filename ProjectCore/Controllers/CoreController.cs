using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using ProjectCore.Data;
using ProjectCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCore.Controllers
{
    public class CoreController : Controller
    {

        SanalData _model;
        public CoreController(SanalData model)
        {
            _model = model;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Index(string UserName,string UserPassword)
        {
            var userKont = _model.users.SingleOrDefault(i => i.UserName.ToUpper() == UserName.ToUpper() && i.UserPassword == UserPassword);
            if (userKont==null)
            {
                ViewBag.mesaj = "Kullanıcı Adı ya da Şifreniz yanlış girdiniz.Tekrar deneyiniz.";
                return View();
            }
            return RedirectToAction("StudentList");
        }
        public IActionResult StudentList()
        {
            var student = _model.students;
            return View(student);
        }
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(string UserName,string UserPassword)
        {
            var userKont =_model.users.SingleOrDefault(i => i.UserName.ToUpper() == UserName.ToUpper());
            if (userKont != null)
            {
                ViewBag.Mesaj = "Sistemde " + UserName + "kullanıcı adını içeren kayıt bulunmaktadır.Farklı bir kullanıcı adı giriniz.";
              
            }
            else
            {
                var yeniKullanici = new User { Id = 3, UserName = UserName, UserPassword = UserPassword, UserRole = 1 };
                _model.users.Add(yeniKullanici);
                ViewBag.Mesaj = "Kaydınız başarıyla tamamlanmıştır.Giriş Yapailirsiniz.";
                
            }
            return View();
        }


        public IActionResult DelStudent(int id)
        {
            var silinecek = _model.students.SingleOrDefault(i => i.Id == id);
            _model.students.Remove(silinecek);
            return RedirectToAction("StudentList");

        }

        public IActionResult UpdateStudent(int id)
        {
            var editStudent = _model.students.SingleOrDefault(i => i.Id == id);
            return View(editStudent);
        }

        [HttpPost]
        public IActionResult UpdateStudent(Student student)
        {
            var updateStudent = _model.students.SingleOrDefault(x => x.Id == student.Id);
            TryUpdateModelAsync(updateStudent);
            return RedirectToAction("StudentList");
        }

        public IActionResult ExcelExport()
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "Student.xlsx";
            var student = _model.students.ToList();
            var workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Student");
            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "StudentName";
            worksheet.Cell(1, 3).Value = "StudentSurname";
            worksheet.Cell(1, 4).Value = "StudentNumber";
            worksheet.Cell(1, 5).Value = "Class";
            for (int index = 1; index <= student.Count; index++)
            {
                worksheet.Cell(index + 1, 1).Value = student[index - 1].Id;
                worksheet.Cell(index + 1, 2).Value = student[index - 1].StudentName;
                worksheet.Cell(index + 1, 3).Value = student[index - 1].StudentSurname;
                worksheet.Cell(index + 1, 4).Value = student[index - 1].StudentNumber;
                worksheet.Cell(index + 1, 5).Value = student[index - 1].Class;

            }
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, contentType, fileName);
            }
          
        }
    }
}
