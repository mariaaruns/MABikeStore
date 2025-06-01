using BikeStore.Domain.Contracts.IService;
using BikeStore.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpHost = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "maria.aruns270798@gmail.com";
        private readonly string _smtpPass = "";

        public async Task<EmailSendResult> SendEmailAsync(string Subject, string Body, List<string> Attachments, List<string> ToAddress, List<string> ToCc, bool IsHtmlBody = true)
        {
            var result = new EmailSendResult();

            try
            {
                using (var client = new SmtpClient(_smtpHost, _smtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);

                    using (var mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(_smtpUser);
                        mailMessage.Subject = Subject;
                        mailMessage.Body = Body;
                        mailMessage.IsBodyHtml = IsHtmlBody;

                        foreach (var to in ToAddress)
                            mailMessage.To.Add(to);

                        if (ToCc != null)
                        {
                            foreach (var cc in ToCc)
                                mailMessage.CC.Add(cc);
                        }

                        if (Attachments != null)
                        {
                            foreach (var file in Attachments)
                            {
                                mailMessage.Attachments.Add(new Attachment(file));
                            }
                        }

                        await client.SendMailAsync(mailMessage);

                        result.Success = true;
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
                return result;
            }
        }
    }
}
