using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using cSharpSeleniumFramework.utilities;
using cSharpSeleniumFramework.pageObjects;
using NUnit.Framework;

namespace cSharpSeleniumFramework
{
    [Parallelizable(ParallelScope.Children)]
    public class E2ETest : Base
    {
        // Comand to run test
        //dotnet test pathto.csproj (To run all test)
        //dotnet test pathto.csproj --filter TestCategory=Smoke
        //dotnet test cSharpSeleniumFramework.csproj --filter TestCategory=Smoke -- TestRunParameters.Parameter\(name=\"browserName\",value=\"Firefox\")

        [Test,TestCaseSource("AddTestDataConfig"),Category("Regrassion")]
        //[TestCase("rahulshettyacademy", "learning")]
        //[TestCase("rahulshettyemy", "learning")]
        [Parallelizable(ParallelScope.All)]
        public void EndToEndFlow(string userName,string password, String[] expectedProducts)
        {
            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(8));
            //String[] expectedProducts = { "iphone X", "Blackberry" };
            String[] actualProducts = new String[2];
            LoginPage loginPage = new LoginPage(getDriver());
            ProductsPage productsPage = loginPage.validLogin(userName,password);
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



            driver.Value.FindElement(By.Id("country")).SendKeys("ind");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
            driver.Value.FindElement(By.LinkText("India")).Click();
            driver.Value.FindElement(By.XPath("//label[@for='checkbox2']")).Click();
            driver.Value.FindElement(By.CssSelector("input[value='Purchase']")).Click();
            String confirmText = driver.Value.FindElement(By.CssSelector(".alert-success")).Text;
            StringAssert.Contains("Success", confirmText);
            //driver.Value.Quit();
        }
        [Test,Category("Smoke")]
        public void WindowHandle()
        {
            String actualEmail = "mentor@rahulshettyacademy.com";
            String parentWindow = driver.Value.CurrentWindowHandle;
            driver.Value.FindElement(By.ClassName("blinkingText")).Click();
            Assert.That(driver.Value.WindowHandles.Count, Is.EqualTo(2));
            driver.Value.SwitchTo().Window(driver.Value.WindowHandles[1]);
            String text = driver.Value.FindElement(By.CssSelector(".red")).Text;
            String[] splittedText = text.Split("at");
            String[] trimmedString = splittedText[1].Trim().Split(" ");
            String email = trimmedString[0];
            TestContext.Progress.WriteLine(email);
            Assert.That(email, Is.EqualTo(actualEmail));
            driver.Value.SwitchTo().Window(parentWindow);
            driver.Value.FindElement(By.Id("username")).SendKeys(email);

        }
        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            //yield return new TestCaseData("rahulshettyacademy", "learning");
            //yield return new TestCaseData("rahulshettyacademy", "learning");
            //yield return new TestCaseData("rahuldemy", "learning");
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("username"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
            yield return new TestCaseData(getDataParser().extractData("usernameWrong"), getDataParser().extractData("password"), getDataParser().extractDataArray("products"));
        }
    }
}