using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using cSharpSeleniumFramework.utilities;
using cSharpSeleniumFramework.pageObjects;

namespace cSharpSeleniumFramework
{
    public class E2ETest : Base
    {
 
        [Test]
        public void EndToEndFlow()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            String[] expectedProducts = { "iphone X", "Blackberry" };
            String[] actualProducts = new String[2];
            LoginPage loginPage = new LoginPage(getDriver());
            ProductsPage productsPage = loginPage.validLogin("rahulshettyacademy", "learning");
            productsPage.waitForPageDisplay();
           
            IList<IWebElement> productsList = productsPage.getCards();
            TestContext.Progress.WriteLine(productsList.Count);
            foreach (IWebElement product in productsList)
            {
                TestContext.Progress.WriteLine(product.FindElement(productsPage.getCardTitle()).Text);
                if (expectedProducts.Contains(product.FindElement(productsPage.getCardTitle()).Text))
                {
                    product.FindElement(productsPage.addToCartButton()).Click();
                }
            }
            CheckoutPage checkoutPage = productsPage.checkout();
           
            IList<IWebElement> checkoutCards = checkoutPage.getCards();
            for (int i = 0; i < checkoutCards.Count; i++)
            {
                actualProducts[i] = checkoutCards[i].Text;
            }
            Assert.That(actualProducts, Is.EqualTo(expectedProducts));
            checkoutPage.checkOut();



            driver.FindElement(By.Id("country")).SendKeys("ind");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
            driver.FindElement(By.LinkText("India")).Click();
            driver.FindElement(By.XPath("//label[@for='checkbox2']")).Click();
            driver.FindElement(By.CssSelector("input[value='Purchase']")).Click();
            String confirmText = driver.FindElement(By.CssSelector(".alert-success")).Text;
            StringAssert.Contains("Success", confirmText);
            driver.Quit();
        }
    }
}