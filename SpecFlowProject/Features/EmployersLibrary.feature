Feature: EmployersLibrary

Background: Set employees in company
	Given I create company with name 'FLS'
	And I add persons to the company with name 'FLS'
		| LastName | FirstName |
		| Vlasov   | Alexei    |
		| Vlasov   | Andrei    |
		| Ivlev    | Andrei    |
		| Filipov  | Vladimir  |

@Alex
Scenario: GetStuffOfficeEmployers_WhenAllPersonInOffice
	When I Get List Of All Company Employers as new Director of company 'FLS' and put it in actual scenario context 'allCompanyEmployers'
	When I Get List Of Stuff Office Employers as new Director of company 'FLS' and put it in expected scenario context 'stuffOfficeEmployers'
	Then I validate count of all office employers collection 'allCompanyEmployers' is '4'
	And I validate collection of all office employers 'allCompanyEmployers' consist of person with last name 'Vlasov', 'Vlasov', 'Ivlev', 'Filipov'
	And I validate count of stuff office employers collection 'stuffOfficeEmployers' is '4'
	And I validate collection of stuff office employers 'stuffOfficeEmployers' consist of person with last name 'Vlasov', 'Vlasov', 'Ivlev', 'Filipov'
	And I validate that collection of stuff office employers 'stuffOfficeEmployers' does not contain absent persons

@Alex
Scenario: GetStuffOfficeEmployers_WhenNobodyPersonInOffice
	When I Get List Of All Company Employers as new Director of company 'FLS' and put it in actual scenario context 'allCompanyEmployers'
	When I Get List Of Nobody Stuff Office Employers as new Director of company 'FLS' and put it in expected scenario context 'NobodyOfficeEmployersInOffice'
	Then I validate count of all office employers collection 'allCompanyEmployers' is '4'
	And I validate collection of all office employers 'allCompanyEmployers' consist of person with last name 'Vlasov', 'Vlasov', 'Ivlev', 'Filipov'
	And I validate count of Nobody stuff office employers collection 'NobodyOfficeEmployersInOffice' is '0'
	And I validate collection of office employers 'NobodyOfficeEmployersInOffice' is empty

@Alex
Scenario: GetStuffOfficeEmployers_WhenOnePersonComeOut
	When The person with LastName 'Ivlev' and FirstName 'Andrei' come out from the office company 'FLS'
	When I Get List Of All Company Employers as new Director of company 'FLS' and put it in actual scenario context 'allCompanyEmployers'
	When I Get List Of Stuff Office Employers as new Director of company 'FLS' and put it in expected scenario context 'stuffOfficeEmployers'
	Then I validate count of all office employers collection 'allCompanyEmployers' is '4'
	And I validate collection of all office employers 'allCompanyEmployers' consist of person with last name 'Vlasov', 'Vlasov', 'Ivlev', 'Filipov'
	And I validate collection of office employers 'stuffOfficeEmployers' consist of person with last name 'Vlasov', 'Vlasov', 'Filipov'
	Then I validate count of office employers collection 'stuffOfficeEmployers' is '3'

@Andrew
Scenario: GetAbsentOfficeEmployers_WhenAllPersonInOffice
	When I Get List Of All Company Employers as new Director of company 'FLS' and put it in actual scenario context 'allCompanyEmployers'
	When I Get List Of Absent Office Employers as new Director of company 'FLS' and put it in expected scenario context 'absentOfficeEmployers'
	Then I validate count of all office employers collection 'allCompanyEmployers' is '4'
	And I validate collection of all office employers 'allCompanyEmployers' consist of person with last name 'Vlasov', 'Vlasov', 'Ivlev', 'Filipov'
	And I validate count of absent office employers collection 'absentOfficeEmployers' is '0'
	And I validate collection of absent office employers 'absentOfficeEmployers' is empty

@Andrew
Scenario: GetAbsentOfficeEmployers_WhenOnePersonComeOut
	When The person with LastName 'Filipov' and FirstName 'Vladimir' come out from the office company 'FLS'
	When I Get List Of All Company Employers as new Director of company 'FLS' and put it in actual scenario context 'allCompanyEmployers'
	When I Get List Of Absent Office Employers as new Director of company 'FLS' and put it in expected scenario context 'absentOfficeEmployers'
	Then I validate count of all office employers collection 'allCompanyEmployers' is '4'
	And I validate collection of all office employers 'allCompanyEmployers' consist of person with last name 'Vlasov', 'Vlasov', 'Ivlev', 'Filipov'
	And I validate count of absent office employers collection 'absentOfficeEmployers' is '1'
	And I validate collection of absent office employers 'absentOfficeEmployers' consist of person with last name 'Filipov'
	And I validate that collection of absent office employers 'absentOfficeEmployers' does not contain persons in office

@Andrew
Scenario: GetAbsentOfficeEmployers_WhenAllPersonComeOutAndOneComeIn
	When All person come out from the office company 'FLS'
	When The person with LastName 'Ivlev' and FirstName 'Andrei' come in from the office company 'FLS'
	When I Get List Of All Company Employers as new Director of company 'FLS' and put it in actual scenario context 'allCompanyEmployers'
	When I Get List Of Absent Office Employers as new Director of company 'FLS' and put it in expected scenario context 'absentOfficeEmployers'
	Then I validate count of all office employers collection 'allCompanyEmployers' is '4'
	And I validate collection of all office employers 'allCompanyEmployers' consist of person with last name 'Vlasov', 'Vlasov', 'Ivlev', 'Filipov'
	And I validate count of absent office employers collection 'absentOfficeEmployers' is '3'
	And I validate collection of absent office employers 'absentOfficeEmployers' consist of person with last name 'Vlasov', 'Vlasov', 'Filipov',
	And I validate that collection of absent office employers 'absentOfficeEmployers' does not contain persons in office