using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace Amazon.Utilities
{
    public static class WaitHelper
    {
        // A generic method to wait for an element to be clickable
        public static void WaitUntilClickable(IWebDriver driver, By locator, int seconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        // A generic method to wait for an element to be visible
        public static IWebElement WaitUntilVisible(IWebDriver driver, By locator, int seconds = 15)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            return wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }
    }
}