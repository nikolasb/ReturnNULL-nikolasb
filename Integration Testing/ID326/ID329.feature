Feature: As a  user I want to search for bills in any state so I can stay up to date on politics nationally.

Scenario: Bill exists for Alaska
	Given that I am on the BillHub page
	When I search for a bill using the keyword 'An Act relating to a veteran's designation on an identification card or a driver's license for Hmong veterans and Lao veterans; relating to special motor vehicle registration plates for recipients of the Bronze Star awarded for valor, Silver Star, Navy Cross, Distinguished Service Cross, Air Force Cross, Coast Guard Cross, and other awards reflecting valor; relating to special request specialty organization registration plates; and providing for an effective date.' 
		And I select Alasaka in the state dropdown
		And I click the search button.
	Then the words 'An Act relating to a veteran's designation on an identification card or a driver's license for Hmong veterans and Lao veterans; relating to special motor vehicle registration plates for recipients of the Bronze Star awarded for valor, Silver Star, Navy Cross, Distinguished Service Cross, Air Force Cross, Coast Guard Cross, and other awards reflecting valor; relating to special request specialty organization registration plates; and providing for an effective date.' should show as a search result.

Scenario: Bill doesn't exist for Alabama
	Given that I am on the BillHub page
	When I search for a bill using the keyword 'An Act relating to a veteran's designation on an identification card or a driver's license for Hmong veterans and Lao veterans; relating to special motor vehicle registration plates for recipients of the Bronze Star awarded for valor, Silver Star, Navy Cross, Distinguished Service Cross, Air Force Cross, Coast Guard Cross, and other awards reflecting valor; relating to special request specialty organization registration plates; and providing for an effective date.' 
		And I select Alabama in the state dropdown
		And I click the search button.
	Then the words 'No Results' should show as a search result.