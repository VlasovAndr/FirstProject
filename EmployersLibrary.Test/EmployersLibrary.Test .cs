using System;
using Xunit;
using System.Collections.Generic;
namespace EmployersLibrary.Test
{
    public class EmployersLibraryTests
    {
        List<Person> stuffCollection;
        Person alexV = new Person() { LastName = "Vlasov", FirstName = "Alexei" };
        Person andreyI = new Person() { LastName = "Ivlev", FirstName = "Andrei" };
        Person andreyV = new Person() { LastName = "Vlasov", FirstName = "Andrei" };
        Person vladimirF = new Person() { LastName = "Filipov", FirstName = "Vladimir" };
        public EmployersLibraryTests()
        {
            stuffCollection = new List<Person>();
            stuffCollection.Add(vladimirF);
            stuffCollection.Add(alexV);
            stuffCollection.Add(andreyI);
            stuffCollection.Add(andreyV);
        }

        [Fact]
        public void GetStuffOfficeEmployers_WhenAllPersonInOffice()
        {
            // arrange
            Company FLS = new Company();
            Director director = new Director(FLS);
            FLS.stuff = stuffCollection;

            //act
            var actualResult = director.GetListOfAllOfficeEmployers();

            // assert
            Assert.Collection(actualResult, item => Assert.Contains("Filipov", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivlev", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName));
            Assert.DoesNotContain(actualResult, item => item.IsEntered == false);
            Assert.Equal(4, actualResult.Count);
            Assert.Equal(4, FLS.stuff.Count);
            Assert.Collection(FLS.stuff, item => Assert.Contains("Filipov", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivlev", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName));
        }

        [Fact]
        public void GetStuffOfficeEmployers_WhenNobodyPersonInOffice()
        {
            // arrange
            Company FLS = new Company();
            Director director = new Director(FLS);
            FLS.stuff = stuffCollection;

            //act
            FLS.PersonComeOut(vladimirF);
            FLS.PersonComeOut(alexV);
            FLS.PersonComeOut(andreyI);
            FLS.PersonComeOut(andreyV);
            var actualResult = director.GetListOfAllOfficeEmployers();

            // assert
            Assert.Empty(actualResult);
            Assert.Equal(0, actualResult.Count);
            Assert.Equal(4, FLS.stuff.Count);
        }

        [Fact]
        public void GetStuffOfficeEmployers_WhenOnePersonComeOut()
        {
            // arrange
            Company FLS = new Company();
            Director director = new Director(FLS);
            FLS.stuff = stuffCollection;

            // act
            FLS.PersonComeOut(andreyI);
            var actualResult = director.GetListOfAllOfficeEmployers();

            // assert
            Assert.Collection(actualResult, item => Assert.Contains("Filipov", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Vlasov", item.LastName));
            Assert.DoesNotContain(actualResult, item => item.IsEntered == false);
            Assert.Equal(3, actualResult.Count);
            Assert.Equal(4, FLS.stuff.Count);
            Assert.Collection(FLS.stuff, item => Assert.Contains("Filipov", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivlev", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName));
        }

        [Fact]
        public void GetStuffOfficeEmployers_WhenOnePersonComeOutAndComeIn()
        {
            // arrange
            Company FLS = new Company();
            Director director = new Director(FLS);
            FLS.stuff = stuffCollection;

            // act
            FLS.PersonComeOut(alexV);
            FLS.PersonComeIn(alexV);
            var actualResult = director.GetListOfAllOfficeEmployers();

            // assert
            Assert.Collection(actualResult, item => Assert.Contains("Filipov", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivlev", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName));
            Assert.DoesNotContain(actualResult, item => item.IsEntered == false);
            Assert.Equal(4, actualResult.Count);
            Assert.Equal(4, FLS.stuff.Count);
            Assert.Collection(FLS.stuff, item => Assert.Contains("Filipov", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivlev", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName));
        }

        [Fact]
        public void GetStuffOfficeEmployers_WhenDifferentPersonComeOutAndComeIn()
        {
            // arrange
            Company FLS = new Company();
            Director director = new Director(FLS);
            FLS.stuff = stuffCollection;

            // act
            FLS.PersonComeOut(andreyV);
            FLS.PersonComeOut(alexV);
            FLS.PersonComeIn(alexV);
            var actualResult = director.GetListOfAllOfficeEmployers();

            // assert
            Assert.Collection(actualResult, item => Assert.Contains("Filipov", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivlev", item.LastName));
            Assert.DoesNotContain(actualResult, item => item.IsEntered == false);
            Assert.Equal(3, actualResult.Count);
            Assert.Equal(4, FLS.stuff.Count);
            Assert.Collection(FLS.stuff, item => Assert.Contains("Filipov", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivlev", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName));
        }

        [Fact]
        public void GetStuffOfficeEmployers_WhenAllPersonComeOutAndOneComeIn()
        {
            // arrange
            Company FLS = new Company();
            Director director = new Director(FLS);
            FLS.stuff = stuffCollection;

            // act
            FLS.PersonComeOut(andreyV);
            FLS.PersonComeOut(alexV);
            FLS.PersonComeOut(vladimirF);
            FLS.PersonComeOut(andreyI);
            FLS.PersonComeIn(alexV);
            var actualResult = director.GetListOfAllOfficeEmployers();

            // assert
            Assert.Collection(actualResult, item => Assert.Contains("Vlasov", item.LastName));
            Assert.DoesNotContain(actualResult, item => item.IsEntered == false);
            Assert.Equal(1, actualResult.Count);
            Assert.Equal(4, FLS.stuff.Count);
            Assert.Collection(FLS.stuff, item => Assert.Contains("Filipov", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivlev", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName));
        }

        [Fact]
        public void GetStuffOfficeEmployers_WhenPersonRemoveFromOffice()
        {
            // arrange
            Company FLS = new Company();
            Director director = new Director(FLS);
            FLS.stuff = stuffCollection;

            // act
            FLS.RemovePerson(vladimirF);
            var actualResult = director.GetListOfAllOfficeEmployers();

            // assert
            Assert.Collection(actualResult, item => Assert.Contains("Vlasov", item.LastName),
                           item => Assert.Contains("Ivlev", item.LastName), item => Assert.Contains("Vlasov", item.LastName));
            Assert.DoesNotContain(actualResult, item => item.IsEntered == false);
            Assert.Equal(3, actualResult.Count);
            Assert.Equal(3, FLS.stuff.Count);
            Assert.Collection(FLS.stuff, item => Assert.Contains("Vlasov", item.LastName),
                           item => Assert.Contains("Ivlev", item.LastName), item => Assert.Contains("Vlasov", item.LastName));
        }

        [Fact]
        public void GetStuffOfficeEmployers_WhenAllPersonComeOutAndComeIn()
        {
            // arrange
            Company FLS = new Company();
            Director director = new Director(FLS);
            FLS.stuff = stuffCollection;

            //act
            FLS.PersonComeOut(vladimirF);
            FLS.PersonComeOut(andreyI);
            FLS.PersonComeOut(alexV);
            FLS.PersonComeOut(andreyV);
            FLS.PersonComeIn(andreyI);
            FLS.PersonComeIn(vladimirF);
            FLS.PersonComeIn(alexV);
            FLS.PersonComeIn(andreyV);
            var actualResult = director.GetListOfAllOfficeEmployers();

            // assert
            Assert.Collection(actualResult, item => Assert.Contains("Filipov", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivlev", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName));
            Assert.DoesNotContain(actualResult, item => item.IsEntered == false);
            Assert.Equal(4, actualResult.Count);
            Assert.Equal(4, FLS.stuff.Count);
            Assert.Collection(FLS.stuff, item => Assert.Contains("Filipov", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivlev", item.LastName),
                           item => Assert.Contains("Vlasov", item.LastName));
        }

        [Fact]
        public void GetStuffOfficeEmployers_WhenCompanyIsEmpty()
        {
            // arrange
            Company FLS = new Company();
            Director director = new Director(FLS);
            FLS.stuff = stuffCollection;

            //act
            FLS.RemovePerson(vladimirF);
            FLS.RemovePerson(andreyI);
            FLS.RemovePerson(alexV);
            FLS.RemovePerson(andreyV);
            var actualResult = director.GetListOfAllOfficeEmployers();

            // assert
            Assert.Empty(actualResult);
            Assert.Empty(FLS.stuff);
            Assert.Equal(0, actualResult.Count);
            Assert.Equal(0, FLS.stuff.Count);
        }

        [Fact]
        public void GetAbsentOfficeEmployers_WhenAllPersonInOffice()
        {
            // arrange
            Company FLS = new Company();
            Director director = new Director(FLS);
            FLS.stuff = stuffCollection;

            // act
            var actualResult = director.GetListOfAllOfficeAbsentEmployers();

            // assert
            Assert.Equal(0, actualResult.Count);
            Assert.Equal(4, FLS.stuff.Count);
            Assert.Collection(FLS.stuff, item => Assert.Contains("Filipov", item.LastName),item => Assert.Contains("Vlasov", item.LastName),
                item => Assert.Contains("Ivlev", item.LastName), item => Assert.Contains("Vlasov", item.LastName));
        }

        [Fact]
        public void GetAbsentOfficeEmployers_WhenOnePersonComeOut()
        {
            // arrange
            Company FLS = new Company();
            Director director = new Director(FLS);
            FLS.stuff = stuffCollection;
           
            // act
            FLS.PersonComeOut(vladimirF);
            var actualResult = director.GetListOfAllOfficeAbsentEmployers();

            // assert
            Assert.Collection(actualResult, item => Assert.Contains("Filipov", item.LastName));
            Assert.Contains(actualResult, item => item.IsEntered == false);
            Assert.Equal(1, actualResult.Count);
            Assert.Equal(4, FLS.stuff.Count);
            Assert.Collection(FLS.stuff, item => Assert.Contains("Filipov", item.LastName), item => Assert.Contains("Vlasov", item.LastName),
                 item => Assert.Contains("Ivlev", item.LastName), item => Assert.Contains("Vlasov", item.LastName));

        }
        [Fact]
        public void GetAbsentOfficeEmployers_WhenPersonComeOutAndComeIn()
        {
            // arrange
            Company FLS = new Company();
            Director director = new Director(FLS);
            FLS.stuff = stuffCollection;

            // act
            FLS.PersonComeOut(vladimirF);
            FLS.PersonComeOut(andreyI);
            FLS.PersonComeOut(andreyV);
            FLS.PersonComeOut(alexV);
            FLS.PersonComeIn(vladimirF);
            var actualResult = director.GetListOfAllOfficeAbsentEmployers();

            //assert
            Assert.Collection(actualResult, item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivlev", item.LastName),
              item => Assert.Contains("Vlasov", item.LastName));
            Assert.DoesNotContain(actualResult, item => item.IsEntered == true);
            Assert.Equal(3, actualResult.Count);
            Assert.Equal(4, FLS.stuff.Count);
            Assert.Collection(FLS.stuff, item => Assert.Contains("Filipov", item.LastName), item => Assert.Contains("Vlasov", item.LastName),
                  item => Assert.Contains("Ivlev", item.LastName), item => Assert.Contains("Vlasov", item.LastName));
        }

        [Fact]
        public void GetAbsentOfficeEmployers_WhenPersonComeOutAndComeInAndComeOut()
        {
            // arrange
            Company FLS = new Company();
            Director director = new Director(FLS);
            FLS.stuff = stuffCollection;

            // act
            FLS.PersonComeOut(vladimirF);
            FLS.PersonComeOut(andreyV);
            FLS.PersonComeOut(andreyI);
            FLS.PersonComeOut(alexV);
            FLS.PersonComeIn(vladimirF);
            FLS.PersonComeIn(andreyV);
            FLS.PersonComeIn(andreyI);
            FLS.PersonComeOut(vladimirF);
            FLS.PersonComeOut(andreyV);
            FLS.PersonComeOut(andreyI);
            var actualResult = director.GetListOfAllOfficeAbsentEmployers();

            //assert
            Assert.Collection(actualResult, item => Assert.Contains("Filipov", item.LastName), item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivlev", item.LastName),
            item => Assert.Contains("Vlasov", item.LastName));
            Assert.Contains(actualResult, item => item.IsEntered == false);
            Assert.Equal(4, actualResult.Count);
            Assert.Equal(4, FLS.stuff.Count);
            Assert.Collection(FLS.stuff, item => Assert.Contains("Filipov", item.LastName), item => Assert.Contains("Vlasov", item.LastName),
                item => Assert.Contains("Ivlev", item.LastName), item => Assert.Contains("Vlasov", item.LastName));
        }

        [Fact]
        public void GetAbsentOfficeEmployers_WhenPersonRemoveFromOffice()
        {
            // arrange
            Company FLS = new Company();
            Director director = new Director(FLS);
            FLS.stuff = stuffCollection;

            // act
            FLS.RemovePerson(vladimirF);
            var actualResult = director.GetListOfAllOfficeAbsentEmployers();

            //assert
            Assert.DoesNotContain(actualResult, item => item.IsEntered == true);
            Assert.Equal(0, actualResult.Count);
            Assert.Equal(3, FLS.stuff.Count);
            Assert.Collection(FLS.stuff, item => Assert.Contains("Vlasov", item.LastName),
               item => Assert.Contains("Ivlev", item.LastName), item => Assert.Contains("Vlasov", item.LastName));
        }
    }
}
