using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EmployersLibrary
{
    public class Company
    {
        public List<Person> stuff;

        public void AddPerson(Person p)
        {
            stuff.Add(p);
        }

        public void RemovePerson(Person p)
        {
            stuff.Remove(p);
        }

        public void PersonComeIn(Person p)
        {
            p.IsEntered = true;
        }

        public void PersonComeOut(Person p)
        {
            p.IsEntered = false;
        }

        public ICollection<Person> GetListOfAllEmployers()
        {
            return stuff;
        }
        public ICollection<Person> GetListOfAllOfficeEmployers()
        {
            //TODO
            return null;
        }
        public ICollection<Person> GetListOfAllOfficeAbsentEmployers()
        {
            //TODO
            return null;
        }
    }

    public class Person
    {
        public string FirstName;
        public string LastName;

        public bool IsEntered;
    }

    public class Employer : Person
    {

    }

    public class Director : Person
    {
        public Company firstLineSoftware;
        public ICollection<Person> GetListOfAllOfficeEmployers() => firstLineSoftware.GetListOfAllOfficeEmployers();

        public ICollection<Person> GetListOfAllOfficeAbsentEmployers() => firstLineSoftware.GetListOfAllOfficeAbsentEmployers();

        public ICollection<Person> GetListOfAllEmployers() => firstLineSoftware.GetListOfAllEmployers();
    }
}
