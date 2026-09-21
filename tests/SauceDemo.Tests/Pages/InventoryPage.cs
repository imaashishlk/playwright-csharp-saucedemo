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
    public const string BikeLight = "sauce-labs-bike-light";
    public const string BoltTShirt = "sauce-labs-bolt-t-shirt";
    public const string FleeceJacket = "sauce-labs-fleece-jacket";
    public const string Onesie = "sauce-labs-onesie";
    public const string RedTShirt = "test.allthethings()-t-shirt-(red)";

    public static readonly string[] All =
    {
        Backpack, BikeLight, BoltTShirt, FleeceJacket, Onesie, RedTShirt
    };
}

