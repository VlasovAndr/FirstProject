using System;
using Xunit;
using System.Collections.Generic;
namespace EmployersLibrary.Test
{
    public class EmployersLibraryTests
    {
        [Fact]
        public void GetStuffOfficeEmployersTest()
        {
            // arrange
            Company firstLine = new Company();
            Person andrewV = new Person() { FirstName = "Andrew", LastName = "Vlasov", IsEntered = true };
            Person andrewI = new Person() { FirstName = "Andrew", LastName = "Ivlev", IsEntered = true };
            Person alexV = new Person() { FirstName = "Alex", LastName = "Vlasov", IsEntered = true };
            Person vovaF = new Person() { FirstName = "Vova", LastName = "Filipov", IsEntered = false };
            firstLine.stuff.Add(andrewV);
            firstLine.stuff.Add(andrewI);
            firstLine.stuff.Add(vovaF);
            firstLine.stuff.Add(alexV);

            //act
            List<Person> actualResult = firstLine.stuffOfficeEmployers;

            //assert
            Assert.Collection(actualResult, item => Assert.Contains("Vlasov", item.LastName),
                           item => Assert.Contains("Ivlev", item.LastName), item => Assert.Contains("Vlasov", item.LastName));
            Assert.Contains(actualResult, item => item.IsEntered == true);
            Assert.Equal(3, actualResult.Count);
            Assert.Equal(4, firstLine.stuff.Count);
            Assert.Collection(firstLine.stuff, item => Assert.Contains("Vlasov", item.LastName),
               item => Assert.Contains("Ivlev", item.LastName), item => Assert.Contains("Filipov", item.LastName),
               item => Assert.Contains("Vlasov", item.LastName));
        }
    }
}
