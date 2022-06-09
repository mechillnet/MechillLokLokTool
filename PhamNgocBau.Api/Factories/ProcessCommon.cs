using PNB.Domain.Common;
using PNB.Service.sPicture;
using PNB.Service.sSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Factories
{
    public class ProcessCommon: IProcessCommon
    {
        #region ctor
     
        private readonly ISettingService _settingService;
        private readonly IPictureService _pictureService;
        public ProcessCommon(
            ISettingService settingService,
            IPictureService pictureService
            )
        {
           
            _settingService = settingService;
            _pictureService = pictureService;
        }
        #endregion
        public string MovieAvatarThumb(int PictureId)
        {
            return _pictureService.GetPictureById(PictureId, PictureSize.AvatarWidth);
        }
        public string MovieAvatarHorizontalThumb(int PictureId)
        {
            return _pictureService.GetPictureById(PictureId, PictureSize.AvatarHorizontalWidth);
        }
        public string CastThumb(int PictureId)
        {
            return _pictureService.GetPictureById(PictureId, PictureSize.CastWidthThumb);
        }
        public string CastAvatar(int PictureId)
        {
            return _pictureService.GetPictureById(PictureId, PictureSize.CastWidth);
        }
    }
}
