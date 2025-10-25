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
        var driver = new ChromeDriver(chromeOptions);
        driver.Navigate().GoToUrl("https://remedium.ru/news/");
        var html = driver.PageSource;
        List<IWebElement> parsedNews = new List<IWebElement>(); 
        var elements = driver.FindElements(By.CssSelector("div.col-12"));
        foreach (var element in elements.Take(5)) // взять первые 5 элементов
        {
            parsedNews.Add(element);
        }
        Console.WriteLine(parsedNews.Count());
        string page = "https://remedium.ru/news/?PAGEN_2=";
        int numberOfPage = 1615;
        string nextPage = page + numberOfPage.ToString();
        for (int i = 0; i < 1615; i++)
        {
            driver.Navigate().GoToUrl(nextPage);
            elements = driver.FindElements(By.CssSelector("div.col-12"));
            foreach (var element in elements.Take(5)) // взять первые 5 элементов
            {
                parsedNews.Add(element);
            }
            numberOfPage -= 1;
            nextPage = page + numberOfPage.ToString();
        }
        /* var news = new List<News>();
         foreach (var n in parsedNews) {
             var title = n.FindElement(By.CssSelector("div.b-section-item__title")).Text;
             var link = n.FindElement(By.CssSelector("div.b-section-item__title a")).GetAttribute("href");
             var date = "N/A";
             try
             {
                date = n.FindElement(By.CssSelector(".b-meta-item > span:nth-child(2)")).Text;
             }
             catch(Exception ex)
             {

             }
             var newNews = new News { Title = title, Date = date, Link = link };
             news.Add(newNews);
         }
         driver.Quit();
         // create the CSV output file
         using (var writer = new StreamWriter("news.csv"))
 // instantiate the CSV writer
         using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
         {
     // populate the CSV file
     csv.WriteRecords(news);
 }

     } */
    }
}


