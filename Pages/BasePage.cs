using Amazon.Utilities;
using OpenQA.Selenium;

namespace Amazon.Pages
{
    public class BasePage
    {
        // Protected driver instance so child pages can access it
        protected IWebDriver Driver;

        protected const int DefaultWait = 10;
        protected const int LongWait = 30;

        // Constructor to initialize the driver
        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        // Wrapper method to click elements with automatic wait
        public void Click(By locator)
        {
            WaitHelper.WaitUntilClickable(Driver, locator);
            Driver.FindElement(locator).Click();
        }

        // Wrapper method to enter text safely
        public void EnterText(By locator, string text)
        {
            var element = WaitHelper.WaitUntilVisible(Driver, locator);
            element.Clear(); 
            element.SendKeys(text);
        }

        // Wrapper method to get text safely
        public string GetText(By locator)
        {
            var element = WaitHelper.WaitUntilVisible(Driver, locator);
            return element.Text;
        }
    }
}