using OpenQA.Selenium;
using SkfScrappingTest.LoginToTestAzure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkfScrappingTest
{
    public class SkfProducto: PageBase
    {
        private readonly string urlProducto = "https://vehicleaftermarket.skf.com/int/en/products/VKML95000";
        private By CodigoH3 => By.XPath("//h1[@class='sku']");
        private By RejectAll => By.XPath(".//a[@class='cmpboxbtn cmpboxbtnno cmptxt_btn_no']");

        public SkfProducto(IWebDriver driver):base(driver)
        {
           
        }

        public async Task<string> GetProductos(string link)
        {
            await _driver.Navigate().GoToUrlAsync(link);

            if (Exists(RejectAll)) 
            {

                IWebElement rejectElement = WaitForElementUntilIsVisible(RejectAll, 10);
                rejectElement.Click();
            }

            IWebElement element = WaitForElementUntilIsVisible(CodigoH3, 10);
            var codigo = element.Text;
            string[] parts = codigo.Split(new[] { "\r\n" }, StringSplitOptions.None);
            string extractedText = parts[0];
            return extractedText;
        }
    }
}
