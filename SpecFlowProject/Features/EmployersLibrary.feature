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
	When I Get List Of All Office Employers as new Director of company 'FLS' and put it in scenario context 'actualResult'
	When I Get List Of All Office Employers as new Director of company 'FLS' and put it in scenario context 'FLS.stuff'
	Then I validate count of collection 'actualResult' is '4'
	And I validate count of collection 'FLS.stuff' is '4'