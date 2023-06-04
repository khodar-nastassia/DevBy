using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System.Collections.ObjectModel;

namespace DevBy.Pages;

internal class IndexPage: DevBasePage
{
    ReadOnlyCollection<IWebElement> _menu;

    IWebElement _vacanciesNuvButton;

    const string VACANCIES_PAGE_XPATH = "//a[@class = 'navbar__nav-item navbar__nav-item_label']";
    const string SITE_PARSE_MENU_XPATH = "//a[@class = 'navbar__nav-item']";
    const string GOOGLE_VINGNETTE_CLOSE_BUTTON_XPATH = "//div[@id='dismiss-button']";

    public IndexPage(IWebDriver driver): base(driver)
    {

        GoToUrl("https://devby.io/");
        Initialize();

    }

    public void Initialize()
    {
        _menu = FindDevByElements(SITE_PARSE_MENU_XPATH);
        _vacanciesNuvButton = FindDevByElement(VACANCIES_PAGE_XPATH);
    }

    public VacanciesPage SwitchToVacanciesPage()
    {
        ClickElement(_vacanciesNuvButton);
        return new VacanciesPage(_driver);
    }

    public PolandPage SwitchToPolandPage()
    {
        ClickElement(_menu[1]);
        return new PolandPage(_driver);
    }

    public void Uninitialize()
    {
        _driver.Close();
    }
}
