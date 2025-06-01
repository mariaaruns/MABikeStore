using BikeStore.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Domain.Contracts.IService
{
    public interface IEmailService
    {
        Task<EmailSendResult> SendEmailAsync(string Subject, string Body, List<string> Attachments, List<string> ToAddress, List<string> ToCc, bool IsHtmlBody = true);
    }
}
