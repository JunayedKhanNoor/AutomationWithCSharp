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
            String[] expectedProducts = { "iphone X", "Blackberry" };
            String[] actualProducts = new String[2];
            LoginPage loginPage = new LoginPage(getDriver());
            loginPage.getUserNAme().SendKeys("rahulshettyacademy");         
            driver.FindElement(By.Name("password")).SendKeys("learning");
            driver.FindElement(By.XPath("//*[@id=\"terms\"]")).Click();
            driver.FindElement(By.CssSelector("input[value='Sign In']")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.
                ElementIsVisible(By.PartialLinkText("Checkout")));
            IList<IWebElement> productsList = driver.FindElements(By.TagName("app-card"));
            TestContext.Progress.WriteLine(productsList.Count);
            foreach (IWebElement product in productsList)
            {
                TestContext.Progress.WriteLine(product.FindElement(By.CssSelector(".card-title a")).Text);
                if (expectedProducts.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))
                {
                    product.FindElement(By.CssSelector(".card-footer button")).Click();
                }
            }
            driver.FindElement(By.PartialLinkText("Checkout")).Click();
           
            IList<IWebElement> checkoutCards = driver.FindElements(By.CssSelector("h4 a"));
            for (int i = 0; i < checkoutCards.Count; i++)
            {
                actualProducts[i] = checkoutCards[i].Text;
            }
            Assert.That(actualProducts, Is.EqualTo(expectedProducts));
            driver.FindElement(By.CssSelector(".btn-success")).Click();
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