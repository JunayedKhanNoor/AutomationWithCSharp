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
    public class FunctionalTest
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
        public void DropDown()
        {
            IWebElement dropdown = driver.FindElement(By.XPath("//*[@id=\"login-form\"]/div[5]/select"));
            SelectElement s = new SelectElement(dropdown);
            s.SelectByText("Teacher");
            s.SelectByValue("consult");
            s.SelectByIndex(1);
            IList<IWebElement> rdios = driver.FindElements(By.CssSelector("input[type='radio']"));
            //rdios[1].Click();
            foreach(IWebElement radioButton in rdios)
            {
                if(radioButton.GetAttribute("value").Equals("user"))
                {
                    radioButton.Click();
                }
            }
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(8));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.Id("okayBtn")));
            driver.FindElement(By.Id("okayBtn")).Click();
            Boolean result = driver.FindElement(By.Id("usertype")).Selected;
            //Assert.IsTrue(result);
            Assert.That(result, Is.True);
            //driver.Quit();
        }
        }
}
