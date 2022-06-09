using Microsoft.AspNetCore.Http;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sSeo
{
    public interface IUrlRecordService
    {
        void DeleteUrlRecord(UrlRecord urlRecord);
        UrlRecord GetUrlRecordsById(int urlRecordId);
        UrlRecord GetUrlRecordsByEntityId(int urlRecordId, string EntityName);

        void InsertUrlRecord(UrlRecord urlRecord);

        /// <summary>
        /// Updates the URL record
        /// </summary>
        /// <param name="urlRecord">URL record</param>
        void UpdateUrlRecord(UrlRecord urlRecord);

        UrlRecord GetBySlug(string slug);


        string GetSeName(string name);
        string ConvertToSearch(string name);
        string ConvertFileName(IFormFile file);
    }
}
