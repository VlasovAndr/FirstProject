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
    public sealed class CalculatorStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public CalculatorStepDefinitions(ScenarioContext scenarioContext)
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
            var allCompanyEmployersCollection = director.GetListOfAllOfficeEmployers();
            _scenarioContext[_allCompanyEmployersCollection] = allCompanyEmployersCollection;
        }

        [When(@"I Get List Of Stuff Office Employers as new Director of company '(.*)' and put it in expected scenario context '(.*)'")]
        public void WhenIGetListOfStuffOfficeEmployersAsNewDirectorOfCompanyAndPutItInExpectedScenarioContext(string _company, string _stuffCollection)
        {
            var company = (Company)_scenarioContext[_company];
            var director = new Director(company);
            var stuffCollection = director.GetListOfAllEmployers();
            _scenarioContext[_stuffCollection] = stuffCollection;
        }

        [Then(@"I validate count of all office employers collection '(.*)' is '(.*)'")]
        public void ThenIValidateCountOfAllOfficeEmployersCollectionIs(string _collection, int expectedCount)
        {
            var allOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Equal(expectedCount, allOfficeEmployersCollection.Count);
        }

        [Then(@"I validate count of stuff office employers collection '(.*)' is '(.*)'")]
        public void ThenIValidateCountOfStuffOfficeEmployersCollectionIs(string _collection, int expectedCount)
        {
            var stuffOfficeEmployersCollection = (List<Person>)_scenarioContext[_collection];
            Assert.Equal(expectedCount, stuffOfficeEmployersCollection.Count);
        }
    }
}

