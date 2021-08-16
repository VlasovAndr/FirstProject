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

        [Then(@"I validate count of all office employers collection '(.*)' is '(.*)'")]
        public void ThenIValidateCountOfAllOfficeEmployersCollectionIs(string _collection, int expectedCount)
        {
            var allOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Equal(expectedCount, allOfficeEmployersCollection.Count);
        }

        [Then(@"I validate collection of all office office employers '(.*)' consist of person with last name '(.*)', '(.*)', '(.*)', '(.*)'")]
        public void ThenIValidateCollectionOfAllOfficeOfficeEmployersConsistOfPersonWithLastName(string _collection, string firstPerson, string secondPerson, string thirdPerson, string fourthPerson)
        {
            var allOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Collection(allOfficeEmployersCollection, item => Assert.Contains(firstPerson, item.LastName),
                   item => Assert.Contains(secondPerson, item.LastName), item => Assert.Contains(thirdPerson, item.LastName),
                   item => Assert.Contains(fourthPerson, item.LastName));
        }

        [Then(@"I validate count of stuff office employers collection '(.*)' is '(.*)'")]
        public void ThenIValidateCountOfStuffOfficeEmployersCollectionIs(string _collection, int expectedCount)
        {
            var stuffOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Equal(expectedCount, stuffOfficeEmployersCollection.Count);
        }

        [Then(@"I validate collection of stuff office employers '(.*)' consist of person with last name '(.*)', '(.*)', '(.*)', '(.*)'")]
        public void ThenIValidateCollectionOfStuffOfficeEmployersConsistOfPersonWithLastName(string _collection, string firstPerson, string secondPerson, string thirdPerson, string fourthPerson)
        {
            var stuffOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Collection(stuffOfficeEmployersCollection, item => Assert.Contains(firstPerson, item.LastName),
                   item => Assert.Contains(secondPerson, item.LastName), item => Assert.Contains(thirdPerson, item.LastName),
                   item => Assert.Contains(fourthPerson, item.LastName));
        }

        [Then(@"I validate that collection of stuff office employers '(.*)' does not contain absent persons")]
        public void ThenIValidateThatCollectionOfStuffOfficeEmployersDoesNotContainAbsentPersons(string _collection)
        {
            var stuffOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.DoesNotContain(stuffOfficeEmployersCollection, item => item.IsEntered == false);
        }

        [Then(@"I validate that collection of stuff office employers '(.*)' '(.*)' absent persons")]
        public void ThenIValidateThatCollectionOfStuffOfficeEmployersAbsentPersons(string _collection, string _isAbsent)
        {
            var stuffOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            _scenarioContext[_isAbsent] = false;
            var absent = (bool)_scenarioContext[_isAbsent];
            Assert.DoesNotContain(stuffOfficeEmployersCollection, item => item.IsEntered == absent);
        }

    }
}

