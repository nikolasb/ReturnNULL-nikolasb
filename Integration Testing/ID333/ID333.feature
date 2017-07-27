Feature: As a user I want to see the current most approved bill every time I came to the bill page, 
so that I could understand what topic or bill is most controversial.

Scenario: Confirm that the most approved vote bill info are displayed in the bill page.
	Given that the user is on the homepage
	When the user clicks on the BillHub tag
	Then the Current Most Approved Bill text will be displayed


Scenario: Confirm that the most approved vote bill info are displayed in the bill page.
	Given that the user is on the homepage
	When the user clicks on the BillHub tag
	Then the count of Current Most Approved Bill will be displayed


