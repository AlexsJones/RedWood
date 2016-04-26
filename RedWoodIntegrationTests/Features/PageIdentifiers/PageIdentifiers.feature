Feature: PageIdentifiers
	In order to verify a page that I'm on
	I want to use matchers or identifiers as validation

@PhantomJs
Scenario: Verify the page I am on
	Given I'm on BbcPage
	Then I should be on the correct page
