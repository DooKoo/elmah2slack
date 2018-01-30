namespace Elmah2slack
{
    using System.Configuration;
    internal class Settings
    {
        public static string WebhookUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["elmah2slack.webhook.url"] ?? "";
            }
        }

        public static string ChannelName
        {
            get
            {
                return ConfigurationManager.AppSettings["elmah2slack.channel"] ?? "";
            }
        }

    }
}
