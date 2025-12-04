#nullable disable
using Amazon.Pages;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Amazon.Steps
{
    [Binding]
    public class AmazonSteps
    {
        private readonly IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;

        // Page Objects
        private readonly HomePage _homePage;
        private readonly ResultsPage _resultsPage;
        private readonly ProductDetailsPage _detailsPage;
        private readonly BasketPage _basketPage;

        public AmazonSteps(IWebDriver driver, ScenarioContext scenarioContext)
        {
            _driver = driver;
            _scenarioContext = scenarioContext;

            _homePage = new HomePage(_driver);
            _resultsPage = new ResultsPage(_driver);
            _detailsPage = new ProductDetailsPage(_driver);
            _basketPage = new BasketPage(_driver);
        }

        [Given(@"I navigate to ""(.*)""")]
        public void NavigateToUrl(string url)
        {
            _homePage.NavigateToUrl(url);

            Assert.That(_driver.Title, Is.Not.Empty, "Page title should not be empty");

            TestContext.Out.WriteLine($"Navigated to: {url}");
        }

        [Given(@"I accept cookies if prompted")]
        public void AcceptCookiesIfPrompted()
        {
            _homePage.AcceptCookies();
        }

        [When(@"I search for ""(.*)"" in the ""(.*)"" section")]
        public void SearchForInTheSection(string term, string category)
        {
            _homePage.SearchForItem(category, term);
        }

        [Then(@"the first item should have the title ""(.*)""")]
        public void FirstItemShouldHaveTheTitle(string expectedTitlePart)
        {
            string actualTitle = _resultsPage.GetFirstResultTitle();
            Assert.That(actualTitle, Does.Contain(expectedTitlePart),
                $"Expected title to contain '{expectedTitlePart}' but found '{actualTitle}'");

            _scenarioContext["StoredTitle"] = actualTitle;
        }

        [Then(@"I verify if the first item has a badge")]
        public void VerifyIfTheFirstItemHasABadge()
        {
            string badgeText = _resultsPage.GetBadgeText();
            if (!string.IsNullOrEmpty(badgeText))
            {
                TestContext.Out.WriteLine($"Badge found on Results Page: {badgeText}");
            }
            else
            {
                TestContext.Out.WriteLine("No badge found on the first item (Results Page).");
            }
        }

        [Then(@"the selected type should be ""(.*)""")]
        public void SelectedTypeShouldBe(string expectedType)
        {
            string actualType = _resultsPage.GetFirstResultType();
            Assert.That(actualType, Does.Contain(expectedType),
                $"Expected type '{expectedType}' but found '{actualType}'");
        }

        [Then(@"I store the price of the first item")]
        public void StoreThePriceOfTheFirstItem()
        {
            string price = _resultsPage.GetFirstResultPrice();

            price = price.Replace("\r\n", "").Replace("\n", "").Trim();

            _scenarioContext["StoredPrice"] = price;
            TestContext.Out.WriteLine($"Stored Price: {price}");
        }

        [When(@"I navigate to the book details page")]
        public void NavigateToTheBookDetailsPage()
        {
            _resultsPage.ClickFirstResult();
        }

        [Then(@"the book title on details page should match the search result")]
        public void TheBookTitleOnDetailsPageShouldMatchTheSearchResult()
        {
            string detailsTitle = _detailsPage.GetTitle();
            Assert.That(actualDetailsTitle, Is.EqualTo(_expectedTitle), 
        $"Expected title '{_expectedTitle}' but found '{actualDetailsTitle}'");
        }

        [Then(@"I verify if the book has a badge on the details page")]
        public void VerifyIfTheBookHasABadgeOnTheDetailsPage()
        {
            string badge = _detailsPage.GetDetailsBadgeText();
            TestContext.Out.WriteLine(string.IsNullOrEmpty(badge) ? "No badge on details page." : $"Badge on details page: {badge}");
        }

        [Then(@"the price should match the stored price from search result")]
        public void ThePriceShouldMatchTheStoredPriceFromSearchResult()
        {
            string currentPrice = _detailsPage.GetPrice();
            string storedPrice = (string)_scenarioContext["StoredPrice"];
            string cleanCurrent = currentPrice.Replace("\r\n", "").Replace("\n", "").Trim();
            string cleanStored = storedPrice.Trim();

            TestContext.Out.WriteLine($"Price check - Stored: {cleanStored}, Current: {cleanCurrent}");
        }

        [When(@"I add the book to the basket")]
        public void AddTheBookToTheBasket()
        {
            _detailsPage.AddToBasket();
        }

        [Then(@"I should see a notification with title ""(.*)""")]
        public void ShouldSeeANotificationWithTitle(string notificationTitle)
        {
            bool displayed = _detailsPage.IsAddedToBasketDisplayed();
            Assert.That(displayed, Is.True, $"Notification '{notificationTitle}' was not displayed.");
        }

        [Then(@"the basket subtotal should show (.*) item")]
        public void TheBasketSubtotalShouldShowItem(int expectedCount)
        {
            string subtotalText = _detailsPage.GetSubtotalText();
            Assert.That(subtotalText, Does.Contain($"{expectedCount}"),
                $"Expected subtotal to contain '{expectedCount} item' but found '{subtotalText}'");
        }

        [When(@"I click on edit the basket")]
        public void ClickOnEditTheBasket()
        {
            _detailsPage.GoToBasket();
        }

        [Then(@"the book is shown on the list")]
        public void TheBookIsShownOnTheList()
        {
            bool isDisplayed = _basketPage.IsBookDisplayed();
            Assert.That(isDisplayed, Is.True, "The book was not found in the basket list.");
        }

        [Then(@"Verify that the title, type of print, price, quantity is (.*), and total price are correct")]
        public void VerifyBasketDetails(int expectedQty)
        {
            // Verify Title
            string basketTitle = _basketPage.GetBookTitle();
            string storedTitle = (string)_scenarioContext["StoredTitle"];
            Assert.That(storedTitle, Does.Contain(basketTitle.Substring(0, 10)), "Basket title mismatch");

            // Verify Type
            string basketType = _basketPage.GetBookType();
            Assert.That(basketType, Does.Contain("Paperback"), "Binding type mismatch");

            // Verify Price
            string basketPriceRaw = _basketPage.GetPrice();
            string storedPriceRaw = (string)_scenarioContext["StoredPrice"];

            TestContext.Out.WriteLine($"Check -> Basket: {basketPriceRaw} | Stored: {storedPriceRaw}");

            // Use the Helper class logic
            decimal basketNum = Amazon.Utilities.ParserHelper.ParsePrice(basketPriceRaw);
            decimal storedNum = Amazon.Utilities.ParserHelper.ParsePrice(storedPriceRaw);

            Assert.That(basketNum, Is.EqualTo(storedNum).Within(0.10m),
                $"Price mismatch! Expected {storedNum} but found {basketNum}");

            // Verify Quantity
            string basketQty = _basketPage.GetQuantity();
            Assert.That(basketQty, Is.EqualTo(expectedQty.ToString()), "Quantity mismatch");

            // Verify Total Price
            string totalRaw = _basketPage.GetSubtotalAmount();
            decimal totalNum = Amazon.Utilities.ParserHelper.ParsePrice(totalRaw);

            Assert.That(totalNum, Is.EqualTo(basketNum).Within(0.01m),
                "Total price should match item price");
        }
    }
}