#Redwood

Is a collection of tools for UI and API testing in a BDD style (Specflow)
It wraps most components in IoC based paradigms and has support for the concept of pages.

There is an [examples feature](https://github.com/AlexsJones/RedWood/tree/master/RedWood.SpecFlow/Features) that visits a google doodle page using specflow.

E.g.

```
@PhantomJs
Scenario: Visit a website
	Given I have a base service URL http://www.google.com
	And I visit the subpage DoodlesPage
	Then I am on the right page
```

The DoodlesPage is something that is retained as IPage between steps and can be downcast for more specialised functionalities
(All pages have extensions baked in such as `.Visit()`)

Pages can be resolved like so and extensions available [here](https://github.com/AlexsJones/RedWood/blob/master/RedWood.Pages/Extensions/Page/PageObjectExtensions.cs)

```
{
	var p = ScenarioContext.Current<Page>() as LoginPage;
	
	p.LoginSpecificActivity();
}

```
