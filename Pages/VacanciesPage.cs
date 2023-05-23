using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace DevBy.Pages;

internal class VacanciesPage: DevBasePage
{
    List<IWebElement> _vacanciesButtons = new List<IWebElement>();
    int _allVacanciesCount;

    const string VACANCIES_LIST_XPATH = "//span[@class='radio']";
    const string CLOSE_REKLAMA = "//button[@class='wishes-popup__button-close wishes-popup__button-close_icon']";

    public VacanciesPage(IWebDriver driver):base(driver)
    {
        _driver = driver;
        //driver.ExecuteJavaScript("return document.getElementsByClassName('adsbygoogle adsbygoogle-noablate')[0];");

        driver.ExecuteJavaScript("return document.getElementsByClassName('wishes-popup__content')[0];");
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
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30)); 
            var _countVacancy = wait.Until(driver => driver.FindElement(By.XPath("//div[@class='vacancies-list__header-breadcrumbs js-vacancies-list-count']")));

            var nameVacancy = FindDevByElement("//span[@class='vacancies-list__filter-tag__text']").Text;
            var countVacancy = int.Parse(FindDevByElement("//h1").Text.Split(' ')[0]);
            if (listVacancies.ContainsKey(nameVacancy))
                {
                continue;
                }

             listVacancies.Add(nameVacancy, countVacancy);
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
