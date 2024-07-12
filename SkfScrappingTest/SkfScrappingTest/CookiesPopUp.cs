using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using SkfScrappingTest.LoginToTestAzure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkfScrappingTest
{
    public class CookiesPopUp: PageBase
    {
        private IWebDriver _driver;
        private By BotonAceptar => By.XPath("a[@class='cmpboxbtn cmpboxbtnyes cmptxt_btn_yes']");
        private By PopUp => By.CssSelector("//div[@id='cmpwrapper']");

        public CookiesPopUp(IWebDriver driver):base(driver)
        {
            _driver = driver;
        }

        internal void AceptarCookies()
        {
            try
            {
                string script = "return document.querySelector('div#cmpwrapper').shadowRoot.querySelector('a.cmpboxbtn.cmpboxbtnyes.cmptxt_btn_yes').click()";
                _driver.ExecuteJavaScript(script);
            }
            catch (WebDriverTimeoutException e)
            {
                Console.WriteLine("El pop-up de cookies no apareció en el tiempo esperado: " + e.Message);
            }
        }
    }
}
