using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using LazZiya.ImageResize;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MI.Bo.Bussiness;
using MI.Entity.Common;
using MI.Entity.Models;
using Utils;
using Image = System.Drawing.Image;
using OfficeOpenXml.ConditionalFormatting.Contracts;
using JanHome.CMS.ExcelHelper;


namespace JanHome.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadV2Controller : ControllerBase
    {
        FileUploadBCL _fileUploadBCL;
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        private readonly IExportUtility _workingFile;

        public FileUploadV2Controller(IHostingEnvironment env, IConfiguration iConfig, IExportUtility workingFile)
        {
            _env = env;
            _config = iConfig;
            _fileUploadBCL = new FileUploadBCL();
            _workingFile = workingFile;
        }

        [Route("UploadImage")]
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Upload(List<IFormFile> files)
        {

            string serverPath = _config.GetValue<string>("AppSettings:UploadFolder");
            string folder = string.Format("{0:yyyy/MM/dd}", DateTime.Now);
            var res = new ResponseData();
            res.Success = false;
            try
            {

                var webRoot = _env.WebRootPath;
                var fileDirectory = Path.Combine(serverPath, folder);
                var pathToSave = Path.Combine(webRoot, fileDirectory);

                if (Request.Form.Files.Count > 0)
                {
                    var uploadResults = new List<FileInfo>();
                    string imageAllowUpload = _config.GetValue<string>("AppSettings:ImageAllowUpload");
                    string documentAllowUpload = _config.GetValue<string>("AppSettings:DocumentAllowUpload");
                    bool fileUploadSubFix = _config.GetValue<bool>("AppSettings:FileUploadSubFix");
                    int fileUploadMaxSize = _config.GetValue<int>("AppSettings:FileUploadMaxSize");
                    int imageScaleWidth = _config.GetValue<int>("AppSettings:ImageScaleWidth");
                    int imageScaleHeight = _config.GetValue<int>("AppSettings:ImageScaleHeight");
                    foreach (var file in Request.Form.Files)
                    {
                        var ext = Path.GetExtension(fileDirectory + "\\" + file.FileName);

                        var imageAllowFileArray = imageAllowUpload.Split(',');
                        var documentAllowFileArray = documentAllowUpload.Split(',');
                        if (Array.IndexOf(imageAllowFileArray, ext.ToLower()) != -1 || Array.IndexOf(documentAllowFileArray, ext.ToLower()) != -1)
                        {
                            var fileName = Path.GetFileName(fileDirectory + "\\" + file.FileName);
                            if (fileUploadMaxSize >= (file.Length / 1024))
                            {

                                if (fileUploadSubFix)
                                {
                                    string filenameNoExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);
                                    fileName = Utility.UnicodeToKoDauAndGach(filenameNoExtension) + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + ext;
                                }
                                var fullPath = Path.Combine(pathToSave, fileName);
                                if (!Directory.Exists(pathToSave))
                                {
                                    Directory.CreateDirectory(pathToSave);
                                }

                                if (!_fileUploadBCL.ExistFile(fileName))
                                {
                                    using (var stream = new FileStream(fullPath, FileMode.Create))
                                    {
                                        file.CopyTo(stream);
                                        var obj = new FileUpload();
                                        obj.Name = fileName;
                                        obj.FilePath = "/" + Path.Combine(folder).Replace("\\", "/") + "/" + fileName;
                                        obj.FileExt = ext;
                                        //   obj.Dimensions = dimensions;
                                        obj.FileSize = file.Length / 1024;
                                        obj.Status = 1;
                                        obj.Type = 2;

                                        ResponseData responseData = new ResponseData();
                                        try
                                        {
                                            bool result = _fileUploadBCL.Add(obj);
                                            if (result)
                                            {

                                                responseData.Success = true;
                                                responseData.Message = "Thành công";
                                                FileInfo fileInfo = new FileInfo
                                                {
                                                    name = fileName,
                                                    path = "/" + Path.Combine(folder).Replace("\\", "/") + "/" + fileName,
                                                    ext = ext,
                                                    size = file.Length / 1024,//Math.Round((double)file.Length / 1024, 1),
                                                    code = 200,
                                                    messages = "Upload thành công ne"

                                                };
                                                uploadResults.Add(fileInfo);
                                                if (Array.IndexOf(imageAllowFileArray, ext.ToLower()) != -1)
                                                {
                                                    using (var _stream = file.OpenReadStream())
                                                    {
                                                        var uploadedImage = Image.FromStream(_stream);
                                                        var dimensions =
                                                            uploadedImage.PhysicalDimension.Width + "x" +
                                                            uploadedImage.PhysicalDimension.Height;
                                                        obj.Dimensions = dimensions;
                                                        _fileUploadBCL.Update(obj);

                                                        var thumbPathToSave = Path.Combine(webRoot, serverPath, "thumb",
                                                            folder);
                                                        if (!Directory.Exists(thumbPathToSave))
                                                        {
                                                            Directory.CreateDirectory(thumbPathToSave);
                                                        }

                                                        var thumbFileDirectory = Path.Combine(folder);
                                                        thumbFileDirectory = thumbFileDirectory.Replace("/", "\\");

                                                        var thumbPath =
                                                            "wwwroot\\" + serverPath + "\\thumb\\" +
                                                            thumbFileDirectory +
                                                            "\\" + fileName;
                                                        if (ext.ToLower().Equals(".png"))
                                                        {


                                                            //  postedFile.SaveAs(fileDirectory);
                                                            Bitmap origBMP = new Bitmap(uploadedImage);
                                                            if (origBMP.Width <= imageScaleWidth)
                                                            {
                                                                origBMP.Save(thumbPath);


                                                            }
                                                            else
                                                            {
                                                                int origWidth = origBMP.Width;
                                                                int origHeight = origBMP.Height;
                                                                int newWidth = imageScaleWidth;
                                                                int newHeight = newWidth * origHeight / origWidth;

                                                                Bitmap newBMP = new Bitmap(origBMP, newWidth, newHeight);
                                                                Graphics objGra = Graphics.FromImage(newBMP);
                                                                objGra.DrawImage(origBMP, new Rectangle(0, 0, newBMP.Width, newBMP.Height), 0, 0, origWidth, origHeight, GraphicsUnit.Pixel);
                                                                objGra.Dispose();
                                                                newBMP.Save(thumbPath);
                                                                newBMP.Dispose();
                                                                write_watermark_text(thumbPath, true);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (imageScaleWidth > 0 && imageScaleHeight > 0)
                                                            {
                                                                var img = ImageResize.Scale(uploadedImage, 200, 100);
                                                                img.SaveAs(@"" + thumbPath + "");
                                                                img.Dispose();
                                                                write_watermark_text(thumbPath, false);
                                                            }

                                                            if (imageScaleWidth > 0 && imageScaleHeight == 0)
                                                            {
                                                                var img = ImageResize.ScaleByWidth(uploadedImage,
                                                                    imageScaleWidth);
                                                                img.SaveAs(@"" + thumbPath + "");
                                                                img.Dispose();
                                                                write_watermark_text(thumbPath, false);
                                                            }

                                                        }
                                                    }
                                                }


                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            FileInfo fileInfo = new FileInfo
                                            {
                                                name = fileName,
                                                path = "/",
                                                ext = ext,
                                                size = file.Length / 1024, //Math.Round((double)file.Length / 1024, 1),
                                                code = 500,
                                                messages = ex.Message

                                            };
                                            uploadResults.Add(fileInfo);
                                        }


                                    }
                                    write_watermark_text(fullPath);


                                    write_watermark_text(fullPath,false);

                                }
                                else
                                {
                                    FileInfo fileInfo = new FileInfo
                                    {
                                        name = fileName,
                                        path = "/",
                                        ext = ext,
                                        size = file.Length / 1024,
                                        code = 900,
                                        messages = "File đã tồn tại trên hệ thống"

                                    };
                                    uploadResults.Add(fileInfo);
                                }
                            }
                            else
                            {
                                FileInfo fileInfo = new FileInfo
                                {
                                    name = fileName,
                                    path = "/",
                                    ext = ext,
                                    size = file.Length / 1024,
                                    code = 900,
                                    messages = "Limited file size"

                                };
                                uploadResults.Add(fileInfo);
                            }

                        }
                        else
                        {
                            FileInfo fileInfo = new FileInfo
                            {
                                name = file.FileName,
                                path = "/",
                                ext = ext,
                                size = file.Length / 1024,
                                code = 900,
                                messages = "File extension not allowed"

                            };
                            uploadResults.Add(fileInfo);

                        }
                    }
                    res.Data = uploadResults;
                    res.Success = true;
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


        [Route("UploadExcel")]
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadExcel(List<IFormFile> files)
        {
            string serverPath = _config.GetValue<string>("AppSettings:UploadFolder");
            string folder = string.Format("{0:yyyy/MM/dd}", DateTime.Now);
            var res = new ResponseData();
            try
            {

                var webRoot = _env.WebRootPath;
                var fileDirectory = Path.Combine(serverPath, folder);
                var pathToSave = Path.Combine(webRoot, fileDirectory);

                if (Request.Form.Files.Count > 0)
                {
                    var uploadResults = new List<FileInfo>();
                    string imageAllowUpload = _config.GetValue<string>("AppSettings:ImageAllowUpload");
                    string documentAllowUpload = _config.GetValue<string>("AppSettings:DocumentAllowUpload");
                    bool fileUploadSubFix = _config.GetValue<bool>("AppSettings:FileUploadSubFix");
                    int fileUploadMaxSize = _config.GetValue<int>("AppSettings:FileUploadMaxSize");
                    int imageScaleWidth = _config.GetValue<int>("AppSettings:ImageScaleWidth");
                    int imageScaleHeight = _config.GetValue<int>("AppSettings:ImageScaleHeight");
                    foreach (var file in Request.Form.Files)
                    {
                        var ext = Path.GetExtension(fileDirectory + "\\" + file.FileName);

                        var imageAllowFileArray = imageAllowUpload.Split(',');
                        var documentAllowFileArray = documentAllowUpload.Split(',');
                        if (Array.IndexOf(imageAllowFileArray, ext.ToLower()) != -1 || Array.IndexOf(documentAllowFileArray, ext.ToLower()) != -1)
                        {
                            var fileName = Path.GetFileName(fileDirectory + "\\" + file.FileName);
                            if (fileUploadMaxSize >= (file.Length / 1024))
                            {

                                if (fileUploadSubFix)
                                {
                                    string filenameNoExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);
                                    fileName = Utility.UnicodeToKoDauAndGach(filenameNoExtension) + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + ext;
                                }
                                var fullPath = Path.Combine(pathToSave, fileName);
                                if (!Directory.Exists(pathToSave))
                                {
                                    Directory.CreateDirectory(pathToSave);
                                }

                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    file.CopyTo(stream);

                                }
                                //Doc file

                                var r = _workingFile.ImportProductPriceLocationExcel(fullPath);
                            }
                            else
                            {
                                FileInfo fileInfo = new FileInfo
                                {
                                    name = fileName,
                                    path = "/",
                                    ext = ext,
                                    size = file.Length / 1024,
                                    code = 900,
                                    messages = "Limited file size"

                                };
                                uploadResults.Add(fileInfo);
                            }

                        }
                        else
                        {
                            FileInfo fileInfo = new FileInfo
                            {
                                name = file.FileName,
                                path = "/",
                                ext = ext,
                                size = file.Length / 1024,
                                code = 900,
                                messages = "File extension not allowed"

                            };
                            uploadResults.Add(fileInfo);
                        }



                        res.Success = true;

                    }
                    res.Data = uploadResults;
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


        [Route("UploadExcel_Spectification")]
        [HttpPost]
        public IActionResult UploadExcelMaintainSpectificationInProduct(List<IFormFile> files)
        {
            string serverPath = _config.GetValue<string>("AppSettings:UploadFolder");
            string folder = string.Format("{0:yyyy/MM/dd}", DateTime.Now);
            var res = new ResponseData();
            try
            {

                var webRoot = _env.WebRootPath;
                var fileDirectory = Path.Combine(serverPath, folder);
                var pathToSave = Path.Combine(webRoot, fileDirectory);

                if (Request.Form.Files.Count > 0)
                {
                    var uploadResults = new List<FileInfo>();
                    string imageAllowUpload = _config.GetValue<string>("AppSettings:ImageAllowUpload");
                    string documentAllowUpload = _config.GetValue<string>("AppSettings:DocumentAllowUpload");
                    bool fileUploadSubFix = _config.GetValue<bool>("AppSettings:FileUploadSubFix");
                    int fileUploadMaxSize = _config.GetValue<int>("AppSettings:FileUploadMaxSize");
                    int imageScaleWidth = _config.GetValue<int>("AppSettings:ImageScaleWidth");
                    int imageScaleHeight = _config.GetValue<int>("AppSettings:ImageScaleHeight");
                    foreach (var file in Request.Form.Files)
                    {
                        var ext = Path.GetExtension(fileDirectory + "\\" + file.FileName);

                        var imageAllowFileArray = imageAllowUpload.Split(',');
                        var documentAllowFileArray = documentAllowUpload.Split(',');
                        if (Array.IndexOf(imageAllowFileArray, ext.ToLower()) != -1 || Array.IndexOf(documentAllowFileArray, ext.ToLower()) != -1)
                        {
                            var fileName = Path.GetFileName(fileDirectory + "\\" + file.FileName);
                            if (fileUploadMaxSize >= (file.Length / 1024))
                            {

                                if (fileUploadSubFix)
                                {
                                    string filenameNoExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);
                                    fileName = Utility.UnicodeToKoDauAndGach(filenameNoExtension) + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + ext;
                                }
                                var fullPath = Path.Combine(pathToSave, fileName);
                                if (!Directory.Exists(pathToSave))
                                {
                                    Directory.CreateDirectory(pathToSave);
                                }

                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    file.CopyTo(stream);

                                }
                                //Doc file

                                var r = _workingFile.ImportMMaintainSpectificatinInProduct(fullPath);
                            }
                            else
                            {
                                FileInfo fileInfo = new FileInfo
                                {
                                    name = fileName,
                                    path = "/",
                                    ext = ext,
                                    size = file.Length / 1024,
                                    code = 900,
                                    messages = "Limited file size"

                                };
                                uploadResults.Add(fileInfo);
                            }

                        }
                        else
                        {
                            FileInfo fileInfo = new FileInfo
                            {
                                name = file.FileName,
                                path = "/",
                                ext = ext,
                                size = file.Length / 1024,
                                code = 900,
                                messages = "File extension not allowed"

                            };
                            uploadResults.Add(fileInfo);
                        }



                        res.Success = true;

                    }
                    res.Data = uploadResults;
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



        [HttpDelete("Delete")]
        public ResponseData Delete(int id)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                bool ads = _fileUploadBCL.Remove(id);
                if (ads)
                {
                    responseData.Success = true;
                    responseData.Message = "Thành công";
                }
            }
            catch (Exception ex)
            {

            }
            return responseData;
        }
        [HttpGet("GetAll")]
        public ResponseData GetAll(int take, int pageIndex, string keyword)
        {
            ResponseData responseData = new ResponseData();
            try
            {
                if (string.IsNullOrEmpty(keyword))
                    keyword = "";
                if (keyword.Equals("undefined"))
                    keyword = "";
                int skip = pageIndex > 1 ? ((take * pageIndex) - take) : 0;
                var totals = 0;
                var objs = _fileUploadBCL.Find(keyword, skip, take, out totals);

                //var objs = new List<FileUpload>();
                //objs = _fileUploadBCL.FindAll();

                //if (!String.IsNullOrEmpty(keyword))
                //{
                //    keyword = keyword.ToLower();
                //    objs = _fileUploadBCL.FindAll(x => x.Name.ToLower() == keyword || x.Name.ToLower().Contains(keyword));
                //}

                //var totals = objs.Count();

                var data = objs.OrderByDescending(it => it.UploadedDate).Skip(skip).Take(take).ToList();

                responseData.TotalRow = totals;
                responseData.Success = true;
                responseData.Message = "Thành công";
                responseData.Data = objs.ToList<object>();

            }
            catch (Exception ex)
            {

            }
            return responseData;
        }


        [Route("UploadFile")]
        [HttpPost, DisableRequestSizeLimit]
        public IActionResult UploadFile(List<IFormFile> files)
        {

            string serverPath = _config.GetValue<string>("AppSettings:UploadFolder");
            string folder = string.Format("{0:yyyy/MM/dd}", DateTime.Now);
            var res = new ResponseData();
            res.Success = false;
            try
            {

                var webRoot = _env.WebRootPath;
                var fileDirectory = Path.Combine(serverPath, folder);
                var pathToSave = Path.Combine(webRoot, fileDirectory);

                if (Request.Form.Files.Count > 0)
                {
                    var uploadResults = new List<FileInfo>();
                    string imageAllowUpload = _config.GetValue<string>("AppSettings:ImageAllowUpload");
                    string documentAllowUpload = _config.GetValue<string>("AppSettings:DocumentAllowUpload");
                    bool fileUploadSubFix = _config.GetValue<bool>("AppSettings:FileUploadSubFix");
                    int fileUploadMaxSize = _config.GetValue<int>("AppSettings:FileUploadMaxSize");
                    int imageScaleWidth = _config.GetValue<int>("AppSettings:ImageScaleWidth");
                    int imageScaleHeight = _config.GetValue<int>("AppSettings:ImageScaleHeight");
                    foreach (var file in Request.Form.Files)
                    {
                        var ext = Path.GetExtension(fileDirectory + "\\" + file.FileName);

                        var imageAllowFileArray = imageAllowUpload.Split(',');
                        var documentAllowFileArray = documentAllowUpload.Split(',');
                        if (Array.IndexOf(imageAllowFileArray, ext.ToLower()) != -1 || Array.IndexOf(documentAllowFileArray, ext.ToLower()) != -1)
                        {
                            var fileName = Path.GetFileName(fileDirectory + "\\" + file.FileName);
                            if (fileUploadMaxSize >= (file.Length / 1024))
                            {

                                if (fileUploadSubFix)
                                {
                                    string filenameNoExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);
                                    fileName = Utility.UnicodeToKoDauAndGach(filenameNoExtension) + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + ext;
                                }
                                var fullPath = Path.Combine(pathToSave, fileName);
                                if (!Directory.Exists(pathToSave))
                                {
                                    Directory.CreateDirectory(pathToSave);
                                }


                                using (var stream = new FileStream(fullPath, FileMode.Create))
                                {
                                    file.CopyTo(stream);


                                    var obj = new FileUpload();
                                    obj.Name = fileName;
                                    obj.FilePath = "/" + Path.Combine(folder).Replace("\\", "/") + "/" + fileName;
                                    obj.FileExt = ext;
                                    //   obj.Dimensions = dimensions;
                                    obj.FileSize = file.Length / 1024;
                                    obj.Status = 1;
                                    obj.Type = 2;
                                    //uploadResults.Add(obj);
                                    ResponseData responseData = new ResponseData();
                                    try
                                    {
                                        bool result = true;
                                        if (result)
                                        {

                                            responseData.Success = true;
                                            responseData.Message = "Thành công";
                                            FileInfo fileInfo = new FileInfo
                                            {
                                                name = fileName,
                                                path = "/" + Path.Combine(folder).Replace("\\", "/") + "/" + fileName,
                                                ext = ext,
                                                size = file.Length / 1024,//Math.Round((double)file.Length / 1024, 1),
                                                code = 200,
                                                messages = "Upload thành công"

                                            };
                                            uploadResults.Add(fileInfo);
                                            if (Array.IndexOf(imageAllowFileArray, ext.ToLower()) != -1)
                                            {
                                                using (var _stream = file.OpenReadStream())
                                                {
                                                    var uploadedImage = Image.FromStream(_stream);
                                                    var dimensions =
                                                        uploadedImage.PhysicalDimension.Width + "x" +
                                                        uploadedImage.PhysicalDimension.Height;
                                                    obj.Dimensions = dimensions;
                                                    //_fileUploadBCL.Update(obj);

                                                    var thumbPathToSave = Path.Combine(webRoot, serverPath, "thumb",
                                                        folder);
                                                    if (!Directory.Exists(thumbPathToSave))
                                                    {
                                                        Directory.CreateDirectory(thumbPathToSave);
                                                    }

                                                    var thumbFileDirectory = Path.Combine(folder);
                                                    thumbFileDirectory = thumbFileDirectory.Replace("/", "\\");

                                                    var thumbPath =
                                                        "wwwroot\\" + serverPath + "\\thumb\\" +
                                                        thumbFileDirectory +
                                                        "\\" + fileName;
                                                    if (ext.ToLower().Equals(".png"))
                                                    {


                                                        //  postedFile.SaveAs(fileDirectory);
                                                        Bitmap origBMP = new Bitmap(uploadedImage);
                                                        if (origBMP.Width <= imageScaleWidth)
                                                        {
                                                            origBMP.Save(thumbPath);

                                                        }
                                                        else
                                                        {
                                                            int origWidth = origBMP.Width;
                                                            int origHeight = origBMP.Height;
                                                            int newWidth = imageScaleWidth;
                                                            int newHeight = newWidth * origHeight / origWidth;

                                                            Bitmap newBMP = new Bitmap(origBMP, newWidth, newHeight);
                                                            Graphics objGra = Graphics.FromImage(newBMP);
                                                            objGra.DrawImage(origBMP, new Rectangle(0, 0, newBMP.Width, newBMP.Height), 0, 0, origWidth, origHeight, GraphicsUnit.Pixel);
                                                            objGra.Dispose();
                                                            newBMP.Save(thumbPath);

                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (imageScaleWidth > 0 && imageScaleHeight > 0)
                                                        {
                                                            var img = ImageResize.Scale(uploadedImage, 200, 100);
                                                            img.SaveAs(@"" + thumbPath + "");
                                                        }

                                                        if (imageScaleWidth > 0 && imageScaleHeight == 0)
                                                        {
                                                            var img = ImageResize.ScaleByWidth(uploadedImage,
                                                                imageScaleWidth);
                                                            img.SaveAs(@"" + thumbPath + "");
                                                        }

                                                    }
                                                }
                                            }


                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        FileInfo fileInfo = new FileInfo
                                        {
                                            name = fileName,
                                            path = "/",
                                            ext = ext,
                                            size = file.Length / 1024, //Math.Round((double)file.Length / 1024, 1),
                                            code = 500,
                                            messages = ex.Message

                                        };
                                        uploadResults.Add(fileInfo);
                                    }


                                }

                            }
                            else
                            {
                                FileInfo fileInfo = new FileInfo
                                {
                                    name = fileName,
                                    path = "/",
                                    ext = ext,
                                    size = file.Length / 1024,
                                    code = 900,
                                    messages = "Limited file size"

                                };
                                uploadResults.Add(fileInfo);
                            }

                        }
                        else
                        {
                            FileInfo fileInfo = new FileInfo
                            {
                                name = file.FileName,
                                path = "/",
                                ext = ext,
                                size = file.Length / 1024,
                                code = 900,
                                messages = "File extension not allowed"

                            };
                            uploadResults.Add(fileInfo);

                        }
                    }
                    res.Data = uploadResults;
                    res.Success = true;
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
        private void write_watermark_text(string sourceImagePath,bool isThumb = false)
        {
            try
            {

                Image img = Image.FromFile(sourceImagePath);

                int width = img.Width;
                int height = img.Height;


                Point text_starting_point = new Point(height/6, width/4);


                Image watermark = Image.FromFile(Path.Combine(_env.WebRootPath, !isThumb ? "watermark.jpg" : "watermark-thumb.jpg"));


                float height_wtm = (watermark.Height);
                float width_wtm = watermark.Width;

                // de watermark o vi tri height = 90% anh goc
                float height_wtk_position = ((float)((height - height_wtm)*(0.9)));
                Graphics graphics = Graphics.FromImage(img);
                if (!isThumb)
                {
                    height_wtm = ((float)(height * (0.1)));
                    height_wtk_position = ((float)((height - height_wtm) * (0.9)));
                }
                graphics.DrawImage(watermark, 0, height_wtk_position, width,height_wtm);
                //graphics.DrawString(text, text_font, brush, text_starting_point);
                graphics.Dispose();

                var image_2 = new Bitmap(img);

                img.Dispose();

                if (System.IO.File.Exists(sourceImagePath))
                    System.IO.File.Delete(sourceImagePath);

                image_2.Save(sourceImagePath);
                image_2.Dispose();
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
    public class FileInfo
    {
        public int code { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public long size { get; set; }
        public string ext { get; set; }
        public string messages { get; set; }
    }
}



