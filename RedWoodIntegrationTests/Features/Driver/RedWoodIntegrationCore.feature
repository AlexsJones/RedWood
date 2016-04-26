Feature: RedWoodIntegrationCore
	In order to test out some of the principle library features
	I want to confirm the iOC works and retrieve and use functionality

@PhantomJs
Scenario: Navigate to a webpage
	Given I have a web browser
	When I navigate to http://google.com
	Then the page title should be Google