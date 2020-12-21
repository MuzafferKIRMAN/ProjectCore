using ProjectCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCore.Data
{
    public class SanalData
    {
        public List<User> users = new List<User>
        {
             new User{Id=1,UserName="Name1",UserPassword="123",UserRole=1},
                new User{Id=2,UserName="Name2",UserPassword="987",UserRole=2}
        };

        public List<Student> students = new List<Student>
        {
             new Student{Id=1,Class="10 F",StudentName="Ahmet",StudentSurname="Çalışkan",StudentNumber=1 },
                new Student{Id=2,Class="10 E",StudentName="Ayşe",StudentSurname="Zeki",StudentNumber=2}
        };
    }
}
