using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class AlertsActionsAutoSuggestive
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/AutomationPractice/";
        }
        [Test]
        public void test_Alert()
        {
            String name = "Junayed";
            driver.FindElement(By.CssSelector("#name")).SendKeys(name);
            driver.FindElement(By.CssSelector("input[onClick*='displayConfirm']")).Click();
            String alert_text = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();
            StringAssert.Contains(name, alert_text);

        }
        [Test]
        public void Ttest_autosuggestive()
        {
            driver.FindElement(By.Id("autocomplete")).SendKeys("ind");
            Thread.Sleep(3000);
            IList<IWebElement> optionSuggestions = driver.FindElements(By.CssSelector(".ui-menu-item div"));
            foreach(IWebElement optionSuggestionItem in optionSuggestions)
            {
                if (optionSuggestionItem.Text.Equals("India"))
                {
                    optionSuggestionItem.Click();
                }
            }
            TestContext.Progress.WriteLine(driver.FindElement(By.Id("autocomplete")).GetAttribute("value"));
        }
        [Test]
        public void test_Action()
        {
            /*driver.Url = "https://rahulshettyacademy.com";
            Actions a = new Actions(driver);
            a.MoveToElement(driver.FindElement(By.CssSelector("a.dropdown-toggle"))).Perform();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//ul[@class='dropdown-menu']/li[1]/a")).Click();*/

            driver.Url = " https://demoqa.com/droppable/";
            Actions a = new Actions(driver);
            a.DragAndDrop(driver.FindElement(By.Id("draggable")),driver.FindElement(By.Id("droppable"))).Perform();
            
        }
        [Test]
        public void frames()
        {
            //Scroll
            IWebElement frame = driver.FindElement(By.Id("courses-iframe"));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", frame);
            //Id, Name, Index for iframe detect
            driver.SwitchTo().Frame("courses-iframe");
            driver.FindElement(By.LinkText("All Access Plan")).Click();
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);
            driver.SwitchTo().DefaultContent();
           
            js.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(By.CssSelector("h1")));
            TestContext.Progress.WriteLine(driver.FindElement(By.CssSelector("h1")).Text);
        }
    }
}
