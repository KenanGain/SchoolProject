using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolProject.Models;
using System.Diagnostics;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace SchoolProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        // GET: Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }
        // GET: Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            
            return View(NewTeacher);
        }

        // GET: Teacher/DeleteConfirm
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        //POST : /Teacher/Delete/{id}

        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/New
        public ActionResult New() 
        {
            return View();
        }

        //GET : /Teacher/Create
        [HttpPost]
        
        public ActionResult Create(string teacherfname , string teacherlname , string employeenumber , string hiredate , double salary )
        {
            //Identify that this method is running
            //Identify the inpusts provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
           // Debug.WriteLine(teacherid);
            Debug.WriteLine(teacherfname);
            Debug.WriteLine(teacherlname);
            Debug.WriteLine(employeenumber);
            // Debug.WriteLine(hiredate);
            Debug.WriteLine(salary);

            Teacher NewTeacher = new Teacher();
           // NewTeacher.teacherid = teacherid;
            NewTeacher.teacherfname = teacherfname;
            NewTeacher.teacherlname = teacherlname;
            NewTeacher.employeenumber = employeenumber;
           // NewTeacher.hiredate = hiredate;
            NewTeacher.salary= salary;

            TeacherDataController Controller = new TeacherDataController();
            Controller.AddTeacher(NewTeacher);
      

            return RedirectToAction("List");

        }

    }
}