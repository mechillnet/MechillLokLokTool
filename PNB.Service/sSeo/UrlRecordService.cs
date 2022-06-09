using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace PNB.Service.sSeo
{
   public partial class UrlRecordService : IUrlRecordService
    {
        private IRepository<UrlRecord> _urlRecordRepository;

        public UrlRecordService(IRepository<UrlRecord> urlRecordRepository)
        {
            _urlRecordRepository = urlRecordRepository;
        }
          public virtual void DeleteUrlRecord(UrlRecord urlRecord)
        {
            if(urlRecord==null)
                throw new ArgumentNullException(nameof(urlRecord));
            _urlRecordRepository.Delete(urlRecord.Id);
        }
       public UrlRecord GetUrlRecordsById(int urlRecordId)
        {
            if (urlRecordId == 0)
                return null;
          return _urlRecordRepository.GetById(urlRecordId);
        }
        public UrlRecord GetUrlRecordsByEntityId(int urlRecordId,string EntityName)
        {
            var query = from url in _urlRecordRepository.Table where url.EntityId == urlRecordId && url.EntityName == EntityName select url;
            return query.FirstOrDefault();
        }

        public virtual void InsertUrlRecord(UrlRecord urlRecord)
        {
            if (urlRecord == null)
                throw new ArgumentNullException(nameof(urlRecord));
             _urlRecordRepository.Insert(urlRecord);
        }

        /// <summary>
        /// Updates the URL record
        /// </summary>
        /// <param name="urlRecord">URL record</param>
       public virtual void UpdateUrlRecord(UrlRecord urlRecord)
        {
            if (urlRecord == null)
                throw new ArgumentNullException(nameof(urlRecord));
            _urlRecordRepository.Update(urlRecord);
        }

      public  UrlRecord GetBySlug(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                return null;
            var query = from u in _urlRecordRepository.Table where u.Slug==slug select u;
            return query.FirstOrDefault();
        }


       public string GetSeName(string name)
        {
            if (String.IsNullOrEmpty(name))
                return name;
            name = name.Trim().ToLowerInvariant();
            var sb = new StringBuilder();
            foreach (char c in name.ToCharArray())
            {
                string c2 = c.ToString();
                sb.Append(c2);
            }
            string name2 = sb.ToString();
         
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = name2.Normalize(NormalizationForm.FormD);
            name2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            name2 = name2.Replace(" ", "-");
            while (name2.Contains("--"))
                name2 = name2.Replace("--", "-");
            while (name2.Contains("__"))
                name2 = name2.Replace("__", "_");
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            name2 = rgx.Replace(name2, "");
            return name2;
        }
        public string ConvertToSearch(string name)
        {
            if (String.IsNullOrEmpty(name))
                return name;
            name = name.Trim().ToLowerInvariant();
            var sb = new StringBuilder();
            foreach (char c in name.ToCharArray())
            {
                string c2 = c.ToString();
                sb.Append(c2);
            }
            string name2 = sb.ToString();
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = name2.Normalize(NormalizationForm.FormD);
            name2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            return name2;
        }
        public string ConvertFileName(IFormFile file)
        {
          return  this.GetSeName(System.IO.Path.GetFileNameWithoutExtension(file.FileName)) + DateTime.UtcNow.ToString("yyyyMMddhhmmssffff") + System.IO.Path.GetExtension(file.FileName);
        }
    }
}
