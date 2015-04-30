Feature: PageBaseModifier
	I want to be able to set a service base url 
	Everything should then hinge off of this url

@Firefox
Scenario: Using Base Url pattern
	Given I have a base service Url http://www.bbc.co.uk/
	And I am on relative BbcNewsPage
	Then I should be on the correct page
