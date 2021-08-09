using System;
using Xunit;
using System.Collections.Generic;
namespace EmployersLibrary.Test
{
    public class EmployersLibraryTests
    {
        [Fact]
        public void GetStuffOfficeEmployers_WhenAllPersonInOffice()
        {
            // arrange
            Company FLS = new Company();
            Person alexV = new Person() { LastName = "Vlasov", FirstName = "Alexei" };
            Person andreyI = new Person() { LastName = "Ivlev", FirstName = "Andrei" };
            Person andreyV = new Person() { LastName = "Vlasov", FirstName = "Andrei" };
            Person vladimirF = new Person() { LastName = "Filipov", FirstName = "Vladimir" };
            FLS.stuff = new List<Person>();
            FLS.stuff.Add(vladimirF);
            FLS.stuff.Add(alexV);
            FLS.stuff.Add(andreyI);
            FLS.stuff.Add(andreyV);

            //act
            List<Person> actualResult = FLS.stuffOfficeEmployers;

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
            Person alexV = new Person() { LastName = "Vlasov", FirstName = "Alexei" };
            Person andreyI = new Person() { LastName = "Ivlev", FirstName = "Andrei" };
            Person andreyV = new Person() { LastName = "Vlasov", FirstName = "Andrei" };
            Person vladimirF = new Person() { LastName = "Filipov", FirstName = "Vladimir" };
            FLS.stuff = new List<Person>();
            FLS.stuff.Add(vladimirF);
            FLS.stuff.Add(alexV);
            FLS.stuff.Add(andreyI);
            FLS.stuff.Add(andreyV);

            //act
            FLS.PersonComeOut(vladimirF);
            FLS.PersonComeOut(alexV);
            FLS.PersonComeOut(andreyI);
            FLS.PersonComeOut(andreyV);
            List<Person> actualResult = FLS.stuffOfficeEmployers;

            // assert

            Assert.Equal(0, actualResult.Count);
            Assert.Equal(4, FLS.stuff.Count);
        }

        [Fact]
        public void GetStuffOfficeEmployers_WhenOnePersonComeOut()
        {
            // arrange
            Company FLS = new Company();
            Person alexV = new Person() { LastName = "Vlasov", FirstName = "Alexei" };
            Person andreyI = new Person() { LastName = "Ivlev", FirstName = "Andrei" };
            Person andreyV = new Person() { LastName = "Vlasov", FirstName = "Andrei" };
            Person vladimirF = new Person() { LastName = "Filipov", FirstName = "Vladimir" };
            FLS.stuff = new List<Person>();
            FLS.stuff.Add(vladimirF);
            FLS.stuff.Add(alexV);
            FLS.stuff.Add(andreyI);
            FLS.stuff.Add(andreyV);

            // act
            FLS.PersonComeOut(andreyI);
            List<Person> actualResult = FLS.stuffOfficeEmployers;

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
            Person alexV = new Person() { LastName = "Vlasov", FirstName = "Alexei" };
            Person andreyI = new Person() { LastName = "Ivlev", FirstName = "Andrei" };
            Person andreyV = new Person() { LastName = "Vlasov", FirstName = "Andrei" };
            Person vladimirF = new Person() { LastName = "Filipov", FirstName = "Vladimir" };
            FLS.stuff = new List<Person>();
            FLS.stuff.Add(vladimirF);
            FLS.stuff.Add(alexV);
            FLS.stuff.Add(andreyI);
            FLS.stuff.Add(andreyV);

            // act
            FLS.PersonComeOut(alexV);
            FLS.PersonComeIn(alexV);
            List<Person> actualResult = FLS.stuffOfficeEmployers;

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
            Person alexV = new Person() { LastName = "Vlasov", FirstName = "Alexei" };
            Person andreyI = new Person() { LastName = "Ivlev", FirstName = "Andrei" };
            Person andreyV = new Person() { LastName = "Vlasov", FirstName = "Andrei" };
            Person vladimirF = new Person() { LastName = "Filipov", FirstName = "Vladimir" };
            FLS.stuff = new List<Person>();
            FLS.stuff.Add(vladimirF);
            FLS.stuff.Add(alexV);
            FLS.stuff.Add(andreyI);
            FLS.stuff.Add(andreyV);

            // act
            FLS.PersonComeOut(andreyV);
            FLS.PersonComeOut(alexV);
            FLS.PersonComeIn(alexV);
            List<Person> actualResult = FLS.stuffOfficeEmployers;

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
            Person alexV = new Person() { LastName = "Vlasov", FirstName = "Alexei" };
            Person andreyI = new Person() { LastName = "Ivlev", FirstName = "Andrei" };
            Person andreyV = new Person() { LastName = "Vlasov", FirstName = "Andrei" };
            Person vladimirF = new Person() { LastName = "Filipov", FirstName = "Vladimir" };
            FLS.stuff = new List<Person>();
            FLS.stuff.Add(vladimirF);
            FLS.stuff.Add(alexV);
            FLS.stuff.Add(andreyI);
            FLS.stuff.Add(andreyV);

            // act
            FLS.PersonComeOut(andreyV);
            FLS.PersonComeOut(alexV);
            FLS.PersonComeOut(vladimirF);
            FLS.PersonComeOut(andreyI);
            FLS.PersonComeIn(alexV);
            List<Person> actualResult = FLS.stuffOfficeEmployers;

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
        public void GetAbsentOfficeEmployers_WhenAllPersonInOffice()
        {
            // arrange
            Company FLS = new Company();
            Person alex = new Person() { LastName = "Vlasov", FirstName = "Alexei" };
            Person andrew = new Person() { LastName = "Ivlev", FirstName = "Andrew" };
            Person andrei = new Person() { LastName = "Vlasov", FirstName = "Andrei" };
            Person pavel = new Person() { LastName = "Ivanov", FirstName = "Pavel" };
            FLS.stuff = new List<Person>();
            FLS.stuff.Add(alex);
            FLS.stuff.Add(andrei);
            FLS.stuff.Add(andrew);
            FLS.stuff.Add(pavel);

            // act
            List<Person> actualResult = FLS.stuffOfficeAbsentEmployers;

            // assert
            Assert.Equal(0, actualResult.Count);
            Assert.Equal(4, FLS.stuff.Count);
        }

        [Fact]
        public void GetAbsentOfficeEmployers_WhenOnePersonComeOut()
        {
            // arrange
            Company FLS = new Company();
            Person alex = new Person() { LastName = "Vlasov", FirstName = "Alexei" };
            Person andrew = new Person() { LastName = "Ivlev", FirstName = "Andrew" };
            Person andrei = new Person() { LastName = "Vlasov", FirstName = "Andrei" };
            Person pavel = new Person() { LastName = "Ivanov", FirstName = "Pavel" };
            FLS.stuff = new List<Person>();
            FLS.stuff.Add(alex);
            FLS.stuff.Add(andrew);
            FLS.stuff.Add(andrei);
            FLS.stuff.Add(pavel);

            // act
            FLS.PersonComeOut(pavel);
            List<Person> actualResult = FLS.stuffOfficeAbsentEmployers;


            // assert
            Assert.Collection(FLS.stuff, item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivlev", item.LastName),
             item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivanov", item.LastName));
            Assert.Contains(actualResult, item => item.IsEntered == false);
            Assert.Equal(1, actualResult.Count);

        }
        [Fact]
        public void GetAbsentOfficeEmployers_WhenPersonComeOutAndComeIn()
        {
            // arrange
            Company FLS = new Company();
            Person pavel = new Person() { LastName = "Ivanov", FirstName = "Pavel" };
            Person andrew = new Person() { LastName = "Ivlev", FirstName = "Andrew" };
            Person andrei = new Person() { LastName = "Vlasov", FirstName = "Andrei" };
            Person alex = new Person() { LastName = "Vlasov", FirstName = "Alexei" };
            FLS.stuff = new List<Person>();
            FLS.stuff.Add(pavel);
            FLS.stuff.Add(andrei);
            FLS.stuff.Add(andrew);
            FLS.stuff.Add(alex);

            // act
            FLS.PersonComeOut(pavel);
            FLS.PersonComeOut(andrew);
            FLS.PersonComeOut(andrei);
            FLS.PersonComeOut(alex);
            FLS.PersonComeIn(pavel);
            List<Person> actualResult = FLS.stuffOfficeAbsentEmployers;

            //assert
            Assert.Collection(FLS.stuff, item => Assert.Contains("Ivanov", item.LastName), item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivlev", item.LastName),
            item => Assert.Contains("Vlasov", item.LastName));
            Assert.DoesNotContain(actualResult, item => item.IsEntered == true);
            Assert.Equal(3, actualResult.Count);

        }

        [Fact]
        public void GetAbsentOfficeEmployers_WhenPersonComeOutAndComeInAndComeOut()
        {
            // arrange
            Company FLS = new Company();
            Person pavel = new Person() { LastName = "Ivanov", FirstName = "Pavel" };
            Person andrew = new Person() { LastName = "Ivlev", FirstName = "Andrew" };
            Person andrei = new Person() { LastName = "Vlasov", FirstName = "Andrei" };
            Person alex = new Person() { LastName = "Vlasov", FirstName = "Alexei" };
            FLS.stuff = new List<Person>();
            FLS.stuff.Add(pavel);
            FLS.stuff.Add(andrei);
            FLS.stuff.Add(andrew);
            FLS.stuff.Add(alex);

            // act

            FLS.PersonComeOut(pavel);
            FLS.PersonComeOut(andrew);
            FLS.PersonComeOut(andrei);
            FLS.PersonComeOut(alex);
            FLS.PersonComeIn(pavel);
            FLS.PersonComeIn(andrew);
            FLS.PersonComeIn(andrei);
            FLS.PersonComeOut(pavel);
            FLS.PersonComeOut(andrew);
            FLS.PersonComeOut(andrei);
            List<Person> actualResult = FLS.stuffOfficeAbsentEmployers;

            //assert
            Assert.Collection(FLS.stuff, item => Assert.Contains("Ivanov", item.LastName), item => Assert.Contains("Vlasov", item.LastName), item => Assert.Contains("Ivlev", item.LastName),
            item => Assert.Contains("Vlasov", item.LastName));
            Assert.Contains(actualResult, item => item.IsEntered == false);
            Assert.Equal(4, actualResult.Count);

        }

        [Fact]
        public void GetAbsentOfficeEmployers_WhenPersonRemoveFromOffice()
        {
            // arrange
            Company FLS = new Company();
            Person pavel = new Person() { LastName = "Ivanov", FirstName = "Pavel" };
            Person alex = new Person() { LastName = "Vlasov", FirstName = "Alexei" };
            Person andrew = new Person() { LastName = "Ivlev", FirstName = "Andrew" };
            Person andrei = new Person() { LastName = "Vlasov", FirstName = "Andrei" };


            FLS.stuff = new List<Person>();
            FLS.stuff.Add(pavel);
            FLS.stuff.Add(alex);
            FLS.stuff.Add(andrew);
            FLS.stuff.Add(andrei);

            // act
            FLS.RemovePerson(pavel);
            FLS.RemovePerson(alex);
            FLS.PersonComeOut(andrew);
            FLS.PersonComeOut(andrei);

            List<Person> actualResult = FLS.stuffOfficeAbsentEmployers;

            //assert
            Assert.Collection(FLS.stuff, item => Assert.Contains("Ivlev", item.LastName), item => Assert.Contains("Vlasov", item.LastName));
            Assert.Contains(actualResult, item => item.IsEntered == false);
            Assert.Equal(2, actualResult.Count);

        }
    }
}
