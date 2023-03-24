using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc.Html;
using MySql.Data.MySqlClient;
using SchoolProject.Models;

namespace SchoolProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            double searchkey;

            if (double.TryParse(SearchKey, out searchkey))
            {
                MySqlConnection Conn = School.AccessDatabase();
                Conn.Open();

                MySqlCommand cmd = Conn.CreateCommand();

                cmd.CommandText = "Select * from teachers where salary >= (@key)";

                cmd.Parameters.AddWithValue("@key", Convert.ToDouble(searchkey));

                cmd.Prepare();

                MySqlDataReader ResultSet = cmd.ExecuteReader();

                List<Teacher> Teachers = new List<Teacher> { };

                while (ResultSet.Read())
                {
                    //Access Column information by the DB column name as an index
                    int TeacherId = (int)ResultSet["teacherid"];
                    string TeacherFname = (string)ResultSet["teacherfname"];
                    string TeacherLname = (string)ResultSet["teacherlname"];
                    string EmployeeNumber = (string)ResultSet["employeenumber"];
                    string HireDate = ResultSet["hiredate"].ToString();
                    double Salary = Convert.ToDouble(ResultSet["salary"]);

                    Teacher NewTeacher = new Teacher();
                    NewTeacher.teacherid = TeacherId;
                    NewTeacher.teacherfname = TeacherFname;
                    NewTeacher.teacherlname = TeacherLname;
                    NewTeacher.employeenumber = EmployeeNumber;
                    NewTeacher.hiredate = HireDate;
                    NewTeacher.salary = Salary;

                    //Add the Student Name to the List
                    Teachers.Add(NewTeacher);
                }
                //Close the connection between the MySQL Database and the WebServer
                Conn.Close();

                //Return the final list of Teacher names
                return Teachers;


            }
            else
            {

                MySqlConnection Conn = School.AccessDatabase();
                Conn.Open();

                MySqlCommand cmd = Conn.CreateCommand();

                cmd.CommandText = "Select * from teachers where lower(teacherfname) like (@key) or lower(teacherlname) like (@key) or lower(concat(teacherfname,' ',teacherlname)) like lower(@key) or lower(hiredate) like lower(@key)";

                cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");

                cmd.Prepare();

                MySqlDataReader ResultSet = cmd.ExecuteReader();

                List<Teacher> Teachers = new List<Teacher> { };

                while (ResultSet.Read())
                {
                    //Access Column information by the DB column name as an index
                    int TeacherId = (int)ResultSet["teacherid"];
                    string TeacherFname = (string)ResultSet["teacherfname"];
                    string TeacherLname = (string)ResultSet["teacherlname"];
                    string EmployeeNumber = (string)ResultSet["employeenumber"];
                    string HireDate = ResultSet["hiredate"].ToString();
                    double Salary = Convert.ToDouble(ResultSet["salary"]);

                    Teacher NewTeacher = new Teacher();
                    NewTeacher.teacherid = TeacherId;
                    NewTeacher.teacherfname = TeacherFname;
                    NewTeacher.teacherlname = TeacherLname;
                    NewTeacher.employeenumber = EmployeeNumber;
                    NewTeacher.hiredate = HireDate;
                    NewTeacher.salary = Salary;

                    //Add the Student Name to the List
                    Teachers.Add(NewTeacher);
                }
                //Close the connection between the MySQL Database and the WebServer
                Conn.Close();

                //Return the final list of Teacher names
                return Teachers;

            }
        }
        [HttpGet]
        public Teacher FindTeacher(int id) 
        {
            Teacher NewTeacher = new Teacher();

            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from teachers where teacherid = "+id;

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmployeeNumber = (string)ResultSet["employeenumber"];
                string HireDate = ResultSet["hiredate"].ToString();
                double Salary = Convert.ToDouble(ResultSet["salary"]);

                NewTeacher.teacherid = TeacherId;
                NewTeacher.teacherfname = TeacherFname;
                NewTeacher.teacherlname = TeacherLname;
                NewTeacher.employeenumber = EmployeeNumber;
                NewTeacher.hiredate = HireDate;
                NewTeacher.salary = Salary;
             }

            return NewTeacher;
        
        }
        
    }
}
