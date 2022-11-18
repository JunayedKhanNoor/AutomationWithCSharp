using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class WindowHandelers
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }
        [Test]
        public void WindowHandle()
        {
            String actualEmail = "mentor@rahulshettyacademy.com";
            String parentWindow = driver.CurrentWindowHandle;
            driver.FindElement(By.ClassName("blinkingText")).Click();
            Assert.That(driver.WindowHandles.Count, Is.EqualTo(2));
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            String text = driver.FindElement(By.CssSelector(".red")).Text;
            String[] splittedText = text.Split("at");
            String[] trimmedString = splittedText[1].Trim().Split(" ");
            String email = trimmedString[0];
            TestContext.Progress.WriteLine(email);
            Assert.That(email, Is.EqualTo(actualEmail));
            driver.SwitchTo().Window(parentWindow);
            driver.FindElement(By.Id("username")).SendKeys(email);
            
        }
    }
}
