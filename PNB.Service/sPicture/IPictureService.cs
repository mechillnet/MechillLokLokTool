using Microsoft.AspNetCore.Http;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PNB.Service.sPicture
{
   public interface IPictureService
    {
       
        Picture GetPictureById(int pictureId);

        void DeletePicture(Picture picture);

        IPagedList<Picture> GetPictures(string virtualPath = "", int pageIndex = 0, int pageSize = int.MaxValue);


        Picture InsertPicture(Picture picture);

        Picture UpdatePicture(Picture picture);
        string GetPictureById(int? id, int? width);
        string GetPictureById(int? id, int? width, int? height);
        byte[] ConvertoBinaryImage(IFormFile file);
        byte[] ConvertoBinaryImage(FileStream file);
    }
}
