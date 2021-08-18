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
            var allOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Equal(expectedCount, allOfficeEmployersCollection.Count);
        }


        [Then(@"I validate count of absent office employers collection '(.*)' is '(.*)'")]
        public void ThenIValidateCountOfAbsentOfficeEmployersCollectionIs(string _collection, int expectedCount)
        {
            var absentOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Equal(expectedCount, absentOfficeEmployersCollection.Count);
        }

        [Then(@"I validate collection of all office employers '(.*)' consist of person with last name '(.*)', '(.*)', '(.*)', '(.*)'")]
        public void ThenIValidateCollectionOfAllOfficeEmployersConsistOfPersonWithLastName(string _collection, string firstPerson, string secondPerson, string thirdPerson, string fourthPerson)
        {
            var allOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Collection(allOfficeEmployersCollection, item => Assert.Contains(firstPerson, item.LastName),
                   item => Assert.Contains(secondPerson, item.LastName), item => Assert.Contains(thirdPerson, item.LastName),
                   item => Assert.Contains(fourthPerson, item.LastName));
        }

        [Then(@"I validate collection of stuff office employers '(.*)' consist of person with last name '(.*)', '(.*)', '(.*)', '(.*)'")]
        public void ThenIValidateCollectionOfStuffOfficeEmployersConsistOfPersonWithLastName(string _collection, string firstPerson, string secondPerson, string thirdPerson, string fourthPerson)
        {
            var stuffOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Collection(stuffOfficeEmployersCollection, item => Assert.Contains(firstPerson, item.LastName),
                   item => Assert.Contains(secondPerson, item.LastName), item => Assert.Contains(thirdPerson, item.LastName),
                   item => Assert.Contains(fourthPerson, item.LastName));
        }

        [Then(@"I validate collection of absent office employers '(.*)' is empty")]
        public void ThenIValidateCollectionOfAbsentOfficeEmployersIsEmpty(string _collection)
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


        [When(@"The person with LastName '(.*)' and FirstName '(.*)' come out from the office company '(.*)'")]
        public void WhenThePersonWithLastNameAndFirstNameComeOutFromTheOfficeCompany(string lastName, string firstName, string _company)
        {
            var company = (Company)_scenarioContext[_company];
            var person = company.stuff.Find((element) => (element.LastName == lastName) && (element.FirstName == firstName));
            company.PersonComeOut(person);
        }

        [Then(@"I validate collection of absent office employers '(.*)' consist of person with last name '(.*)'")]
        public void ThenIValidateCollectionOfAbsentOfficeEmployersConsistOfPersonWithLastName(string _collection, string firstPerson)
        {
            var absentOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Collection(absentOfficeEmployersCollection, item => Assert.Contains(firstPerson, item.LastName));
        }

        [Then(@"I validate that collection of absent office employers '(.*)' does not contain persons in office")]
        public void ThenIValidateThatCollectionOfAbsentOfficeEmployersDoesNotContainPersonsInOffice(string _collection)
        {
            var absentOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.DoesNotContain(absentOfficeEmployersCollection, item => item.IsEntered == true);
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

        [When(@"The person with LastName '(.*)' and FirstName '(.*)' come in from the office company '(.*)'")]
        public void WhenThePersonWithLastNameAndFirstNameComeInFromTheOfficeCompany(string lastName, string firstName, string _company)
        {
            var company = (Company)_scenarioContext[_company];
            var person = company.stuff.Find((element) => (element.LastName == lastName) && (element.FirstName == firstName));
            company.PersonComeIn(person);
        }

        [Then(@"I validate collection of absent office employers '(.*)' consist of person with last name '(.*)', '(.*)', '(.*)',")]
        public void ThenIValidateCollectionOfAbsentOfficeEmployersConsistOfPersonWithLastName(string _collection, string firstPerson, string secondPerson, string thirdPerson)
        {
            var absentOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Collection(absentOfficeEmployersCollection, item => Assert.Contains(firstPerson, item.LastName),
                   item => Assert.Contains(secondPerson, item.LastName), item => Assert.Contains(thirdPerson, item.LastName));
        }

        [When(@"I Get List Of Nobody Stuff Office Employers as new Director of company '(.*)' and put it in expected scenario context '(.*)'")]
        public void WhenIGetListOfNobodyStuffOfficeEmployersAsNewDirectorOfCompanyAndPutItInExpectedScenarioContext(string _company, string _NobodyCollection)
        {
            var company = (Company)_scenarioContext[_company];
            var director = new Director(company);
            var NobodyOfficeEmployersCollection = director.GetListOfAllOfficeAbsentEmployers();
            _scenarioContext[_NobodyCollection] = NobodyOfficeEmployersCollection;
        }

        [Then(@"I validate count of Nobody stuff office employers collection '(.*)' is '(.*)'")]
        public void ThenIValidateCountOfNobodyStuffOfficeEmployersCollectionIs(string _collection, int expectedCount)
        {
            var NobodyOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Equal(expectedCount, NobodyOfficeEmployersCollection.Count);
        }

        [Then(@"I validate collection of office employers '(.*)' is empty")]
        public void ThenIValidateCollectionOfOfficeEmployersIsEmpty(string _collection)
        {
            var NobodyOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Empty(NobodyOfficeEmployersCollection);
        }


        [Then(@"I validate collection of office employers '(.*)' consist of person with last name '(.*)', '(.*)', '(.*)'")]
        public void ThenIValidateCollectionOfOfficeEmployersConsistOfPersonWithLastName(string _collection, string firstPerson, string secondPerson, string thirdPerson)
        {
            var allOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Collection(allOfficeEmployersCollection, item => Assert.Contains(firstPerson, item.LastName),
                   item => Assert.Contains(secondPerson, item.LastName), item => Assert.Contains(thirdPerson, item.LastName));
        }

        [Then(@"I validate count of office employers collection '(.*)' is '(.*)'")]
        public void ThenIValidateCountOfOfficeEmployersCollectionIs(string _collection, int expectedCount)
        {
            var OfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Equal(expectedCount, OfficeEmployersCollection.Count);
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
                    var a = (Guid)_scenarioContext[row["ID"]];
                    var m = _scenarioContext[row["ID"]];
                    var b = row["ID"];
                    var c = row;
                    var person = company.stuff.Find((element) => (element.Id == (Guid)_scenarioContext[row["ID"]]));
                    actualCollections.Add(person);
                }
                Assert.Equal(expectedCollections, actualCollections);
            }
        }




    }
}

