using Amazon.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Amazon.Pages
{
    public class HomePage : BasePage
    {
        // Selectors
        private By CookieAcceptButton => By.Id("sp-cc-accept");
        private By SearchDropdown => By.Id("searchDropdownBox");
        private By SearchBox => By.Id("twotabsearchtextbox");
        private By SearchButton => By.Id("nav-search-submit-button");
        private By BotCheckButton => By.XPath("//*[contains(text(), 'Continue shopping')]");
        private By DeliveryDismissButton => By.XPath("//*[@data-action-type='DISMISS']");

        public HomePage(IWebDriver driver) : base(driver) { }

        public void NavigateToUrl(string url)
        {
            Driver.Navigate().GoToUrl(url);

            // Check for Bot Check
            HandleBotCheck();

            // Accept Cookies
            AcceptCookies();

            // Dismiss Delivery Location Notification
            DismissDeliveryNotification();
        }

        public void DismissDeliveryNotification()
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));
                var dismissBtn = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(DeliveryDismissButton));
                dismissBtn.Click();
            }
            catch (WebDriverTimeoutException)
            {
                TestContext.Out.WriteLine("Info: 'Deliver to' notification did not appear (Timeout). Continuing...");
            }
            catch (NoSuchElementException)
            {
                TestContext.Out.WriteLine("Info: 'Deliver to' notification element not found. Continuing...");
            }
        }

        public void HandleBotCheck()
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));
                var button = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(BotCheckButton));
                button.Click();
            }
            catch (WebDriverTimeoutException) { }
            catch (NoSuchElementException) { }
        }

        public void AcceptCookies()
        {
            try
            {
                var cookieBtn = Driver.FindElements(CookieAcceptButton);
                if (cookieBtn.Count > 0)
                {
                    cookieBtn[0].Click();
                }
            }
            catch (WebDriverTimeoutException) { }
        }

        public void SearchForItem(string category, string term)
        {
            var dropdownElement = Driver.FindElement(SearchDropdown);
            var select = new SelectElement(dropdownElement);
            select.SelectByText(category);

            EnterText(SearchBox, term);
            Click(SearchButton);
        }
    }
}