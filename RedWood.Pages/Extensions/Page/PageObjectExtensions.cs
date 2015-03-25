namespace RedWood.Pages.Extensions.Page
{
    public static class PageObjectExtensions
    {
        public static void Visit(this Implementation.Page.Page page)
        {
          page.Driver.Navigate().GoToUrl(page.Url);
        }

        public static string Title(this Implementation.Page.Page page)
        {
            return page.Driver.Title;
        }
    }
}
