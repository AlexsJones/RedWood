Feature: DynamicPages
	In order to test dynamic in memory pages
	We need to test out the functionality

@Firefox
Scenario: Dynamic Page add method
	Given I have created a dynamic page
	And I create a dynamic method
	Then the expando object should work correctly when I execute it
