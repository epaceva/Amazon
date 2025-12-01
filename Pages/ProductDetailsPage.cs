#nullable disable
using Amazon.Utilities;
using OpenQA.Selenium;

namespace Amazon.Pages
{
    public class ProductDetailsPage : BasePage
    {
        private By ProductTitle => By.Id("productTitle");
        private By ProductPrice => By.XPath("//div[@id='tmm-grid-swatch-PAPERBACK']//span[contains(text(), '£') or contains(text(), 'BGN')]");
        private By ProductType => By.Id("subtitle");
        private By AddToBasketButton => By.Id("add-to-cart-button");
        private By AddedMessage => By.XPath("//*[contains(text(), 'Added to Basket')]");
        private By CartCount => By.Id("nav-cart-count");
        private By EditBasketButton => By.XPath("//span[@id='sw-gtc']");
        private By SubtotalLabel => By.XPath("//*[@id='nav-cart-count-container']");


        private By LookInsideElement => By.Id("sitbLogoImg");
        private By LookInsideText => By.XPath("//div[@id='img-canvas']//span[contains(text(),'Look inside')]");
        private By DetailsBadge => By.CssSelector(".a-icon-text-fba, .a-badge-text, #zeitgeistBadge_feature_div");

        public ProductDetailsPage(IWebDriver driver) : base(driver) { }

        public string GetTitle() => GetText(ProductTitle);

        public string GetPrice()
        {
            var element = WaitHelper.WaitUntilVisible(Driver, ProductPrice, LongWait);
            return element.GetAttribute("innerText").Trim();
        }

        public string GetBookType() => GetText(ProductType);

        public void AddToBasket() => Click(AddToBasketButton);

        public bool IsAddedToBasketDisplayed()
        {
            return Driver.FindElements(AddedMessage).Count > 0;
        }

        public void GoToBasket() => Click(EditBasketButton);

        public string GetSubtotalText()
        {
            return GetText(SubtotalLabel);
        }

        public string GetDetailsBadgeText()
        {
            var elements = Driver.FindElements(DetailsBadge);
            if (elements.Count > 0)
            {
                return elements[0].Text;
            }
            return string.Empty;
        }
    }
}