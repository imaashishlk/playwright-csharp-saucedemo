using System.Text.RegularExpressions;

namespace SauceDemo.Tests.Support;

public abstract class UiTestBase : IAsyncLifetime
{
    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;

    protected IPage Page { get; private set; } = null!;

    // Runs before every test: open a browser and a page
    public async ValueTask InitializeAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _playwright.Selectors.SetTestIdAttribute("data-test");
        _browser = await _playwright.Chromium.LaunchAsync();
        Page = await _browser.NewPageAsync(new BrowserNewPageOptions
        {
            BaseURL = "https://www.saucedemo.com"
        });
    }

    // Runs after every test: screenshot if it failed, then close everything
    public async ValueTask DisposeAsync()
    {
        var failed = TestContext.Current.TestState?.Result == TestResult.Failed;
        // var failed = true;

        if (failed)
        {
            var testName = TestContext.Current.Test?.TestDisplayName ?? "unknown-test";
            var fileName = Regex.Replace(testName, @"[^a-zA-Z0-9]+", "_").Trim('_');
            var folder = Directory.CreateDirectory(
                Path.Combine(AppContext.BaseDirectory, "screenshots"));

            await Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = Path.Combine(folder.FullName, $"{fileName}.png"),
                FullPage = true
            });
        }

        await _browser.CloseAsync();
        _playwright.Dispose();
    }
}