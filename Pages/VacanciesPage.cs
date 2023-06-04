using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace DevBy.Pages;

internal class VacanciesPage: DevBasePage
{
    List<IWebElement> _vacanciesButtons = new List<IWebElement>();
    IWebElement _vacanciesNuvButton;
    int _allVacanciesCount;

    const string ADS_GOOGLE_XPATH = "//ins[@class='adsbygoogle adsbygoogle-noablate' and @data-vignette-loaded]";
    const string VACANCIES_PAGE_XPATH = "//a[@class = 'navbar__nav-item navbar__nav-item_label']";
    const string VACANCIES_LIST_XPATH = "//span[@class='radio']";
    const string CLOSE_REKLAMA = "//button[@class='wishes-popup__button-close wishes-popup__button-close_icon']";
    const string NUMBER_OF_VACANCIES_XPATH = "//h1";
    public VacanciesPage(IWebDriver driver):base(driver)
    {
        //IWebElement iframe = FindDevByElement("//iframe[@id='google_esf']");
        //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].remove();", iframe);
        //var adv = FindDevByElement("//ins[@class='adsbygoogle adsbygoogle-noablate'][2]");
        //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].remove();", adv);
        //"//iframe[@title='iframe' and @src]"
        //ins[@class='adsbygoogle adsbygoogle-noablate']
        //driver.ExecuteJavaScript("return document.getElementById('ad_position_box')[0];");
        //driver.SwitchTo().Frame(FindDevByElement("//iframe[@id='ad_iframe'"));
        //driver.SwitchTo().Window(driver.CurrentWindowHandle);
        //iframe[@id="aswift_1"]
        //iframe[@id='ad_frame']
        //driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@id=\"aswift_1")));
        //var closeButtonAds = driver.FindElement(By.XPath("//div[@id='dismiss-button']"));
        //ClickElement(closeButtonAds);

        IWebElement ads = null;
        try { ads = driver.FindElement(By.XPath(ADS_GOOGLE_XPATH)); }
        catch (NoSuchElementException) { }
        if (ads != null && ads.Displayed)
        {
            driver.Navigate().Refresh();
            _vacanciesNuvButton = FindDevByElement(VACANCIES_PAGE_XPATH);
            ClickElement(_vacanciesNuvButton);

        }

        driver.ExecuteJavaScript("return document.getElementsByClassName('wishes-popup__content')[0];");//запуск рекламы
        var closeRekl = FindDevByElement(CLOSE_REKLAMA);
        ClickElement(closeRekl);

        Initialize();
    }

    public List<string> GetVacanciesTitle()
    {
        return _vacanciesButtons.Select(x =>x.Text).ToList();
    }

    public Dictionary<string,int> GetInfo()
    {

        Dictionary<string,int> listVacancies = new Dictionary<string, int>();
        foreach (var item in _vacanciesButtons)
        {
            ClickElement(item);
            Thread.Sleep(500);
            var numberOfVacancy = int.Parse(FindDevByElement(NUMBER_OF_VACANCIES_XPATH).Text.Split(' ')[0]);
            listVacancies.Add(item.Text, numberOfVacancy);
        }
        return listVacancies;
    }

    public bool CheckNumberVacancies(int number)
    {
        return _allVacanciesCount == number;
    }

    public void Initialize()
    {
        _vacanciesButtons.AddRange(FindDevByElements(VACANCIES_LIST_XPATH));
        _allVacanciesCount = int.Parse(FindDevByElement("//h1[@ class='vacancies-list__header-title']").Text.Split(' ')[0]);
    }

    public void Uninitialize()
    {
        _driver.Close();
    }
}
