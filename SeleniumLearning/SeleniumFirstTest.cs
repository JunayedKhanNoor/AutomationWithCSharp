using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class SeleniumFirstTest
    {
        IWebDriver driver;
       [SetUp]
        public void StartBrowser()
        {
            //ChromeDriver driver = new ChromeDriver();
            // new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            // driver = new ChromeDriver();
            // new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
            // driver = new FirefoxDriver();
            new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
            driver = new EdgeDriver();
        }
        [Test]
        public void Test1()
        {
            driver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
            driver.Manage().Window.Maximize();
            TestContext.Progress.WriteLine(driver.Title);
            TestContext.Progress.WriteLine(driver.Url);
            //TestContext.Progress.WriteLine(driver.PageSource);
            //driver.Close();
            driver.Quit();
        }
    }
}
