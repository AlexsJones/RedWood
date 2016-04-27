#Redwood

Is a collection of tools for UI and API testing in a BDD style (Specflow)
It wraps most components in IoC based paradigms and has support for the concept of pages.

##TL;DR

`Install-Package Redwood`
Gives you the core tools and IoC friendly interfaces for useful helpers while testing

`Install-Package RedWood.Pages`
Gives you a page object model system which is best explained by viewing the example


There is an [examples feature](https://github.com/AlexsJones/RedWood/tree/master/RedWood.SpecFlow/Features) that visits a google doodle page using specflow.

E.g.

```C#
@PhantomJs
Scenario: Visit a website
	Given I have a base service URL http://www.google.com
	And I visit the subpage DoodlesPage
	Then I am on the right page
```

DoodlesPage is reflected to a concrete class implementation
```C#
    public class DoodlesPage : Page
    {
        public DoodlesPage(IWebDriver driver) : base(driver, "doodles", 
            new[] {
                new RedWood.Pages.Interface.Page.KeyIdentifier(By.LinkText("Doodles Archive"))
            })
        {

        }
    }
```

As you can see we can setup an array of `KeyIdentifier` which can be used to judge if we're on the right page.

