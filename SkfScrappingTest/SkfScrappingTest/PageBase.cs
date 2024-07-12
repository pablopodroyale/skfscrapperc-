using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkfScrappingTest
{
    using System;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;
    using OpenQA.Selenium.Interactions;
    using System.Threading;
    using System.IO;
    using System.Linq;
    using SeleniumExtras.WaitHelpers;

    namespace LoginToTestAzure.Helpers
    {
        public class PageBase
        {
            protected readonly IWebDriver _driver;

            public PageBase(IWebDriver driver)
            {
                _driver = driver;

            }

            protected void ExecuteScript(By element, string script)
            {
                IWebElement webElement = _driver.FindElement(element);
                IJavaScriptExecutor executor = (IJavaScriptExecutor)_driver;
                executor.ExecuteScript(script, webElement);
            }

            protected bool IsElementVisible(By locator)
            {
                var isVisible = false;
                try
                {
                    isVisible = WaitForElementUntilIsVisible(locator).Displayed;
                }
                catch (NoSuchElementException e)
                {
                    isVisible = false;
                }
                catch (Exception e)
                {
                    isVisible = false;
                }
                return isVisible;
            }

            protected IWebElement WaitForElementUntilIsVisible(By locator, int seconds = 10)
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));

                var element = wait.Until(ExpectedConditions.ElementIsVisible(locator));

                element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));

                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);

                return element;
            }

            protected IWebElement WaitForElementUntilIsClickable(By locator, int seconds = 10)
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));

                var element = wait.Until(ExpectedConditions.ElementToBeClickable(locator));

                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);

                return element;
            }

            protected void WaitSeconds(int seconds)
            {
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
            }

            protected void ScrollToElement(By locator)
            {
                var element = _driver.FindElement(locator);
                Actions actions = new Actions(_driver);
                actions.MoveToElement(element);
                actions.Perform();
            }

            protected void Scroll(By locator)
            {
                _driver.FindElement(locator).SendKeys(Keys.ArrowDown);
            }

            protected void ScrollPixel(int pixel)
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                js.ExecuteScript($"window.scrollBy(0,{pixel})");
            }

            protected void ScrollUntilVisible(By element)
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                IWebElement webElement = _driver.FindElement(element);
                js.ExecuteScript("arguments[0].scrollIntoViewIfNeeded(true);", webElement);
            }

            protected void OpenPage(string url, bool maximize = false)
            {
                _driver.Navigate().GoToUrl(url);

                if (maximize)
                {
                    _driver.Manage().Window.Maximize();
                }
            }

            protected void ClickElement(By locator, bool waitUntilVisible = true)
            {
                if (waitUntilVisible)
                {
                    //WaitForElementUntilIsVisible(locator);
                    ScrollToElement(locator);
                }

                _driver.FindElement(locator).Click();
            }

            protected void ClearFieldAndSentKeys(By locator, string text)
            {
                //WaitForElementUntilIsVisible(locator);

                _driver.FindElement(locator).Click();

                _driver.FindElement(locator).Clear();
                _driver.FindElement(locator).SendKeys(Keys.Control + "a");
                _driver.FindElement(locator).SendKeys(Keys.Delete);
                _driver.FindElement(locator).SendKeys(text);

            }

            protected string GetElementText(By locator)
            {
                string value = _driver.FindElement(locator).Text;

                return value;
            }

            protected void SwitchToANewWindow()
            {
                _driver.SwitchTo().Window(_driver.WindowHandles.Last());
            }

            protected void SwitchToABaseWindow()
            {
                _driver.SwitchTo().Window(_driver.WindowHandles.First());
            }

            public bool Exists(By by)
            {

                try
                {
                    if (_driver.FindElements(by).Count != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {

                    return false;
                }

            }

            public void Zoom(int zoom)
            {
                string actionZoom = String.Format("document.body.style.zoom='{0}%'", zoom);
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                js.ExecuteScript(actionZoom);
            }
        }
    }

}
