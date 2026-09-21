using SauceDemo.Tests.Pages;
using SauceDemo.Tests.Support;

namespace SauceDemo.Tests.Tests;

public class InventoryTests : UiTestBase
{
    [Fact]
    public async Task Shows_six_products()
    {
        var inventory = await new LoginPage(Page).LoginAsValidUserAsync();

        await Assertions.Expect(inventory.Items).ToHaveCountAsync(6);
    }

    [Fact]
    public async Task Adding_a_product_updates_cart_badge()
    {
        var inventory = await new LoginPage(Page).LoginAsValidUserAsync();

        await inventory.AddToCartAsync(Products.Backpack);

        await Assertions.Expect(inventory.CartBadge).ToHaveTextAsync("1");
    }
}