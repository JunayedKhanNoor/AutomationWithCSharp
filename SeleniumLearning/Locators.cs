using OpenQA.Selenium.Edge;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumLearning
{
    internal class Locators
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            //Implicit TimeOut
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }
        [Test]
        public void LocatorIdentification()
        {
            driver.FindElement(By.Id("username")).SendKeys("Junayed");
            driver.FindElement(By.Id("username")).Clear();
            driver.FindElement(By.Id("username")).SendKeys("rahulshettyacademy hkjhk");
            driver.FindElement(By.Name("password")).SendKeys("learning kkh");
            driver.FindElement(By.XPath("//*[@id=\"terms\"]")).Click();
            driver.FindElement(By.CssSelector("input[value='Sign In']")).Click();
            TestContext.Progress.WriteLine(driver.Title);
            TestContext.Progress.WriteLine(driver.Url);
            //Thread.Sleep(3000);
            //Explicit wait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.
                TextToBePresentInElementValue(driver.FindElement(By.Id("signInBtn")), "Sign In"));
            
            String errorMessage = driver.FindElement(By.ClassName("alert-danger")).Text;
            TestContext.Progress.WriteLine(errorMessage);
            IWebElement link = driver.FindElement(By.LinkText("Free Access to InterviewQues/ResumeAssistance/Material"));
            String hrefAttribute = link.GetAttribute("href");
            String expectedURL = "https://rahulshettyacademy.com/documents-request";
            //Assert.IsNotNull(link);
            //Assert.AreEqual(expectedURL, hrefAttribute);
            Assert.That(hrefAttribute, Is.EqualTo(expectedURL));
            driver.Quit();
        }
    }
}
