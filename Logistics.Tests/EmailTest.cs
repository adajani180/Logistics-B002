using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Net.Configuration;
using static Logistics.Helpers.NotificationHelper;

namespace Logistics.Tests
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        public void TestSendEmail()
        {
            SmtpSection section = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            Assert.IsNull(section);

            var e = new EMail()
            {
                From = section.From,
                To = new System.Collections.Generic.List<string>() { "Ahmed.Dajani@gmail.com" },
                Subject = "SATIS Notification",
                Body = new System.Text.StringBuilder($"<p><b>SATIS Notification</b></p>")
            };

            // from Logistics.Helpers.NotificationHelper
            var IsSent = SendEMail(e);

            Assert.IsTrue(IsSent);
        }
    }
}
