using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Xunit;
using EmployersLibrary;

namespace SpecFlowProject1.Steps
{
    [Binding]
    public sealed class EmployersStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public EmployersStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("I create company with name '(.*)'")]
        public void ICreateCompanyWithName(string company)
        {
            _scenarioContext[company] = new Company();
        }

        [Given("I add persons to the company with name '(.*)'")]
        public void IAddPersonsToTheCompanyWithName(string _company, Table table)
        {
            var company = (Company)_scenarioContext[_company];

            foreach (var row in table.Rows)
            {
                var person = new Person() { LastName = row["LastName"], FirstName = row["FirstName"] };
                _scenarioContext[row["ID"]] = person.Id;
                company.AddPerson(person);
            }
        }

        [When(@"I Get List Of All Company Employers as new Director of company '(.*)' and put it in actual scenario context '(.*)'")]
        public void WhenIGetListOfAllCompanyEmployersAsNewDirectorOfCompanyAndPutItInActualScenarioContext(string _company, string _allCompanyEmployersCollection)
        {
            var company = (Company)_scenarioContext[_company];
            var director = new Director(company);
            var allCompanyEmployersCollection = director.GetListOfAllEmployers();
            _scenarioContext[_allCompanyEmployersCollection] = allCompanyEmployersCollection;
        }

        [When(@"I Get List Of Stuff Office Employers as new Director of company '(.*)' and put it in expected scenario context '(.*)'")]
        public void WhenIGetListOfStuffOfficeEmployersAsNewDirectorOfCompanyAndPutItInExpectedScenarioContext(string _company, string _stuffCollection)
        {
            var company = (Company)_scenarioContext[_company];
            var director = new Director(company);
            var stuffCollection = director.GetListOfAllOfficeEmployers();
            _scenarioContext[_stuffCollection] = stuffCollection;
        }

        [When(@"I Get List Of Absent Office Employers as new Director of company '(.*)' and put it in expected scenario context '(.*)'")]
        public void WhenIGetListOfAbsentOfficeEmployersAsNewDirectorOfCompanyAndPutItInExpectedScenarioContext(string _company, string _absentCollection)
        {
            var company = (Company)_scenarioContext[_company];
            var director = new Director(company);
            var absentOfficeEmployersCollection = director.GetListOfAllOfficeAbsentEmployers();
            _scenarioContext[_absentCollection] = absentOfficeEmployersCollection;
        }

        [Then(@"I validate count of '(.*)' collection is '(.*)'")]
        public void ThenIValidateCountOfCollectionIs(string _collection, int expectedCount)
        {
            var actualCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Equal(expectedCount, actualCollection.Count);
        }

        [Then(@"I validate collection '(.*)' is empty")]
        public void ThenIValidateCollectionIsEmpty(string _collection)
        {
            var absentOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Empty(absentOfficeEmployersCollection);
        }

        [Then(@"I validate that collection of '(.*)' does not contain absent persons")]
        public void ThenIValidateThatCollectionOfDoesNotContainAbsentPersons(string _collection)
        {
            var stuffOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.DoesNotContain(stuffOfficeEmployersCollection, item => item.IsEntered == false);
        }

        [Then(@"I validate that collection of absent office employers '(.*)' does not contain persons in office")]
        public void ThenIValidateThatCollectionOfAbsentOfficeEmployersDoesNotContainPersonsInOffice(string _collection)
        {
            var absentOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.DoesNotContain(absentOfficeEmployersCollection, item => item.IsEntered == true);
        }

        [When(@"The person with id come out from the office company '(.*)'")]
        public void WhenThePersonWithIdComeOutFromTheOfficeCompany(string _company, Table table)
        {
            var company = (Company)_scenarioContext[_company];
            foreach (var row in table.Rows)
            {
                var person = company.stuff.Find((element) => (element.Id == (Guid)_scenarioContext[row["ID"]]));
                company.PersonComeOut(person);
            }
        }

        [When(@"The person with id come in to the office company '(.*)'")]
        public void WhenThePersonWithIdComeInToTheOfficeCompany(string _company, Table table)
        {
            var company = (Company)_scenarioContext[_company];
            foreach (var row in table.Rows)
            {
                var person = company.stuff.Find((element) => (element.Id == (Guid)_scenarioContext[row["ID"]]));
                company.PersonComeIn(person);
            }
        }

        [When(@"The person with id remove from the company '(.*)'")]
        public void WhenThePersonWithIdRemoveFromTheCompany(string _company, Table table)
        {
            var company = (Company)_scenarioContext[_company];
            foreach (var row in table.Rows)
            {
                var person = company.stuff.Find((element) => (element.Id == (Guid)_scenarioContext[row["ID"]]));
                company.RemovePerson(person);
            }
        }

        [When(@"All person come out from the office company '(.*)'")]
        public void WhenAllPersonComeOutFromTheOfficeCompany(string _company)
        {
            var company = (Company)_scenarioContext[_company];

            foreach (var person in company.stuff)
            {
                company.PersonComeOut(person);
            }
        }

        [Then(@"I validate collection of '(.*)' '(.*)' company consist of person with id")]
        public void ThenIValidateCollectionOfCompanyConsistOfPersonWithId(string _collection, string _company, Table table)
        {
            {
                var company = (Company)_scenarioContext[_company];
                var expectedCollections = (List<Person>)_scenarioContext[_collection];
                var actualCollections = new List<Person>();
                foreach (var row in table.Rows)
                {
                    var person = company.stuff.Find((element) => (element.Id == (Guid)_scenarioContext[row["ID"]]));
                    actualCollections.Add(person);
                }
                Assert.Equal(expectedCollections, actualCollections);
            }
        }
    }
}

