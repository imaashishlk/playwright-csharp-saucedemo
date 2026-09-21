using System.Text.RegularExpressions;
using SauceDemo.Tests.Pages;
using SauceDemo.Tests.Support;

namespace SauceDemo.Tests.Tests;

public class LoginTests : UiTestBase
{
    [Fact]
    public async Task Valid_user_can_log_in()
    {
        var login = new LoginPage(Page);
        await login.GotoAsync();
        await login.LoginAsync(TestUsers.Standard, TestUsers.Password);

        await Assertions.Expect(Page).ToHaveURLAsync(new Regex("inventory"));
        await Assertions.Expect(new InventoryPage(Page).Title).ToHaveTextAsync("Products");
    }

    [Fact]
    public async Task Locked_out_user_sees_error()
    {
        var login = new LoginPage(Page);
        await login.GotoAsync();
        await login.LoginAsync(TestUsers.LockedOut, TestUsers.Password);

        await Assertions.Expect(login.ErrorMessage).ToContainTextAsync("locked out");
    }

    [Theory]
    [InlineData("standard_user", "wrong_password", "do not match")]
    [InlineData("", "secret_sauce", "Username is required")]
    [InlineData("standard_user", "", "Password is required")]
    public async Task Invalid_credentials_show_error(string user, string password, string expectedText)
    {
        var login = new LoginPage(Page);
        await login.GotoAsync();
        await login.LoginAsync(user, password);

        await Assertions.Expect(login.ErrorMessage).ToContainTextAsync(expectedText);
    }
}
