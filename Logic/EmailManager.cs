using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Logic
{
    public class EmailManager
    {
        public bool SendInvite(string RecipientMail, string RecipientName, string TeamName)
        {
            var client = new SendGridClient(Hidden.SendgridApiKey);
            SendGridMessage message = new SendGridMessage();
            message.SetFrom(new EmailAddress("NoReply@Str1XHyper.nl", "F4DED Tournaments"));
            message.AddTo(new EmailAddress(RecipientMail, RecipientName));
            message.SetTemplateId("d-9bdeb16093254f16a0fc75eaf99d0dee");
            message.SetTemplateData(new SendInviteMailData()
            {
                RecipientName = RecipientName,
                TeamName = TeamName,
                Reference = "Str1XHyper.nl"
            });

            var response = client.SendEmailAsync(message).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Console.WriteLine("Email sent");
                return true;
            } else
            {
                return false;
            }
        }

        public bool ReceiveInvite(string RecipientMail, string RecipientName, string TeamName, string InviteeName)
        {
            var client = new SendGridClient(Hidden.SendgridApiKey);
            SendGridMessage message = new SendGridMessage();
            message.SetFrom(new EmailAddress("NoReply@Str1XHyper.nl", "F4DED Tournaments"));
            message.AddTo(new EmailAddress(RecipientMail, RecipientName));
            message.SetTemplateId("d-63bed604fd7a42118cca658cfb34d426");
            message.SetTemplateData(new ReceiveInviteMailData()
            {
                RecipientName = RecipientName,
                TeamName = TeamName,
                Reference = "Str1XHyper.nl",
                Invitee = InviteeName
            });

            var response = client.SendEmailAsync(message).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Console.WriteLine("Email sent");
                return true;
            }
            else
            {
                return false;
            }
        }

        private struct SendInviteMailData
        {
            [JsonProperty("Recipient")]
            public string RecipientName { get; set; }

            [JsonProperty("Team")]
            public string TeamName { get; set; }
            [JsonProperty("Reference")]
            public string Reference { get; set; }
        }
        private struct ReceiveInviteMailData
        {
            [JsonProperty("TeamOwner")]
            public string RecipientName { get; set; }

            [JsonProperty("Team")]
            public string TeamName { get; set; }
            [JsonProperty("Invitee")]
            public string Invitee { get; set; }
            [JsonProperty("Reference")]
            public string Reference { get; set; }
        }
    }



}
