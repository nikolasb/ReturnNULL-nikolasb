Feature: As a user I want to see thethe drop down panel are working at the homepage, 
so that I could check my local rep with the function.

Scenario: Confirm that the homepage loaded correctly and have the panel loaded as well.
	Given that the user is on the homepage
	When the user clicks Getting started with BillHub
	Then the the bill hub text will be shown in bold


Scenario: Confirm that the most approved vote bill info are displayed in the bill page.
	Given that the user is on the homepage
	When the user clicks on the BillHub tag
	Then the count of Current Most Approved Bill will be displayed


