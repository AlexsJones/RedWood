Feature: Example
	In order to demonstrate a test framework
	I would like to create several examples
	To show how testing is possible with specflow

@PhantomJs
Scenario: Visit a website
	Given I have a base service URL http://www.google.com
	And I visit the subpage DoodlesPage
	Then I am on the right page

@Chrome
Scenario: Visit a website with Chrome
	Given I have a base service URL http://www.google.com
	And I visit the subpage DoodlesPage
	Then I am on the right page
