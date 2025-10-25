using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using CsvHelper;
using System.Globalization;
public class News
{
    public string? Title { get; set; }
    public string? Date { get; set; }
    public string? Link { get; set; }
    public string? Content{ get; set; }
}

class Program
{
    static void Main(string[] args)
    {

        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArguments("--headless=new"); // comment out for testing
        using var driver = new ChromeDriver(chromeOptions);
        driver.Navigate().GoToUrl("https://remedium.ru/news/");
        var html = driver.PageSource;
        List<IWebElement> parsedNews = new List<IWebElement>();
        var elements = driver.FindElements(By.CssSelector("div.col-12"));
        foreach (var element in elements.Take(5)) // взять первые 5 элементов
        {
            parsedNews.Add(element);
        }
        string page = "https://remedium.ru/news/?PAGEN_2=";
        int numberOfPage = 1615;
        string nextPage = page + numberOfPage.ToString();
        for (int i = 0; i < 3; i++)
        {
            driver.Navigate().GoToUrl(nextPage);
            elements = driver.FindElements(By.CssSelector("div.col-12"));
            foreach (var element in elements.Take(5)) // взять первые 5 элементов
            {
                var title = element.FindElement(By.CssSelector("div.b-section-item__title")).Text;
                var link = element.FindElement(By.CssSelector("div.b-section-item__title a")).GetAttribute("href");
                var date = element.FindElement(By.CssSelector(".b-meta-item > span:nth-child(2)")).Text;
                driver.Navigate().GoToUrl(link);
                var content = driver.FindElement(By.CssSelector("div.b-news-detail-body")) 
                Console.WriteLine(title);
                Console.WriteLine(link); 
                Console.WriteLine(date);
                parsedNews.Add(element);
            }
            numberOfPage -= 1;
            nextPage = page + numberOfPage.ToString();
        }
        Console.WriteLine(parsedNews.Count());
        var news = new List<News>();

    }
}


