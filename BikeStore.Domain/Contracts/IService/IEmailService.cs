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
        Task<bool> SendEmailAsync(string Subject, string Body, List<string> Attachments, bool IsHtmlBody = true);
    }
}
