using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentValidation.Models;

namespace StudentValidation.Controllers
{
    public class StudentDetailsController : Controller
    {
        private StudentContext db = new StudentContext();

        // GET: StudentDetails
        public ActionResult Index()
        {
            return View(db.StudentDetails.ToList());
        }

        // GET: StudentDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentDetails studentDetails = db.StudentDetails.Find(id);
            if (studentDetails == null)
            {
                return HttpNotFound();
            }
            return View(studentDetails);
        }

        // GET: StudentDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "studentNumber,firstName,surname,email,tel,cell,isActive")] StudentDetails studentDetails)
        {
            if (ModelState.IsValid)
            {
                db.StudentDetails.Add(studentDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentDetails);
        }

        // GET: StudentDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentDetails studentDetails = db.StudentDetails.Find(id);
            if (studentDetails == null)
            {
                return HttpNotFound();
            }
            return View(studentDetails);
        }

        // POST: StudentDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "studentNumber,firstName,surname,email,tel,cell,isActive")] StudentDetails studentDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentDetails);
        }

        // GET: StudentDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentDetails studentDetails = db.StudentDetails.Find(id);
            if (studentDetails == null)
            {
                return HttpNotFound();
            }
            return View(studentDetails);
        }

        // POST: StudentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            StudentDetails studentDetails = db.StudentDetails.Find(id);
            db.StudentDetails.Remove(studentDetails);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult Search(string name, string searchBy)
        {
            if ((ModelState.IsValid) && (!string.IsNullOrEmpty(name)) && (!string.IsNullOrEmpty(searchBy)))
            {
                var AzureTable = new AzureTablesBusiness.AzureTablesBusiness();
                return View("Index", AzureTable.SearchStudents("student", name, searchBy));
            }

            return RedirectToAction("Index");
        }

        //For the Business Layer
        public List<StudentEntity> SearchStudents(string tableName, string name, string searchBy)
        {

            var table = GetTableReference(tableName);


            if (searchBy == "Name" )
            {

                var query = (from stu in table.CreateQuery<StudentEntity>().Execute()
                             where 
                             (stu.firstName.Contains(name)) && (stu.isActive.Equals(true)) 
                             select stu);
            }
            else if (searchBy == "Surname")
            {

                var query = (from stu in table.CreateQuery<StudentEntity>().Execute()
                            (stu.surname.Contains(name)) && (stu.isActive.Equals(true))
                             select stu);
            }

            else if (searchBy == "Number")
            {

                var query = (from stu in table.CreateQuery<StudentEntity>().Execute()
                             (stu.studentName.Contains(name)) && (stu.isActive.Equals(true))
                             select stu);
            }
            return new List<StudentEntity>(query);
        }


    }
}
