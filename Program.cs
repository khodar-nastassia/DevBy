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

        //Получите список всех доступных специализаций, по которым имеются вакансии, и их число
        var listVacancies = vacanciesPage.GetInfo();// создание словарь (профессия. число вакансий)

        //сортировка по убыванию числа вакансий и ее вывод 
        var sortVacancies = listVacancies.OrderByDescending(x => x.Value).ToList(); // sort by number of vacancy
        foreach (var item in sortVacancies)
        {
            Console.WriteLine(item.Key + "- " + item.Value);
        }


        //Проверьте, что суммарное число вакансий по специализациям совпадает с числом вакансий, указанных на главной странице
        int number = sortVacancies.Select(x => x.Value).Sum();// вычисленное значение число вакансий
        bool result = vacanciesPage.CheckNumberVacancies(number);
        Console.WriteLine(result);

        indexPage.Uninitialize();
    }
}