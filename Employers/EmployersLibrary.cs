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
        public List<Person> stuffOfficeEmployers;
        public List<Person> stuffOfficeAbsentEmployers;
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
            stuffOfficeEmployers = new List<Person>();
            var count = stuff.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                if (stuff[i].IsEntered == true)
                {
                    stuffOfficeEmployers.Add(stuff[i]);
                }
            }
            return stuffOfficeEmployers;
        }
        public ICollection<Person> GetListOfAllOfficeAbsentEmployers()
        {
            stuffOfficeAbsentEmployers = new List<Person>();
            var count = stuff.Count;
            for (int i = count - 1; i >= 0; i--)
            {
                if (stuff[i].IsEntered == false)
                {
                    stuffOfficeAbsentEmployers.Add(stuff[i]);
                }
            }
            return stuffOfficeAbsentEmployers;
            //TODO
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
