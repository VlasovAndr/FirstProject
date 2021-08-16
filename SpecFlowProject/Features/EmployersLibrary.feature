Feature: EmployersLibrary

Background: Set employees in company
	Given I create company with name 'FLS'
	And I add persons to the company with name 'FLS'
		| LastName | FirstName |
		| Vlasov   | Alexei    |
		| Vlasov   | Andrei    |
		| Ivlev    | Andrei    |
		| Filipov  | Vladimir  |

		
Scenario: GetStuffOfficeEmployers_WhenAllPersonInOffice
	When I Get List Of All Company Employers as new Director of company 'FLS' and put it in actual scenario context 'allCompanyEmployersResult'
	When I Get List Of Stuff Office Employers as new Director of company 'FLS' and put it in expected scenario context 'stuffOfficeEmployersResult'
	Then I validate count of all office employers collection 'allCompanyEmployersResult' is '4'
	And I validate count of stuff office employers collection 'stuffOfficeEmployersResult' is '4'