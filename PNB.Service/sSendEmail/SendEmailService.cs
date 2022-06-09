using PNB.Service.sSetting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace PNB.Service.sSendEmail
{
  public partial  class SendEmailService : ISendEmailService
    {
        private readonly ISettingService _settingService;
        public SendEmailService(ISettingService settingService)
        {
            _settingService = settingService;
        }
       public virtual void SendEmail(string subject, string body, string fromAddress, string fromName, string toAddress, string toName)
        {
            var emailAccount = _settingService.LoadSetting<Mail>();
            var message = new MailMessage
            {
                //from, to, reply to
                From = new MailAddress(fromAddress, fromName)
            };
            message.To.Add(new MailAddress(toAddress, toName));
            //if (!string.IsNullOrEmpty(replyTo))
            //{
            //    message.ReplyToList.Add(new MailAddress(replyTo, replyToName));
            //}

            ////BCC
            //if (bcc != null)
            //{
            //    foreach (var address in bcc.Where(bccValue => !string.IsNullOrWhiteSpace(bccValue)))
            //    {
            //        message.Bcc.Add(address.Trim());
            //    }
            //}

            ////CC
            //if (cc != null)
            //{
            //    foreach (var address in cc.Where(ccValue => !string.IsNullOrWhiteSpace(ccValue)))
            //    {
            //        message.CC.Add(address.Trim());
            //    }
            //}

            //content
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

          
            ////create the file attachment for this e-mail message
            //if (!string.IsNullOrEmpty(attachmentFilePath) &&
            //    _fileProvider.FileExists(attachmentFilePath))
            //{
            //    var attachment = new Attachment(attachmentFilePath);
            //    attachment.ContentDisposition.CreationDate = _fileProvider.GetCreationTime(attachmentFilePath);
            //    attachment.ContentDisposition.ModificationDate = _fileProvider.GetLastWriteTime(attachmentFilePath);
            //    attachment.ContentDisposition.ReadDate = _fileProvider.GetLastAccessTime(attachmentFilePath);
            //    if (!string.IsNullOrEmpty(attachmentFileName))
            //    {
            //        attachment.Name = attachmentFileName;
            //    }

            //    message.Attachments.Add(attachment);
            //}
            ////another attachment?
            //if (attachedDownloadId > 0)
            //{
            //    var download = _downloadService.GetDownloadById(attachedDownloadId);
            //    if (download != null)
            //    {
            //        //we do not support URLs as attachments
            //        if (!download.UseDownloadUrl)
            //        {
            //            var fileName = !string.IsNullOrWhiteSpace(download.Filename) ? download.Filename : download.Id.ToString();
            //            fileName += download.Extension;

            //            var ms = new MemoryStream(download.DownloadBinary);
            //            var attachment = new Attachment(ms, fileName);
            //            //string contentType = !string.IsNullOrWhiteSpace(download.ContentType) ? download.ContentType : "application/octet-stream";
            //            //var attachment = new Attachment(ms, fileName, contentType);
            //            attachment.ContentDisposition.CreationDate = DateTime.UtcNow;
            //            attachment.ContentDisposition.ModificationDate = DateTime.UtcNow;
            //            attachment.ContentDisposition.ReadDate = DateTime.UtcNow;
            //            message.Attachments.Add(attachment);
            //        }
            //    }
            //}

            //send email
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Host = emailAccount.Smtp;
                smtpClient.Port = emailAccount.Port;
                smtpClient.EnableSsl = emailAccount.Ssl;
                smtpClient.Credentials = new NetworkCredential(emailAccount.UserName, emailAccount.Password);
                smtpClient.Send(message);
            }
        }
    }
}
