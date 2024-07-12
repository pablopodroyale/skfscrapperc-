using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SkfScrappingTest.LoginToTestAzure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkfScrappingTest
{
    public class SkfHome: PageBase
    {
        private readonly IWebDriver _driver;
        private readonly string urlHomePageBase = "https://www.skf.com/ar";

        public SkfHome(IWebDriver driver): base(driver) 
        {
            _driver = driver;
        }

        internal void OpenHome()
        {
            _driver.Navigate().GoToUrl(urlHomePageBase);

        }

    }
}
