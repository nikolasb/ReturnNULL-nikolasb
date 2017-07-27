Feature: As a user I want to be able to log in with my google account so that 
I do not have to give/create me own credential

Scenario: Confirm that User is able to log in with their own google account
	Given tht the user is on the log in page and that he has a valid google account
	When the user clicks on the google icon
	Then User will be redirect to the google page and will be able to log in


Scenario: Confirm that User is able to log in with their own google account
		Given that user is in log in page and that they have a valid google account 
		when they click on goole icon and type in their google credential 
		Then they should be able to log in successfully





