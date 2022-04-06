using System;
using System.IO;
using MI.Entity.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private IHostingEnvironment _env;

        public UploadFileController(IHostingEnvironment env)
        {
            _env = env;
        }
        //[EnableCors("AllowSpecificOrigin")]
        [Route("UploadImage")]
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload()
        {
            //List<Image> lstImg = new List<Image>();
            ResponseData res = new ResponseData();
            try
            {
                var webRoot = _env.WebRootPath;
                var folderName = Path.Combine("uploads",
                    "images" + @"\" + DateTime.Now.Year.ToString() + @"\" + DateTime.Now.Month.ToString() + @"\" +
                    DateTime.Now.Day.ToString());
                var pathToSave = Path.Combine(webRoot, folderName);

                if (Request.Form.Files.Count > 0)
                {
                    foreach (var file in Request.Form.Files)
                    {
                        var ext1 = file.FileName.Substring(file.FileName.LastIndexOf('.'));
                        var extension1 = ext1.ToLower();
                        var fileName = Guid.NewGuid().ToString() + extension1;

                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);

                        if (!Directory.Exists(pathToSave))
                        {
                            Directory.CreateDirectory(pathToSave);
                        }

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        string fileSave = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" +
                                          DateTime.Now.Day.ToString() + "/" + fileName;

                        //lstImg.Add(new Image()
                        //{
                        //    Name = fileSave
                        //});
                        res.Success = true;
                        res.Message = "Upload thành công.";
                        res.LinkImage = @"\" + dbPath;
                    }
                }
                else
                {
                    res.ErrorCode = -1;
                    res.Success = false;
                    res.Message = "Hãy chọn một file.";
                }

                return Ok(res);
            }
            catch (Exception ex)
            {
                res.ErrorCode = -2;
                res.Success = false;
                res.Message = "Lỗi hệ thống, Vui lòng liên hệ quản trị.";
                return Ok(res);
            }
        }
    }
}
