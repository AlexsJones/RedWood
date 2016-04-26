Feature: Example
	In order to demonstrate a test framework
	I would like to create several examples
	To show how testing is possible with specflow

@PhantomJs @Example
Scenario: Visit a website
	Given I have a base service URL http://www.google.com
	And I visit the subpage DoodlesPage
	Then I am on the right page

@PhantomJs @Example
Scenario: Visit several subpages
	Given I have a base service URL http://www.google.com
	And I check the subpage and return:
	| page          |
	| DoodlesPage   |
	| TranslatePage |
	| NewsPage      |