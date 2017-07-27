Feature: As a user I want to be able to press the enter button to submit a such on the bill hub so I can more quickly enter my search.

Scenario: Can click search button to submit
	Given that I am on the BillHub page
	When I search for a bill using the keyword 'River' 
		And I select Alabama in the state dropdown
		And I select Agriculture and Food in the tag dropdown
		And I click the search button.
	Then the words 'Tennessee River, gill net fishing in, authorized, Sec. 9-11-88 am'd.' should show as a search result.

Scenario: Can press enter button to submit
	Given that I am on the BillHub page
	When I search for a bill using the keyword 'River' 
		And I select Alabama in the state dropdown
		And I select Agriculture and Food in the tag dropdown
		And I press the enter key.
	Then the words 'Tennessee River, gill net fishing in, authorized, Sec. 9-11-88 am'd.' should show as a search result.