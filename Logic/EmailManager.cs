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
        // API Key: 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

        public bool SendInvite(string RecipientMail, string RecipientName, string TeamName)
        {
            var client = new SendGridClient("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");
            SendGridMessage message = new SendGridMessage();
            message.SetFrom(new EmailAddress("NoReply@Str1XHyper.nl", "F4DED Tournaments"));
            message.AddTo(new EmailAddress(RecipientMail, RecipientName));
            message.SetTemplateId("d-e5045eae728b4c7c954753a0e2a135dc");
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
            var client = new SendGridClient("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");
            SendGridMessage message = new SendGridMessage();
            message.SetFrom(new EmailAddress("NoReply@Str1XHyper.nl", "F4DED Tournaments"));
            message.AddTo(new EmailAddress(RecipientMail, RecipientName));
            message.SetTemplateId("d-e5045eae728b4c7c954753a0e2a135dc");
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
