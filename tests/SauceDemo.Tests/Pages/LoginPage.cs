namespace SauceDemo.Tests.Pages;

public sealed class LoginPage : BasePage
{
    public LoginPage(IPage page) : base(page) { }

    public ILocator ErrorMessage => Page.GetByTestId("error");

    public async Task GotoAsync() => await Page.GotoAsync("/");

    public async Task LoginAsync(string username, string password)
    {
        await Page.GetByTestId("username").FillAsync(username);
        await Page.GetByTestId("password").FillAsync(password);
        await Page.GetByTestId("login-button").ClickAsync();
    }

    public async Task<InventoryPage> LoginAsValidUserAsync()
    {
        await GotoAsync();
        await LoginAsync(TestUsers.Standard, TestUsers.Password);
        return new InventoryPage(Page);
    }
}

public static class TestUsers
{
    public const string Standard = "standard_user";
    public const string LockedOut = "locked_out_user";
    public const string Password = "secret_sauce";
}
