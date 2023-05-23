using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBy.Pages
{
    internal class PolandPage : DevBasePage
    {
        IWebDriver _driver;
        List<IWebElement> _news = new List<IWebElement>();

        const string NEWS_LIST_XPATH = "//div[@class='card card_media card_col-mobile']";

        public PolandPage(IWebDriver driver) : base(driver)
        {
            _driver = driver;
            Initialize();

        }


        public void Initialize()
        {
            _news.AddRange(FindDevByElements(NEWS_LIST_XPATH));
        }
        public void Uninitialize()
        {
            _driver.Close();
        }
    }
}
