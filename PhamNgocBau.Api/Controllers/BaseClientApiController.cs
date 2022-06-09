using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhamNgocBau.Api.Model;
using Newtonsoft.Json;
using PNB.Api.Framework.Common;

namespace PhamNgocBau.Api.Controllers
{
    [Route("api/[controller]")]
    public class BaseClientApiController : ControllerBase
    {
        protected virtual IActionResult ModelStateUnvalid(string message)
        {
            return BadRequest(
                new ResponseApiBaseModel
                {
                    status = -1,
                    message = message
                });
        }
        protected virtual IActionResult ModelStateError()
        {
            var errors = new Dictionary<string, List<string>>();
            foreach (var item in ModelState)
            {
                var errorMessages = item.Value.Errors.Select(x => x.ErrorMessage);
                var validErrorMessages = new List<string>();
                validErrorMessages.AddRange(errorMessages.Where(message => !string.IsNullOrEmpty(message)));
                if (validErrorMessages.Count > 0)
                {
                    if (errors.ContainsKey(item.Key))
                    {
                        errors[item.Key].AddRange(validErrorMessages);
                    }
                    else
                    {
                        errors.Add(item.Key, validErrorMessages.ToList());
                    }
                }
            }
            var errorsJson = JsonConvert.SerializeObject(errors);
            return BadRequest(new ResponseApiBaseModel
            {
                status = (int)BaseApiStatus.Failed,
                message = "vui lòng kiểm tra lại dữ liệu đầu vào",
                data = errors
            });
        }
        protected virtual IActionResult ResponseApiBaseModel()
        {
            return Unauthorized(new ResponseApiBaseModel
            {
                status = (int)BaseApiStatus.Authorization401,
                message = "Xác thực token đăng nhập thất bại"
            });
        }
        protected virtual IActionResult CustomBadRequest(string message)
        {
            return BadRequest(new ResponseApiBaseModel
            {
                status = (int)BaseApiStatus.Failed,
                message = message
            });
        }
        protected virtual IActionResult ResponeNotFound(string message)
        {
            return BadRequest(new ResponseApiBaseModel
            {
                status = (int)BaseApiStatus.NotFound,
                message = message
            });
        }
        protected virtual IActionResult ResponeSystemError()
        {
            return BadRequest(new ResponseApiBaseModel
            {
                status = (int)BaseApiStatus.ErrorSystem,
                message = "Lỗi Hệ Thống , Vui Lòng Liên Hệ Admin",
            });
        }
    }
}
