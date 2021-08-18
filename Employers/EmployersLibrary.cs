using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployersLibrary
{
    public class Company
    {
        public List<Person> stuff;
        public List<Person> stuffOfficeEmployers
        {
            get
            {
                var stuffOfficeEmployers = new List<Person>();
                var count = stuff.Count;
                for (int i = 0; i < count; i++)
                {
                    if (stuff[i].IsEntered == true)
                    {
                        stuffOfficeEmployers.Add(stuff[i]);
                    }
                }
                return stuffOfficeEmployers;
            }
        }
        public List<Person> stuffOfficeAbsentEmployers
        {
            get
            {
                List<Person> stuffOfficeAbsentEmployers = new List<Person>();
                stuffOfficeAbsentEmployers = stuff.FindAll(item => item.IsEntered == false);
                return stuffOfficeAbsentEmployers;
            }
        }

        public Company()
        {
            this.stuff = new List<Person>();
        }
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

    }

    public class Person
    {
        public string FirstName;
        public string LastName;
        public bool IsEntered;
        public Guid Id;
        public Person()
        {
            this.IsEntered = true;
            //this.Id = new Guid();
            this.Id = Guid.NewGuid();
        }
    }

    public class Employer : Person
    {

    }

    public class Director : Person
    {
        public Company company;

        public Director(Company company)
        {
            this.company = company;
        }

        public ICollection<Person> GetListOfAllOfficeEmployers() => company.stuffOfficeEmployers;
        public ICollection<Person> GetListOfAllOfficeAbsentEmployers() => company.stuffOfficeAbsentEmployers;
        public ICollection<Person> GetListOfAllEmployers() => company.stuff;
    }
}
