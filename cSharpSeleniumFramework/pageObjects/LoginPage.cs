using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpSeleniumFramework.pageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;
        //driver.FindElement(By.Name("password")).SendKeys("learning");
       // driver.FindElement(By.XPath("//*[@id=\"terms\"]")).Click();
       // driver.FindElement(By.CssSelector("input[value='Sign In']")).Click();
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver,this);
        }
        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement username;
        [FindsBy(How = How.Name, Using = "password")]
        private IWebElement password;
        [FindsBy(How = How.XPath, Using = "//*[@id=\"terms\"]")]
        private IWebElement checkBox;
        [FindsBy(How = How.CssSelector, Using = "input[value='Sign In']")]
        private IWebElement signInButton;
        public ProductsPage validLogin(string user, string pass)
        {
            username.SendKeys(user);
            password.SendKeys(pass);
            checkBox.Click();
            signInButton.Click();
            return new ProductsPage(driver);
        }
        public IWebElement getUserNAme()
        {
            return username;
        }
    }
}
