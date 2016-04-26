Feature: RealWorldComplexUse
	In order to test the Sky website
	I want to visit a selection of top level web links

@PhantomJs
Scenario: Visit links
	Given I have a base service Url https://corporate.sky.com/
	When I visit the subpage CorporateSkyPage
	Then I should be on the correct page
	When I visit the subpage AboutSkyPage
	Then I should be on the correct page
