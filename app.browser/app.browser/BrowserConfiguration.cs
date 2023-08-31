namespace app.browser
{
    public class BrowserConfiguration
    {

        private readonly bool isRemote;
        private readonly bool isMax;
        private readonly string baseUrl;
        private readonly int timeout;
        private readonly bool isHeadless;

        /// <summary>
        /// if true use web driver
        /// </summary>
        public bool IsRemote => isRemote;
        /// <summary>
        /// if true maximize browser, only if it is not remote driver
        /// </summary>
        public bool IsMax => isMax && !isRemote;
        /// <summary>
        /// run headless
        /// </summary>
        public bool IsHeadless => isHeadless;
        /// <summary>
        /// base url of the solution
        /// </summary>
        public string BaseUrl => baseUrl;
        /// <summary>
        /// scraping timeout
        /// </summary>
        public TimeSpan TimeOut => TimeSpan.FromSeconds(timeout);

        public BrowserConfiguration(bool isMax, string baseUrl,
            int timeout, bool isHeadless, bool isRemote)
        {
            this.isRemote = isRemote;
            this.isMax = isMax;
            this.baseUrl = baseUrl;
            this.timeout = timeout;
            this.isHeadless = isHeadless;
        }
    }
}
