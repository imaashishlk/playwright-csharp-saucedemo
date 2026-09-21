namespace SauceDemo.Tests.Pages;

public abstract class BasePage
{
    protected BasePage(IPage page) => Page = page;

    protected IPage Page { get; }

    public ILocator Title => Page.GetByTestId("title");
    public ILocator CartBadge => Page.GetByTestId("shopping-cart-badge");
}