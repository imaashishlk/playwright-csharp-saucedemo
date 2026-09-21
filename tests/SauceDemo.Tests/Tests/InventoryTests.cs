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

    [Fact]
    public async Task Adding_all_products_puts_six_items_in_cart()
    {
        var login = new LoginPage(Page);
        await login.GotoAsync();
        await login.LoginAsync(TestUsers.Problem, TestUsers.Password);
        var inventory = new InventoryPage(Page);

        foreach (var product in Products.All)
        {
            await inventory.AddToCartAsync(product);
        }

        await Assertions.Expect(inventory.CartBadge).ToHaveTextAsync("6");
    }
}