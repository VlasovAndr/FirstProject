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

        [When("I Get List Of All Office Employers as new Director of company '(.*)' and put it in scenario context '(.*)'")]
        public void IGetListOfAllOfficeEmploytersAsNewDirectorOfCompanyAndPutItInScenarioContext(string _company, string _stuffCollection)
        {
            var company = (Company)_scenarioContext[_company];
            var director = new Director(company);
            var stuffCollection = director.GetListOfAllOfficeEmployers();
            _scenarioContext[_stuffCollection] = stuffCollection;
        }

        [Then("I validate count of collection '(.*)' is '(.*)'")]
        public void ThenIValidateCountOfCollection(string _collection, int expectedCount)
        {
            var collection = (List<Person>)_scenarioContext[_collection];
            Assert.Equal(expectedCount, collection.Count);
        }


    }
}

