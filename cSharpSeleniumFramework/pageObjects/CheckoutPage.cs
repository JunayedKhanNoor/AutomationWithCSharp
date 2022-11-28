using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpSeleniumFramework.pageObjects
{
    public class CheckoutPage

    {
        IWebDriver driver;
        public CheckoutPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How =How.CssSelector,Using = "h4 a")]
        private IList<IWebElement> CheckoutCards;
        [FindsBy(How = How.CssSelector, Using = ".btn-success")]
        private IWebElement CheckoutButton;
        

        public IList<IWebElement> getCards()
        {
            return CheckoutCards;
        }
        public void checkOut()
        {
            CheckoutButton.Click();
        }
    }
}
