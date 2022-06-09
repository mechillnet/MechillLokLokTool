using System;
using System.Collections.Generic;
using System.Text;

namespace PNB.Service.sPicture
{
    public static partial class MediaDefaults
    {
        /// <summary>
        /// Gets a path to the image thumbs files
        /// </summary>
        public static string RootImage => "/images/upload";
        /// <summary>
        /// Gets a path to the image thumbs files
        /// </summary>
        public static string ImageThumbsPath => @"images\thumbs";
        /// <summary>
        /// Gets a path to the image product files
        /// </summary>
        public static string ImageProductsPath => "/images/product";
        public static string ImageMovieProductsPath => "/images/movie";
        /// <summary>
        /// Gets a path to the image product files
        /// </summary>
        public static string ImageCategorysPath => "/images/category";
        /// <summary>
        /// Gets a default avatar file name
        /// </summary>
        public static string DefaultAvatarFileName => "/default-avatar.jpg";

        /// <summary>
        /// Gets a default image file name
        /// </summary>
        public static string DefaultImageFileName => "/images/no-image.png";
    }
}
