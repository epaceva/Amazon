#nullable disable
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Amazon.Drivers
{
    public class BrowserDriver
    {
        public IWebDriver Current { get; private set; }

        public IWebDriver Initialize()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            var options = new ChromeOptions();

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

        public void Cleanup()
        {
            // Close and dispose the driver safely
            if (Current != null)
            {
                Current.Quit();
                Current.Dispose();
                Current = null;
            }
        }
    }
}