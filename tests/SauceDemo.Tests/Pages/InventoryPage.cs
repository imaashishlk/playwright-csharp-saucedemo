namespace SauceDemo.Tests.Pages;

public sealed class InventoryPage : BasePage
{
    public InventoryPage(IPage page) : base(page) { }

    public ILocator Items => Page.GetByTestId("inventory-item");

    public Task AddToCartAsync(string productSlug) =>
        Page.GetByTestId($"add-to-cart-{productSlug}").ClickAsync();
}

public static class Products
{
    public const string Backpack = "sauce-labs-backpack";
}