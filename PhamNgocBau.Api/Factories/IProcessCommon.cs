using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhamNgocBau.Api.Factories
{
    public interface IProcessCommon
    {
        string MovieAvatarThumb(int PictureId);

        string MovieAvatarHorizontalThumb(int PictureId);


         string CastThumb(int PictureId);


      string CastAvatar(int PictureId);
       
    }
}
