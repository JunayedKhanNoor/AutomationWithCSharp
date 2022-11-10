using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
namespace SeleniumLearning

{
    public class E2ETest
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {

            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            //Implicit TimeOut
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }
        [Test]
        public void EndToEndFlow()
        {
            String[] expectedProducts = { "iphone X", "Blackberry" };
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
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
            IWebElement header = driver.FindElement(By.CssSelector("div.container div.row div.col-sm-12.col-md-10.col-md-offset-1 table.table.table-hover tbody:nth-child(2) tr:nth-child(3) td:nth-child(4) > h3:nth-child(1)"));
            TestContext.Progress.WriteLine(header.Text);
            String expected = "Total";
            Assert.That(header.Text, Is.EqualTo(expected));
            //driver.Quit();
        }
    }
}

