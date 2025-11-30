#nullable disable
using Amazon.Drivers;
using BoDi;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Amazon.Hooks
{
    [Binding]
    public class GeneralHooks
    {
        private readonly IObjectContainer _objectContainer;
        private BrowserDriver _browserDriver;

        // Constructor Injection: SpecFlow gives us the container
        public GeneralHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void InitializeWebDriver()
        {
            // Create the driver management instance
            _browserDriver = new BrowserDriver();

            // Initialize Chrome
            IWebDriver driver = _browserDriver.Initialize();

            // Register the driver instance in the container
            _objectContainer.RegisterInstanceAs<IWebDriver>(driver);
        }

        [AfterScenario]
        public void TearDownWebDriver()
        {
            // Close the browser after the test finishes
            _browserDriver?.Cleanup();
        }
    }
} 