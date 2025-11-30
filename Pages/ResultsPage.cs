#nullable disable
using Amazon.Utilities;
using OpenQA.Selenium;
using System.Linq;

namespace Amazon.Pages
{
    public class ResultsPage : BasePage
    {
        private By FirstResultTitle => By.CssSelector("div[data-component-type='s-search-result'] h2 span");
        private By FirstResultType => By.CssSelector("div[data-component-type='s-search-result'] .a-row.a-size-base.a-color-base");
        private By FirstResultPrice => By.CssSelector("div[data-component-type='s-search-result'] .a-price span[aria-hidden='true']");
        private By ResultBadgeText => By.CssSelector("div[data-component-type='s-search-result'] .a-badge-text");

        public ResultsPage(IWebDriver driver) : base(driver) { }

        public string GetFirstResultTitle()
        {
            return GetText(FirstResultTitle);
        }

        public string GetFirstResultType()
        {
            // Verify 'Paperback'
            return GetText(FirstResultType);
        }

        public string GetFirstResultPrice()
        {
            var element = WaitHelper.WaitUntilVisible(Driver, FirstResultPrice);
            return element.GetAttribute("innerText");
        }

        public string GetBadgeText()
        {
            var badges = Driver.FindElements(ResultBadgeText);

            if (badges.Count > 0)
            {
                return badges.First().Text;
            }
            return string.Empty;
        }

        public void ClickFirstResult()
        {
            Click(FirstResultTitle);
        }
    }
}