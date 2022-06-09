using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PNB.Domain;
using PNB.Domain.Common;
using PNB.Domain.Infrastructure;
using PNB.Domain.Models;
using PNB.Service.sSetting;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace PNB.Service.sPicture
{
    public partial class PictureService : IPictureService
    {
        private readonly IRepository<Picture> _pictureRepository;
        private readonly IRepository<PictureBinary> _binaryPictureRepository;
        private readonly ISettingService _settingService;
        private readonly IPNBFileProvider _fileProvider;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PictureService(
            IRepository<Picture> pictureRepository,
             IPNBFileProvider fileProvider,
             ISettingService settingService,
             IWebHostEnvironment webHostEnvironment,
            IRepository<PictureBinary> binaryPictureRepository
            )
        {
            _pictureRepository = pictureRepository;
            _fileProvider = fileProvider;
            _settingService = settingService;
            _webHostEnvironment = webHostEnvironment; ;
            _binaryPictureRepository = binaryPictureRepository;
        }

        public virtual Picture GetPictureById(int pictureId)
        {
            if (pictureId == 0)
                return null;

            return _pictureRepository.GetById(pictureId);
        }
        public virtual void DeletePicture(Picture picture)
        {
            if (picture == null)
                throw new ArgumentNullException(nameof(picture));
            //delete from database
            _pictureRepository.Delete(picture);
        }

        public virtual IPagedList<Picture> GetPictures(string virtualPath = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _pictureRepository.Table;

            if (!string.IsNullOrEmpty(virtualPath))
                query = virtualPath.EndsWith('/') ? query.Where(p => p.VirtualPath.StartsWith(virtualPath) || p.VirtualPath == virtualPath.TrimEnd('/')) : query.Where(p => p.VirtualPath == virtualPath);

            query = query.OrderByDescending(p => p.Id);

            return new PagedList<Picture>(query, pageIndex, pageSize);
        }




        public virtual Picture InsertPicture(Picture picture)
        {
            if (picture == null)
                return null;
            _pictureRepository.Insert(picture);
            return picture;
        }




        public virtual Picture UpdatePicture(Picture picture)
        {
            if (picture == null)
                return null;
            _pictureRepository.Update(picture);
            return picture;
        }
        public string GetPictureById(int? id, int? width)
        {
            if (id == null)
            {
                return MediaDefaults.DefaultImageFileName;
            }
            string host = _settingService.GetSettingByKey("configurationapi.host").Value;
            bool enableCdn = bool.Parse(_settingService.GetSettingByKey("configurationapi.enablecdngooglephoto").Value);
            string extension = ".jpeg";
            string prefixPictureSize = "";
            if(width!=null)
            {
                prefixPictureSize = "-" + id.ToString()+"-"+width.ToString();
            }
            var picture = _pictureRepository.GetById(id.Value);
            string fileName = picture.SeoFilename + prefixPictureSize + extension;
            var path = _fileProvider.Combine(_webHostEnvironment.WebRootPath, MediaDefaults.ImageThumbsPath);
            var fullpath = _fileProvider.Combine(_webHostEnvironment.WebRootPath, MediaDefaults.ImageThumbsPath, fileName);
            if (!_fileProvider.FileExists(fullpath))
            {
                var binaryPicture = _binaryPictureRepository.Table.Where(x => x.PictureId == picture.Id).FirstOrDefault();
               if(width != null)
                {
                    if (!_fileProvider.DirectoryExists(path))
                        _fileProvider.CreateDirectory(path);

                    MemoryStream myMemStream = new MemoryStream(binaryPicture.BinaryData);
                    Image fullsizeImage = Image.FromStream(myMemStream);
                    int X = fullsizeImage.Width;
                    int Y = fullsizeImage.Height;
                    int heightAuto = (int)((width * Y) / X);
                    Image newImage = fullsizeImage.GetThumbnailImage(width.Value, heightAuto, null, IntPtr.Zero);
                    MemoryStream myResult = new MemoryStream();
                    newImage.Save(myResult, ImageFormat.Jpeg);  //Or whatever format you want.
                    var bytePicture = myResult.ToArray();
                    using (var stream = new FileStream(fullpath, FileMode.Create, FileAccess.Write, FileShare.Write, 4096))
                    {
                        stream.Write(bytePicture, 0, myResult.ToArray().Length);
                        stream.Dispose();
                    }
                    using (var webPFileStream = new FileStream(fullpath.Replace(".jpeg", ".webp"), FileMode.Create))
                    {
                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                        {
                            imageFactory.Load(bytePicture)
                                        .Format(new WebPFormat())
                                        
                                        .Quality(80)
                                        .Save(webPFileStream);
                        }
                    }
                }
                else
                {
                    using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write, 4096))
                    {
                        stream.Write(binaryPicture.BinaryData, 0, binaryPicture.BinaryData.ToArray().Length);
                        stream.Dispose();
                    }
                    using (var webPFileStream = new FileStream(fullpath.Replace(".jpeg", ".webp"), FileMode.Create))
                    {
                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                        {
                            imageFactory.Load(binaryPicture.BinaryData)
                                        .Format(new WebPFormat())
                                        .Quality(80)
                                        .Save(webPFileStream);
                        }
                    }
                }

            }
           
            string pathPicture = _fileProvider.Combine(MediaDefaults.ImageThumbsPath, fileName).Replace(".jpeg", ".webp");
            if (enableCdn)
            {
                pathPicture = CDNPhoto.GooglePhotoUrl + pathPicture;
            }
            return pathPicture;

        }
        public string GetPictureById(int? id, int?width ,int? height)
        {
            string host = _settingService.GetSettingByKey("configurationapi.host").Value;
            bool enableCdn = bool.Parse(_settingService.GetSettingByKey("configurationapi.enablecdngooglephoto").Value);
            if (id == null)
            {
                return MediaDefaults.DefaultImageFileName;
            }
            string extension = ".jpeg";
            string prefixPictureSize = "";
            if (height != null)
            {
                prefixPictureSize = "-" + id.ToString() + "-" + width.ToString()+"x" + height.ToString();
            }
            var picture = _pictureRepository.GetById(id.Value);
            string fileName = picture.SeoFilename + prefixPictureSize + extension;
            var path = _fileProvider.Combine(_webHostEnvironment.WebRootPath, MediaDefaults.ImageThumbsPath);
            var fullpath = _fileProvider.Combine(_webHostEnvironment.WebRootPath, MediaDefaults.ImageThumbsPath, fileName);
            if (!_fileProvider.FileExists(fullpath))
            {
                var binaryPicture = _binaryPictureRepository.Table.Where(x => x.PictureId == picture.Id).FirstOrDefault();
                if (height != null)
                {
                    if (!_fileProvider.DirectoryExists(path))
                        _fileProvider.CreateDirectory(path);

                    MemoryStream myMemStream = new MemoryStream(binaryPicture.BinaryData);
                    Image fullsizeImage = Image.FromStream(myMemStream);
                   
                    Image newImage = fullsizeImage.GetThumbnailImage(width.Value, height.Value, null, IntPtr.Zero);
                    MemoryStream myResult = new MemoryStream();
                    newImage.Save(myResult, ImageFormat.Jpeg);  //Or whatever format you want.
                    var bytePicture = myResult.ToArray();
                    using (var stream = new FileStream(fullpath, FileMode.Create, FileAccess.Write, FileShare.Write, 4096))
                    {
                        stream.Write(bytePicture, 0, myResult.ToArray().Length);
                        stream.Dispose();


                    }
                    using (var webPFileStream = new FileStream(fullpath.Replace(".jpeg", ".webp"), FileMode.Create))
                    {
                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                        {
                            imageFactory.Load(bytePicture)
                                        .Format(new WebPFormat())
                                        .Quality(80)
                                        .Save(webPFileStream);
                        }
                    }
                }
                else
                {
                    using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Write, 4096))
                    {
                        stream.Write(binaryPicture.BinaryData, 0, binaryPicture.BinaryData.ToArray().Length);
                        stream.Dispose();
                        
                    }
                    
                    using (var webPFileStream = new FileStream(fullpath.Replace(".jpeg", ".webp"), FileMode.Create))
                    {
                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true))
                        {
                            imageFactory.Load(binaryPicture.BinaryData)
                                        .Format(new WebPFormat())
                                        .Quality(80)
                                        .Save(webPFileStream);
                        }
                    }
                }

            }
            string pathPicture = _fileProvider.Combine(MediaDefaults.ImageThumbsPath, fileName).Replace(".jpeg", ".webp");
            if (enableCdn)
            {
                pathPicture =CDNPhoto.GooglePhotoUrl+ pathPicture;
            }
            return pathPicture;

        }
        public byte[] ConvertoBinaryImage(IFormFile file)
        {
            var image = Image.FromStream(file.OpenReadStream());
            var size = new Bitmap(image);
            using var imageStream = new MemoryStream();
            size.Save(imageStream, ImageFormat.Jpeg);
            var imageBytes = imageStream.ToArray();
            return imageBytes;
        }
        public byte[] ConvertoBinaryImage(FileStream file)
        {
            var image = Image.FromStream(file);
            var size = new Bitmap(image);
            using var imageStream = new MemoryStream();
            size.Save(imageStream, ImageFormat.Jpeg);
            var imageBytes = imageStream.ToArray();
            return imageBytes;
        }

    }

}
