using System;
using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace SampleTest
{
    [AllureNUnit]
    public class Tests
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            var driverOptions = new ChromeOptions();
            var runName = GetType().Assembly.GetName().Name;
            var timestamp = $"{DateTime.Now:yyyyMMdd.HHmm}";
            driverOptions.AddAdditionalCapability("name", runName, true);
            driverOptions.AddAdditionalCapability("videoName", $"{runName}.{timestamp}.mp4", true);
            driverOptions.AddAdditionalCapability("logName", $"{runName}.{timestamp}.log", true);
            driverOptions.AddAdditionalCapability("enableVNC", true, true);
            driverOptions.AddAdditionalCapability("enableVideo", true, true);
            driverOptions.AddAdditionalCapability("enableLog", true, true);
            driverOptions.AddAdditionalCapability("screenResolution", "1920x1080x24", true);

            _driver = new RemoteWebDriver(new Uri("http://127.0.0.1:4444/wd/hub"), driverOptions);
        }

        [Test(Description="Check Title on main page")]
        [AllureTag("Smoke")]
        [AllureSeverity(SeverityLevel.normal)]
        public void CheckTitleMainPage()
        {
            _driver.Navigate().GoToUrl("https://duckduckgo.com/");
            IWebElement title = _driver.FindElement(By.CssSelector(".badge-link__title"));
            Assert.AreEqual("Tired of being tracked online? We can help.", title.Text);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}