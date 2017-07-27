Feature: As a user I want to see if my search returned no results so I know if the search was successful.

Scenario: Searching for 'asdf' shows No Results found.
	Given that I am on the BillHub page
	When I search for a bill using the keyword 'asdf' 
		And I click the search button.
	Then the words 'No Results' should show as a search result.

Scenario: Searching for 'angry cats' shows No Results found.
	Given that I am on the BillHub page
	When I search for a bill using the keyword 'angry cats'
		And I click the search button.
	Then the words 'No Results' should show as a search result.