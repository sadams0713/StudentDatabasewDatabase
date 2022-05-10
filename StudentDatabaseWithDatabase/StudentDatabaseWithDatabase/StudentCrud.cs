using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDatabaseWithDatabase
{
    public class StudentCrud
    {
        public modelContext studentDB { get; set; }
        public List<Student> students { get; set; }

        public StudentCRUD()
        {
            studentDB = new modelContext();
            students = studentDB.Students.ToList();
        }

        public void Add(string name, string homeTown, string favFood) 
        {
            studentDB.Students.Add(new Student() { Name = name, Hometown = homeTown, FavFood = favFood });
            studentDB.SaveChanges();
            Console.WriteLine("Added student: " + studentDB.Students.ToList().Last().Name + ".");
        }

        public void Remove(int index)
        {
            Console.WriteLine("Removing student: " + studentDB.Students.ToList()[index].Name);
            studentDB.Students.Remove(studentDB.Students.ToList()[index]);
            studentDB.SaveChanges();
        }
    }
}
