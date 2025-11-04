using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Text;
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
        var mainUrl = "https://remedium.ru/news/";
        driver.Navigate().GoToUrl(mainUrl);
        var elements = driver.FindElements(By.CssSelector("div.col-12"));
        List<News> links = new List<News>();
        string page = "https://remedium.ru/news/?PAGEN_2=";
        int numberOfPage = 1622;
        string nextPage = page + numberOfPage.ToString();
        for (int i = 0; i < 3; i++)
        {
            driver.Navigate().GoToUrl(nextPage);
            elements = driver.FindElements(By.CssSelector("div.col-12"));
            foreach (var element in elements.Take(5))
            {
                var title = element.FindElement(By.CssSelector("div.b-section-item__title")).Text;
                var link = element.FindElement(By.CssSelector("div.b-section-item__title a")).GetAttribute("href");
                var date = element.FindElement(By.CssSelector(".b-meta-item > span:nth-child(2)")).Text;
                var smth = new News() { Title = title, Date = date, Link = link };
                links.Add(smth);
                Console.WriteLine("hey");
            }
            numberOfPage -= 1;
            nextPage = page + numberOfPage.ToString();
        }
        driver.Quit();
        Console.WriteLine("here");
        var newDriver = new ChromeDriver(chromeOptions);

        for (int i = 0; i < links.Count; i++)
        {
            Console.WriteLine("go");
            newDriver.Navigate().GoToUrl(links[i].Link);
            links[i].Content = newDriver.FindElement(By.CssSelector("div.b-news-detail-body")).Text;
        }
        newDriver.Quit();
        var options = new JsonSerializerOptions
        {
            WriteIndented = true, 
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Для кириллицы
        };

        
        string jsonString = JsonSerializer.Serialize(links, options);
        File.WriteAllText("news.json", jsonString);
        }
    
}


