Feature: Basic
	In order to provide a useful framework
	I want to include some precanned steps

@PhantomJs @Example
Scenario: Visit a website
	Given I have a base service URL http://www.google.com
	And I visit the subpage DoodlesPage
	Then I am on the right page
