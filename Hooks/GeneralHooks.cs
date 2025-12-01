#nullable disable
using Amazon.Drivers;
using BoDi;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using System;
using System.IO;
using Allure.Net.Commons;

namespace Amazon.Hooks
{
    [Binding]
    public class GeneralHooks
    {
        private readonly IObjectContainer _objectContainer;
        private BrowserDriver _browserDriver;

        public GeneralHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void InitializeWebDriver()
        {
            _browserDriver = new BrowserDriver();
            IWebDriver driver = _browserDriver.Initialize();
            _objectContainer.RegisterInstanceAs<IWebDriver>(driver);
        }

        [AfterScenario]
        public void TearDownWebDriver()
        {
            _browserDriver?.Cleanup();
        }

        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError != null)
            {
                try
                {
                    var driver = _objectContainer.Resolve<IWebDriver>();
                    var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                    string filename = $"{Guid.NewGuid()}.png";
                    string filePath = Path.Combine(Path.GetTempPath(), filename);

                    screenshot.SaveAsFile(filePath);
                    byte[] fileContent = File.ReadAllBytes(filePath);

                    AllureApi.AddAttachment("Failure Screenshot", "image/png", fileContent);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error taking screenshot: " + e.Message);
                }
            }
        }
    }
}