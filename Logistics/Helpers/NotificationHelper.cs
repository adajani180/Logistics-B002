using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace Logistics.Helpers
{
    public class NotificationHelper
    {
        #region main

        public static bool SendEMail(EMail email)
        {
            var IsSuccess = false;

            if (Validate(email))
            {
                Thread emailThread = new Thread(delegate ()
                {
                    IsSuccess = Send(email);
                });

                emailThread.IsBackground = true;
                emailThread.Start();
            }

            return IsSuccess;
        }

        private static bool Send(EMail email)
        {
            using (var mailMessage = new MailMessage())
            {
                mailMessage.IsBodyHtml = true;
                mailMessage.From = new MailAddress(email.From);

                foreach (string emailAddress in email.To)
                    if (!string.IsNullOrEmpty(emailAddress))
                        mailMessage.To.Add(emailAddress);

                foreach (string emailAddress in email.Cc)
                    if (!string.IsNullOrEmpty(emailAddress))
                        mailMessage.CC.Add(emailAddress);

                //NOTE: dont delete, in case we use attachments
                //if (email.PostedFile != null && email.PostedFile.ContentLength > 0)
                //{
                //    string fileName = Path.GetFileName(email.PostedFile.FileName);
                //    mailMessage.Attachments.Add(new Attachment(email.PostedFile.InputStream, fileName));
                //}

                mailMessage.Subject = email.Subject;
                mailMessage.Body = email.Body.ToString();

                // Instantiate a new instance of SmtpClient
                using (var smtp = new SmtpClient())
                {
                    try
                    {
                        smtp.Send(mailMessage);
                        email.IsSent = true;
                    }
                    catch (Exception exc)
                    {
                        //ErrorHelper.LogError(exc, "ERROR");
                        email.IsSent = false;
                    }
                    finally
                    {
                        smtp.Dispose();
                        mailMessage.Dispose();
                    }

                    return email.IsSent;
                }
            }
        }

        private static bool Validate(EMail email)
        {
            if (email == null)
            {
                //ErrorHelper.LogError(new Exception("Attempting to send an EMail with a null object."), "WARN");
                return false;
            }
            if (email.To == null || email.To.Count == 0)
            {
                //string msg = string.Join(" ", "Attempting to send an EMail without a To recipient.", MiscHelper.Html.StripTagsCharArray(email.Body.ToString()));
                //ErrorHelper.LogError(new Exception(msg), "WARN");
                return false;
            }
            if (email.Body == null || string.IsNullOrEmpty(email.Body.ToString()))
            {
                //ErrorHelper.LogError(new Exception("Attempting to send an EMail without a Body."), "WARN");
                return false;
            }

            return true;
        }

        #endregion

        #region Objects
    
        public class EMail
        {
            public string From { get; set; }
            public List<string> To { get; set; }
            public List<string> Cc { get; set; }
            public List<string> AllRecipients
            {
                get
                {
                    List<string> recipients = new List<string>();
                    recipients.AddRange(this.To);
                    recipients.AddRange(this.Cc);
                    return recipients.Distinct().ToList();
                }
            }
            public string Subject { get; set; }
            public StringBuilder Body { get; set; }
            //public HttpPostedFile PostedFile { get; set; }
            public bool IsSent { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            public EMail()
            {
                this.To = new List<string>();
                this.Cc = new List<string>();
            }
        }

        #endregion
    }
}