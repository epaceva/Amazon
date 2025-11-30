#nullable disable
using Amazon.Utilities;
using OpenQA.Selenium;

namespace Amazon.Pages
{
    public class BasketPage : BasePage
    {
        private By BasketItemTitle => By.CssSelector("span[class=\"a-truncate sc-grid-item-product-title a-size-base-plus\"] span[class=\"a-truncate-cut\"]");
        private By BasketItemType => By.CssSelector(".sc-product-binding");
        private By BasketItemPrice => By.CssSelector(".sc-product-price");
        private By BasketItemQuantity => By.CssSelector("span[data-a-selector=\"value\"]");
        private By BasketSubtotalPrice => By.Id("sc-subtotal-amount-activecart");

        public BasketPage(IWebDriver driver) : base(driver) { }

        public string GetBookTitle()
        {
            return GetText(BasketItemTitle);
        }

        public string GetBookType()
        {
            return GetText(BasketItemType);
        }

        public string GetPrice()
        {
            return GetText(BasketItemPrice);
        }

        public string GetQuantity()
        {
            return GetText(BasketItemQuantity);
        }

        public string GetSubtotalAmount()
        {
            return GetText(BasketSubtotalPrice);
        }

        public bool IsBookDisplayed()
        {
            return Driver.FindElements(BasketItemTitle).Count > 0;
        }
    }
}