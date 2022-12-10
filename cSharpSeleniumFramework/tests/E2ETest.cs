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
using cSharpSeleniumFramework.utilities;

namespace SeleniumLearning

{
    [Parallelizable(ParallelScope.Self)]
    public class E2ETest : Base
    {
        [Test]
        public void EndToEndFlow()
        {
            String[] expectedProducts = { "iphone X", "Blackberry" };
            String[] actualProducts = new String[2];
            driver.Value.FindElement(By.Id("username")).SendKeys("rahulshettyacademy");
            driver.Value.FindElement(By.Name("password")).SendKeys("learning");
            driver.Value.FindElement(By.XPath("//*[@id=\"terms\"]")).Click();
            driver.Value.FindElement(By.CssSelector("input[value='Sign In']")).Click();

            WebDriverWait wait = new WebDriverWait(driver.Value, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.
                ElementIsVisible(By.PartialLinkText("Checkout")));
            IList<IWebElement> productsList = driver.Value.FindElements(By.TagName("app-card"));
            TestContext.Progress.WriteLine(productsList.Count);
            foreach (IWebElement product in productsList)
            {
                TestContext.Progress.WriteLine(product.FindElement(By.CssSelector(".card-title a")).Text);
                if (expectedProducts.Contains(product.FindElement(By.CssSelector(".card-title a")).Text))
                {
                    product.FindElement(By.CssSelector(".card-footer button")).Click();
                }
            }
            driver.Value.FindElement(By.PartialLinkText("Checkout")).Click();
            //IWebElement header = driver.FindElement(By.CssSelector("div.container div.row div.col-sm-12.col-md-10.col-md-offset-1 table.table.table-hover tbody:nth-child(2) tr:nth-child(3) td:nth-child(4) > h3:nth-child(1)"));
            //TestContext.Progress.WriteLine(header.Text);
            //String expected = "Total";
            //Assert.That(header.Text, Is.EqualTo(expected));
            //driver.Quit();

            IList<IWebElement> checkoutCards = driver.Value.FindElements(By.CssSelector("h4 a"));
            for (int i = 0; i < checkoutCards.Count; i++)
            {
                actualProducts[i] = checkoutCards[i].Text;
            }
            Assert.That(actualProducts, Is.EqualTo(expectedProducts));
            driver.Value.FindElement(By.CssSelector(".btn-success")).Click();
            driver.Value.FindElement(By.Id("country")).SendKeys("ind");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.LinkText("India")));
            driver.Value.FindElement(By.LinkText("India")).Click();
            driver.Value.FindElement(By.XPath("//label[@for='checkbox2']")).Click();
            driver.Value.FindElement(By.CssSelector("input[value='Purchase']")).Click();
            String confirmText = driver.Value.FindElement(By.CssSelector(".alert-success")).Text;
            StringAssert.Contains("Success", confirmText);
        }
    }
}

