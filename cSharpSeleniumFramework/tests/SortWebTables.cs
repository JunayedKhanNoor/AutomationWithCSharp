using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumLearning
{
    public class SortWebTables
    {
        IWebDriver driver;
        [SetUp]
        public void StartBrowser()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Url = "https://rahulshettyacademy.com/seleniumPractise/#/offers";
        }
        [Test]
        public void SortTable()
        {
            ArrayList arrayList = new ArrayList();
            SelectElement dropdown = new SelectElement(driver.FindElement(By.Id("page-menu")));
            dropdown.SelectByText("20");
            IList <IWebElement> veggies =  driver.FindElements(By.XPath("//tr/td[1]"));
            TestContext.Progress.WriteLine(veggies.Count);
            foreach(IWebElement veggie in veggies)
            {
                arrayList.Add(veggie.Text);
                //TestContext.Progress.WriteLine(veggie.Text);
            }
            arrayList.Sort();
            foreach(String element in arrayList)
            {
                TestContext.Progress.WriteLine(element);
            }
            driver.FindElement(By.XPath("//span[contains(text(),'Veg/fruit name')]")).Click();
            IList <IWebElement> sortedVeggies = driver.FindElements(By.XPath("//tr/td[1]"));
            ArrayList sortedArrayList = new ArrayList();
            foreach(IWebElement sortedElement in sortedVeggies)
            {
                sortedArrayList.Add(sortedElement.Text);
            }
            //Assert.AreEqual(arrayList, sortedArrayList);
            Assert.That(sortedArrayList, Is.EqualTo(arrayList));
            driver.Quit();
        }

    }
}
