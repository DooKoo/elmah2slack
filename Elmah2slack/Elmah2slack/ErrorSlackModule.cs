using Elmah;
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
            var url = "https://hooks.slack.com/services/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";
            var slackClient = new SlackClient(url);
            var slackMessage = new SlackMessage
            {
                Channel = "#demo",
                Text = application.Server.GetLastError().Message,
                IconEmoji = Emoji.Computer,
                Username = application.Server.MachineName
            };
            try
            {
                slackMessage.Attachments = new List<SlackAttachment>();
                slackMessage.Attachments.Add(new SlackAttachment()
                {
                    Title = "StackTrace",
                    Text = application.Server.GetLastError().StackTrace
                });
            }
            catch(Exception w)
            {
                var o = 0;
            }

            slackMessage.Mrkdwn = false;
            slackClient.Post(slackMessage);
        }
    }
}
