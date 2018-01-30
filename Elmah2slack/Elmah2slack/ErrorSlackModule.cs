using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using Slack.Webhooks;
using Elmah;

namespace Elmah2slack
{
    public class ErrorSlackModule : HttpModuleBase, IExceptionFiltering
    {
        public event ExceptionFilterEventHandler Filtering;

        /// <summary>
        /// Initializes the module and prepares it to handle requests.
        /// </summary>

        protected override void OnInit(HttpApplication application)
        {
            if (application == null)
                throw new ArgumentNullException("application");

            application.Error += OnError;
        }

        /// <summary>
        /// The handler called when an unhandled exception bubbles up to 
        /// the module.
        /// </summary>

        protected virtual void OnError(object sender, EventArgs args)
        {
            var application = (HttpApplication)sender;
            var slackClient = new SlackClient(Settings.WebhookUrl);
            var slackMessage = new SlackMessage
            {
                Channel = Settings.ChannelName,
                Text = application.Server.GetLastError().Message,
                IconEmoji = Emoji.Computer,
                Username = application.Request.Url.AbsoluteUri
            };
            slackMessage.Attachments = new List<SlackAttachment>
            {
                new SlackAttachment()
                {
                    Title = "StackTrace",
                    Text = application.Server.GetLastError().StackTrace
                }
            };

            slackMessage.Mrkdwn = false;
            var result = slackClient.Post(slackMessage);
        }
    }
}
