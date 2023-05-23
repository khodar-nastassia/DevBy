using DevBy.Pages;
using OpenQA.Selenium.Chrome;

namespace DevBy;

internal class Program
{
    static void Main(string[] args)
    {

        var driver = new ChromeDriver();
        IndexPage indexPage = new IndexPage(driver);

        var vacanciesPage = indexPage.SwitchToVacanciesPage();

        var listVacancies = vacanciesPage.GetInfo();
        var sortVacancies = listVacancies.OrderByDescending(x => x.Value).ToList();
        foreach (var item in sortVacancies)
        {
            Console.WriteLine(item.Key + "- " + item.Value);
        }

        int number = sortVacancies.Select(x => x.Value).Sum();
        bool result = vacanciesPage.CheckNumberVacancies(number);

        indexPage.Uninitialize();
    }
}