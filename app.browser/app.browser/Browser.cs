using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager.DriverConfigs.Impl;

namespace app.browser
{
    public class Browser : IDisposable
    {
        private readonly BrowserConfiguration browserConfiguration;
        public IWebDriver Driver { get; set; }

        public Browser(BrowserConfiguration browserConfiguration)
        {
            this.browserConfiguration = browserConfiguration;
            Initialize();
        }

        public async Task Wait(By xpath)
        {
            if (Driver == null)
                throw new Exception("Browser not initialized");

            WebDriverWait wait = new WebDriverWait(Driver, browserConfiguration.TimeOut);
            wait.Until(ExpectedConditions.ElementExists(xpath));
        }


        /// <summary>
        /// for remote access https://novnc.com/info.html
        /// default novnc : secret
        /// http://localhost:7900/?autoconnect=1&resize=scale&password=secret
        /// 
        /// Docker Referrences: https://hub.docker.com/r/selenium/standalone-chrome
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
        public void Initialize()
        {
            var remoteUrl = new Uri($"http://127.0.0.1:4444/wd/hub");
            var webDriverManager = new WebDriverManager.DriverManager();

            webDriverManager.SetUpDriver(new ChromeConfig());

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddExcludedArgument("enable-automation");

            if (browserConfiguration.IsHeadless)
                chromeOptions.AddArgument("--headless");

            Driver = browserConfiguration.IsRemote ?
            new RemoteWebDriver(remoteUrl, chromeOptions) : new ChromeDriver(chromeOptions);

            if (browserConfiguration.IsMax)
                Driver.Manage().Window.Maximize();

            Driver.Navigate().GoToUrl(browserConfiguration.BaseUrl);

            var wait = new WebDriverWait(Driver, browserConfiguration.TimeOut);

            var loaded = wait.Until(d => { return d.Url.Contains(browserConfiguration.BaseUrl); });

            if (!loaded)
                throw new ApplicationException($"{browserConfiguration.BaseUrl} was not loaded");
        }

        public void Dispose()
        {
            Console.WriteLine("Driver has been disposed");
            Driver.Dispose();
        }
    }
}
