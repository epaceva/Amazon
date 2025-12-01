#nullable disable
using Amazon.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Amazon.Drivers
{
    public class BrowserDriver
    {
        public IWebDriver Current { get; private set; }

        public IWebDriver Initialize()
        {
            // Read browser type from appsettings.json
            string browserType = ConfigReader.GetBrowser();

            // Default to Chrome if nothing is specified
            if (string.IsNullOrEmpty(browserType)) browserType = "Chrome";

            switch (browserType.ToLower())
            {
                case "firefox":
                    return StartFirefox();
                case "edge":
                    return StartEdge();
                case "chrome":
                default:
                    return StartChrome();
            }
        }

        private IWebDriver StartChrome()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var options = new ChromeOptions();

            // Headless logic for CI/CD (GitHub Actions)
            if (Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true")
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--window-size=1920,1080");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--no-sandbox");
            }
            else
            {
                options.AddArgument("--start-maximized");
            }

            Current = new ChromeDriver(options);
            return Current;
        }

        private IWebDriver StartFirefox()
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            var options = new FirefoxOptions();

            if (Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true")
            {
                options.AddArgument("--headless");
                options.AddArgument("--width=1920");
                options.AddArgument("--height=1080");
            }

            Current = new FirefoxDriver(options);
            if (Environment.GetEnvironmentVariable("GITHUB_ACTIONS") != "true")
            {
                Current.Manage().Window.Maximize();
            }
            return Current;
        }

        private IWebDriver StartEdge()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            var options = new EdgeOptions();

            if (Environment.GetEnvironmentVariable("GITHUB_ACTIONS") == "true")
            {
                options.AddArgument("--headless=new");
                options.AddArgument("--window-size=1920,1080");
            }
            else
            {
                options.AddArgument("--start-maximized");
            }

            Current = new EdgeDriver(options);
            return Current;
        }

        public void Cleanup()
        {
            if (Current != null)
            {
                Current.Quit();
                Current.Dispose();
                Current = null;
            }
        }
    }
}