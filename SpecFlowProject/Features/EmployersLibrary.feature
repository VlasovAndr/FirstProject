Feature: EmployersLibrary

Background: Set employees in company
	Given I create company with name 'FLS'
	And I add persons to the company with name 'FLS'
		| LastName | FirstName | ID          |
		| Vlasov   | Alexei    | IdOfPerson1 |
		| Vlasov   | Andrei    | IdOfPerson2 |
		| Ivlev    | Andrei    | IdOfPerson3 |
		| Filipov  | Vladimir  | IdOfPerson4 |

@Alex
Scenario: GetStuffOfficeEmployers_WhenAllPersonInOffice
	When I Get List Of All Company Employers as new Director of company 'FLS' and put it in actual scenario context 'allCompanyEmployers'
	When I Get List Of Stuff Office Employers as new Director of company 'FLS' and put it in expected scenario context 'stuffOfficeEmployers'
	Then I validate count of 'allCompanyEmployers' collection is '4'
	And I validate collection of 'allCompanyEmployers' 'FLS' company consist of person with id
		| ID          |
		| IdOfPerson1 |
		| IdOfPerson2 |
		| IdOfPerson3 |
		| IdOfPerson4 |
	And I validate count of 'stuffOfficeEmployers' collection is '4'
	And I validate collection of 'stuffOfficeEmployers' 'FLS' company consist of person with id
		| ID          |
		| IdOfPerson1 |
		| IdOfPerson2 |
		| IdOfPerson3 |
		| IdOfPerson4 |
	And I validate that collection of 'stuffOfficeEmployers' does not contain absent persons

@Alex
Scenario: GetStuffOfficeEmployers_WhenNobodyPersonInOffice
	When All person come out from the office company 'FLS'
	When I Get List Of All Company Employers as new Director of company 'FLS' and put it in actual scenario context 'allCompanyEmployers'
	When I Get List Of Stuff Office Employers as new Director of company 'FLS' and put it in expected scenario context 'NobodyOfficeEmployersInOffice'
	Then I validate count of 'allCompanyEmployers' collection is '4'
	And I validate collection of 'allCompanyEmployers' 'FLS' company consist of person with id
		| ID          |
		| IdOfPerson1 |
		| IdOfPerson2 |
		| IdOfPerson3 |
		| IdOfPerson4 |
	And I validate count of 'NobodyOfficeEmployersInOffice' collection is '0'
	And I validate collection 'NobodyOfficeEmployersInOffice' is empty

@Alex
Scenario: GetStuffOfficeEmployers_WhenOnePersonComeOut
	When The person with id come out from the office company 'FLS'
		| ID          |
		| IdOfPerson3 |
	When I Get List Of All Company Employers as new Director of company 'FLS' and put it in actual scenario context 'allCompanyEmployers'
	When I Get List Of Stuff Office Employers as new Director of company 'FLS' and put it in expected scenario context 'stuffOfficeEmployers'
	Then I validate count of 'allCompanyEmployers' collection is '4'
	And I validate collection of 'allCompanyEmployers' 'FLS' company consist of person with id
		| ID          |
		| IdOfPerson1 |
		| IdOfPerson2 |
		| IdOfPerson3 |
		| IdOfPerson4 |
		And I validate collection of 'stuffOfficeEmployers' 'FLS' company consist of person with id
		| ID          |
		| IdOfPerson1 |
		| IdOfPerson2 |
		| IdOfPerson4 |
	Then I validate count of 'stuffOfficeEmployers' collection is '3'

@Andrew
Scenario: GetAbsentOfficeEmployers_WhenAllPersonInOffice
	When I Get List Of All Company Employers as new Director of company 'FLS' and put it in actual scenario context 'allCompanyEmployers'
	When I Get List Of Absent Office Employers as new Director of company 'FLS' and put it in expected scenario context 'absentOfficeEmployers'
	Then I validate count of 'allCompanyEmployers' collection is '4'
	And I validate collection of 'allCompanyEmployers' 'FLS' company consist of person with id
		| ID          |
		| IdOfPerson1 |
		| IdOfPerson2 |
		| IdOfPerson3 |
		| IdOfPerson4 |
	And I validate count of 'absentOfficeEmployers' collection is '0'
	And I validate collection 'absentOfficeEmployers' is empty

@Andrew
Scenario: GetAbsentOfficeEmployers_WhenOnePersonComeOut
	When The person with id come out from the office company 'FLS'
		| ID          |
		| IdOfPerson4 |
	When I Get List Of All Company Employers as new Director of company 'FLS' and put it in actual scenario context 'allCompanyEmployers'
	When I Get List Of Absent Office Employers as new Director of company 'FLS' and put it in expected scenario context 'absentOfficeEmployers'
	Then I validate count of 'allCompanyEmployers' collection is '4'
	And I validate collection of 'allCompanyEmployers' 'FLS' company consist of person with id
		| ID          |
		| IdOfPerson1 |
		| IdOfPerson2 |
		| IdOfPerson3 |
		| IdOfPerson4 |
	And I validate count of 'absentOfficeEmployers' collection is '1'
	And I validate collection of 'absentOfficeEmployers' 'FLS' company consist of person with id
		| ID          |
		| IdOfPerson4 |
	And I validate that collection of absent office employers 'absentOfficeEmployers' does not contain persons in office

@Andrew
Scenario: GetAbsentOfficeEmployers_WhenAllPersonComeOutAndOneComeIn
	When All person come out from the office company 'FLS'
	When The person with id come in to the office company 'FLS'
		| ID          |
		| IdOfPerson3 |
	When I Get List Of All Company Employers as new Director of company 'FLS' and put it in actual scenario context 'allCompanyEmployers'
	When I Get List Of Absent Office Employers as new Director of company 'FLS' and put it in expected scenario context 'absentOfficeEmployers'
	Then I validate count of 'allCompanyEmployers' collection is '4'
	And I validate collection of 'allCompanyEmployers' 'FLS' company consist of person with id
		| ID          |
		| IdOfPerson1 |
		| IdOfPerson2 |
		| IdOfPerson3 |
		| IdOfPerson4 |
	And I validate count of 'absentOfficeEmployers' collection is '3'
	And I validate collection of 'absentOfficeEmployers' 'FLS' company consist of person with id
		| ID          |
		| IdOfPerson1 |
		| IdOfPerson2 |
		| IdOfPerson4 |
	And I validate that collection of absent office employers 'absentOfficeEmployers' does not contain persons in office

@Andrew
Scenario: GetAbsentOfficeEmployers_WhenPersonComeOutAndComeInAndComeOut
	When All person come out from the office company 'FLS'
	When The person with id come in to the office company 'FLS'
		| ID          |
		| IdOfPerson1 |
		| IdOfPerson2 |
		| IdOfPerson3 |
	When The person with id come out from the office company 'FLS'
		| ID          |
		| IdOfPerson1 |
		| IdOfPerson2 |
		| IdOfPerson3 |
	When I Get List Of All Company Employers as new Director of company 'FLS' and put it in actual scenario context 'allCompanyEmployers'
	When I Get List Of Absent Office Employers as new Director of company 'FLS' and put it in expected scenario context 'absentOfficeEmployers'
	Then I validate count of 'allCompanyEmployers' collection is '4'
	And I validate collection of 'allCompanyEmployers' 'FLS' company consist of person with id
		| ID          |
		| IdOfPerson1 |
		| IdOfPerson2 |
		| IdOfPerson3 |
		| IdOfPerson4 |
	And I validate count of 'absentOfficeEmployers' collection is '4'
	And I validate collection of 'absentOfficeEmployers' 'FLS' company consist of person with id
		| ID          |
		| IdOfPerson1 |
		| IdOfPerson2 |
		| IdOfPerson3 |
		| IdOfPerson4 |
	And I validate that collection of absent office employers 'absentOfficeEmployers' does not contain persons in office

@Andrew
Scenario: GetAbsentOfficeEmployers_WhenOnePersonRemoveFromOffice
	When The person with id remove from the company 'FLS'
		| ID          |
		| IdOfPerson4 |
	When I Get List Of All Company Employers as new Director of company 'FLS' and put it in actual scenario context 'allCompanyEmployers'
	When I Get List Of Absent Office Employers as new Director of company 'FLS' and put it in expected scenario context 'absentOfficeEmployers'
	Then I validate count of 'allCompanyEmployers' collection is '3'
	And I validate collection of 'allCompanyEmployers' 'FLS' company consist of person with id
		| ID          |
		| IdOfPerson1 |
		| IdOfPerson2 |
		| IdOfPerson3 |
	And I validate count of 'absentOfficeEmployers' collection is '0'
	And I validate collection 'absentOfficeEmployers' is empty