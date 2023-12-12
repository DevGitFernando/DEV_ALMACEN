using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace Farmacia
{
    class clsMail
    {
        
        private String fromMail;
        private String fromName;

        SmtpClient client;

        public clsMail()
        {
            client = new SmtpClient();
            client.Credentials = new NetworkCredential("darksider_neo@hotmail.com", "2K9kiubo");
            client.Port = 25; // 587;
            client.Host = "ServerGro";
            client.EnableSsl = false;
        }

        public void From( String fromMail, String fromName )
        {
            this.fromMail = fromMail;
            this.fromName = fromName;
        }

        public String Send(String to, String subject, String body)
        {
            MailMessage msg = new MailMessage();

            msg.To.Add(to);
            msg.From = new MailAddress(this.fromMail, this.fromName , System.Text.Encoding.UTF8);
            msg.Subject = subject;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = body;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;

            try
            {
                this.client.Send(msg);
                return "Mail enviado correctamente";
            }
            catch (SmtpException ex)
            {
                return ex.Message;
            }
        }
    }
}
