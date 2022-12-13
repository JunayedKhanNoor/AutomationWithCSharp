using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Configuration;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using ICSharpCode.SharpZipLib.Zip;
using NUnit.Framework.Interfaces;

namespace cSharpSeleniumFramework.utilities
{
    public class Base
    {
        String browserName;
        //Report File
        public ExtentReports extent;
        public ExtentTest test;
        [OneTimeSetUp]
        public void Setup()
        {
            String workingDicertory = Environment.CurrentDirectory;
            string projectDicetory = Directory.GetParent(workingDicertory).Parent.Parent.FullName;
            string reportPath = projectDicetory + "//index.html";
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Tester Name", "Junayed");
        }
        //public IWebDriver driver;
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        private MediaEntityModelProvider captureScreenShot;

        [SetUp]
        public void StartBrowser()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            browserName = TestContext.Parameters["browserName"];
            if(browserName == null)
            {
                browserName = ConfigurationManager.AppSettings["browser"];
            }
            //Configaration
           
            InitBrowser(browserName);
            //Implicit TimeOut
            driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.Value.Manage().Window.Maximize();
            driver.Value.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }
        public IWebDriver getDriver()
        {
            return driver.Value;
        }
        public void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver.Value = new FirefoxDriver();
                    break;
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver.Value = new ChromeDriver();
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver.Value = new EdgeDriver();
                    break;
                 
            }
        }
        public static jsonReader getDataParser()
        {
            return new jsonReader();
        }
        [TearDown]
        public void AfterTest()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrese = TestContext.CurrentContext.Result.StackTrace;
            DateTime time = DateTime.Now;
            String fileName = "Screenshot_" + time.ToString("h_mm_ss") + ".png";
            if(status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                test.Fail("Test Failed");
                //test.Log(status.Fail, "Test Failed" + stackTrese );
            }
            else if (status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {

            }
            extent.Flush();
            driver.Value.Quit();
        }
       /* public captureScreenShot(IWebDriver driver , String ScreenShotName)
        {
            ITakesScreenshot screenshot = (ITakesScreenshot)driver;
            var picture = screenshot.GetScreenshot().AsBase64EncodedString;
            MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, ScreenShotName).Build();
        }*/
    }
}
