using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Web;

using Newtonsoft.Json;

namespace BMLed.CMS.Modules.Upload.Action
{
    //public class FileManagerServices : IHttpHandler
    //{
    //    #region IHttpHandler Members

    //    public bool IsReusable
    //    {
    //        get { return false; }
    //    }
    //    private  string root = CmsChannelConfiguration.GetAppSetting("StorageUrl");
    //    private string thumb = CmsChannelConfiguration.GetAppSetting("StorageThumb");
    //    private string ThumbSize = CmsChannelConfiguration.GetAppSetting("ThumbSize");
    //    private string maxUploadFileSize = CmsChannelConfiguration.GetAppSetting("MaxUploadFileSize");
    //    private const string SECRET_KEY = "sdfui2y38efo23r89y8we99032u98we";
    //    private const string FILE_ALLOW = ".jpg,.jpeg,.png,.gif,.bit";
    //    public void ProcessRequest(HttpContext context)
    //    {


    //        string path = context.Server.MapPath("~/CORS.txt");
    //        var lines = File.ReadAllLines(path);
    //        List<string> allowedOrigins = new List<string>();
    //        for (int i = 0; i < lines.Length; i++)
    //        {
    //            allowedOrigins.Add(lines[i]);
    //        }
    //        string origin = context.Request.Headers.Get("Origin");
    //        string host = context.Request.Headers.Get("Host");
    //        if ((origin != null && allowedOrigins.Contains(origin)) || (origin != null && context.Request.IsLocal))
    //        {
    //            context.Response.AppendHeader("Access-Control-Allow-Origin", origin);
    //        }
    //        else if (host != null && allowedOrigins.Contains("http://" + host) || context.Request.IsLocal)
    //        {
    //            context.Response.AppendHeader("Access-Control-Allow-Origin", host);
    //        }
    //        context.Response.AddHeader("Vary", "Accept");
    //        try
    //        {
    //            if (context.Request["HTTP_ACCEPT"].Contains("application/json"))
    //                context.Response.ContentType = "application/json";
    //            else
    //                context.Response.ContentType = "text/plain";
    //        }
    //        catch (Exception ex)
    //        {
    //            context.Response.ContentType = "application/json";
    //        }
    //        if (context.Request.RequestType.Equals("OPTIONS")) return;
    //        try
    //        {
    //            switch (context.Request["fn"])
    //            {
    //                case "upload":
    //                    Upload(context);
    //                    break;
    //                case "link":
    //                    Download(context);
    //                    break;
    //                case "upload_base64":
    //                    UploadBase64(context);
    //                    break;
    //                case "get_policy":
    //                    GetPolicy(context);
    //                    break;
    //            }
    //        }
    //        catch (Exception exp)
    //        {
    //            context.Response.Write(exp.Message);
    //        }
    //    }

    //    protected void Upload(HttpContext context)
    //    {
    //        string Serverpath = "";
           
    //     //   folder = string.Format("{0:yyyy/MM/dd}", DateTime.Now);
    //        Serverpath = Path.Combine(HttpContext.Current.Server.MapPath("/"+root));
    //        var postedFile = context.Request.Files[0];
    //        string file;
    //        //For IE to get file name
    //        if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE")
    //        {
    //            string[] files = postedFile.FileName.Split(new char[] { '\\' });
    //            file = files[files.Length - 1];
    //        }
    //        else
    //        {
    //            file = postedFile.FileName;
    //        }
         
    //        if (!Directory.Exists(Serverpath))
    //            Directory.CreateDirectory(Serverpath);

    //        string fileDirectory = Serverpath;


    //        string ext = Path.GetExtension(fileDirectory + "\\" + file);
    //        // check ext
    //        decimal fileSize = context.Request.Files[0].ContentLength;
    //        string size = Math.Round((fileSize / 1024000), 1).ToString(CultureInfo.InvariantCulture);// MB
    //        var allowFileArray = FILE_ALLOW.Split(',');
    //        if (Array.IndexOf(allowFileArray, ext.ToLower()) == -1)
    //        {
    //            FileInfo fileInfo = new FileInfo
    //            {
    //                name = file,
    //                ext = ext,
    //                size = size,
    //                error = 10001,
    //                success = false,
    //                messages = "Định dạng ảnh không hợp lệ."
    //            };
    //            context.Response.Write(JsonConvert.SerializeObject(fileInfo));
    //            return;
    //        }
    //        // check size
    //        if (fileSize > Int32.Parse(maxUploadFileSize))
    //        {
    //            FileInfo fileInfo = new FileInfo
    //            {
    //                name = file,
    //                ext = ext,
    //                size = size,
    //                error = 10002,
    //                success = false,
    //                messages = "Dung lượng ảnh quá lớn."
    //            };
    //            context.Response.Write(JsonConvert.SerializeObject(fileInfo));
    //            return;
    //        }


    //        if (string.IsNullOrEmpty(file))
    //        {
    //            file = Guid.NewGuid() + ext;
    //        }
    //        else
    //        {
                
    //            file = UnicodeToKoDauAndGach(Path.GetFileNameWithoutExtension(file).Replace(ext, "")) + ext;
    //            // check duplicate
    //            if (FileUploadBo.FolderCheckIsExist(0, file, Convert.ToInt32(context.Request["fid"])) > 0)
    //            {

    //                file = Path.GetFileNameWithoutExtension(file).Replace(ext, "") + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond + ext;
    //            }
    //        }


    //        fileDirectory = Serverpath + "\\" + file;

    //        try
    //        {
    //            postedFile.SaveAs(fileDirectory);

    //            Bitmap origBMP = new Bitmap(Image.FromStream(postedFile.InputStream, true, true));
    //            int max = int.Parse(ThumbSize);
    //            if (origBMP.Width <= max)
    //            {
    //                origBMP.Save(Serverpath + thumb + file);
    //               // return;
    //            }
    //            else
    //            {
    //                int origWidth = origBMP.Width;
    //                int origHeight = origBMP.Height;
    //                int newWidth = max;
    //                int newHeight = newWidth * origHeight / origWidth;

    //                Bitmap newBMP = new Bitmap(origBMP, newWidth, newHeight);
    //                Graphics objGra = Graphics.FromImage(newBMP);
    //                objGra.DrawImage(origBMP, new Rectangle(0, 0, newBMP.Width, newBMP.Height), 0, 0, origWidth, origHeight, GraphicsUnit.Pixel);
    //                objGra.Dispose();
    //                newBMP.Save(Serverpath + thumb + file);
    //            }


    //            FileInfo fileInfo = new FileInfo
    //            {
    //                name = file,
    //                folder = "/" + root,
    //                path = "/" + root + "/" + file,
    //                ext = ext,
    //                size = size,
    //                error = 200,
    //                success = true,
    //                messages = "Tải ảnh lên thành công."

    //            };
    //            context.Response.Write(JsonConvert.SerializeObject(fileInfo));
    //        }
    //        catch (Exception ex)
    //        {
    //            string exs = ex.Message;

    //        }

    //    }
      
    //    protected void UploadBase64(HttpContext context)
    //    {
    //        var img = context.Request.Form["img"];
    //        var imageName = context.Request.Form["name"];
    //     //   string pathrefer = context.Request.UrlReferrer.ToString();
    //        string serverpath = "";
    //        string folder;

    //        //var policy = CheckPolicy(context);
    //        //if (policy.expire == 0)
    //        //{
    //        //    context.Response.Write("policy expired!!!!");
    //        //    return;
    //        //}
    //        //var fileName = policy.filename;
    //        //var userPath = policy.user;
    //        //if (userPath != null)
    //        //{
    //        //    folder = userPath + "/" + string.Format("{0:yyyy/MM/dd}", DateTime.Now);
    //        //}
    //        //else
    //        //{
    //        //    folder = string.Format("{0:yyyy/MM/dd}", DateTime.Now);
    //        //}
    //        var obj = img.Split(',');
    //        var contentType = obj[0].Split(';')[0];
    //        var imageFormat = string.Empty;
    //        switch (contentType)
    //        {
    //            case "data:image/png":
    //                imageFormat = ".jpg";
    //                break;
    //            case "data:image/jpeg":
    //                imageFormat = ".jpeg";
    //                break;
    //            case "data:image/jpg":
    //                imageFormat = ".jpeg";
    //                break;
    //            case "data:image/gif":
    //                imageFormat = ".gif";
    //                break;
    //        }

    //        string fileDirectory = serverpath;
    //        if (string.IsNullOrEmpty(imageName))
    //        {
    //            imageName = Guid.NewGuid() + ".png";
    //        }
    //        else
    //        {
    //            if (File.Exists(fileDirectory + "\\" + imageName))
    //            {
    //                File.Delete(fileDirectory + "\\" + imageName);
    //            }
    //            //  string ext = Path.GetExtension(fileDirectory + "\\" + imageName);
    //            imageName = UnicodeToKoDauAndGach(imageName + "-" + DateTime.Now.Ticks);
    //        }
    //        if (!Directory.Exists(serverpath))
    //            Directory.CreateDirectory(serverpath);



    //        fileDirectory = serverpath + "\\" + imageName + imageFormat;

    //        //     byte[] imageBytes = Convert.FromBase64String(fileName);



    //        File.WriteAllBytes(fileDirectory, Convert.FromBase64String(obj[1]));


    //        FileInfo fileInfo = new FileInfo
    //        {
    //            name = imageName,
    //            folder = "/" + root,
    //            path = "/" + root + "/" + "/" + imageName + imageFormat,
    //            error = 200,
    //            success = true,
    //            messages = "Tải ảnh lên thành công."

    //        };
    //        context.Response.Write(JsonConvert.SerializeObject(fileInfo));
    //    }
    //    public void Download(HttpContext context)
    //    {
    //        try
    //        {

    //            var url = context.Request.Form["url"];
    //            if (url.StartsWith("http://") || url.StartsWith("https://"))
    //            {


    //                var request = (HttpWebRequest)WebRequest.Create(url);
    //                request.Method = "GET";
    //                request.MaximumAutomaticRedirections = 1;
    //                request.AllowAutoRedirect = true;
    //                request.Referer = url;
    //                request.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5";
    //                using (WebResponse response = request.GetResponse())

    //                using (Stream stream = response.GetResponseStream())
    //                {

    //                    string imageType = string.Empty;
    //                    switch (response.ContentType)
    //                    {
    //                        case "image/jpeg":
    //                            imageType = ".jpeg";
    //                            break;
    //                        case "image/png":
    //                            imageType = ".png";
    //                            break;
    //                        case "image/jpg":
    //                            imageType = ".jpg";
    //                            break;
    //                        case "image/gif":
    //                            imageType = ".gif";
    //                            break;
    //                        default:
    //                            imageType = string.Empty;
    //                            break;
    //                    }

    //                    if (string.IsNullOrEmpty(imageType))
    //                    {
    //                        FileInfo fileInfo = new FileInfo
    //                        {
    //                            name = "",
    //                            ext = "",
    //                            size = "",
    //                            error = 10002,
    //                            success = false,
    //                            messages = "Đường dẫn ảnh không hợp lệ hoặc bị chặn"
    //                        };
    //                        context.Response.Write(JsonConvert.SerializeObject(fileInfo));
    //                        return;
    //                    }


    //                    string filename = "";
    //                    if (!Directory.Exists(root))
    //                        Directory.CreateDirectory(root);

    //                    string fileDirectory = root;
    //                    string path = response.Headers["Content-Disposition"];
    //                    if (string.IsNullOrWhiteSpace(path))
    //                    {
    //                        var uri = new Uri(url);
    //                        filename = Path.GetFileName(uri.LocalPath);
    //                    }
    //                    else
    //                    {
    //                        ContentDisposition contentDisposition = new ContentDisposition(path);
    //                        filename = contentDisposition.FileName;

    //                    }
    //                    filename = DateTime.Now.Ticks + "_" + filename;
    //                    string size = Math.Round(((decimal)response.ContentLength / 1024000), 1).ToString(CultureInfo.InvariantCulture);// MB
    //                    if ((decimal)response.ContentLength > Int32.Parse(maxUploadFileSize))
    //                    {
    //                        FileInfo fileInfo = new FileInfo
    //                        {
    //                            name = filename,
    //                            ext = imageType,
    //                            size = size,
    //                            error = 10002,
    //                            success = false,
    //                            messages = "Dung lượng ảnh quá lớn."
    //                        };
    //                        context.Response.Write(JsonConvert.SerializeObject(fileInfo));
    //                        return;
    //                    }
    //                    using (var fileStream = File.Create(System.IO.Path.Combine(fileDirectory, filename)))
    //                    {

    //                        stream.CopyTo(fileStream);
    //                        FileInfo fileInfo = new FileInfo
    //                        {
    //                            name = filename,
    //                            folder = "/" + root ,
    //                            path = "/" + root +"/"+ filename,
    //                            ext = imageType,
    //                            size = size,
    //                            error = 200,
    //                            success = true,
    //                            messages = "Tải ảnh lên thành công."

    //                        };
    //                        context.Response.Write(JsonConvert.SerializeObject(fileInfo));


    //                    }

    //                }
    //            }
    //            else
    //            {
    //                FileInfo fileInfo = new FileInfo
    //                {
    //                    name = "",
    //                    ext = "",
    //                    size = "",
    //                    error = 10001,
    //                    success = false,
    //                    messages = "Nhập đường dẫn ảnh."
    //                };
    //                context.Response.Write(JsonConvert.SerializeObject(fileInfo));

    //            }

    //        }
    //        catch (Exception)
    //        {
    //            FileInfo fileInfo = new FileInfo
    //            {
    //                name = "",
    //                ext = "",
    //                size = "",
    //                error = 10002,
    //                success = false,
    //                messages = "Đường dẫn ảnh không hợp lệ hoặc bị chặn."
    //            };
    //            context.Response.Write(JsonConvert.SerializeObject(fileInfo));

    //        }
    //    }
    //    protected void GetPolicy(HttpContext context)
    //    {
    //        var filename = HttpContext.Current.Request["filename"];
    //        var user = HttpContext.Current.Request["user"];
    //        //sau ma hoa data nay.
    //        var policy = new policy
    //        {
    //            filename = filename,
    //            user = user,
    //            expire = DateTime.Now.AddMinutes(3).Ticks
    //        };
    //        string policyStr = Newtonsoft.Json.JsonConvert.SerializeObject(policy);
    //        policyStr = Crypton.EncryptByKey(policyStr, SECRET_KEY);

    //        context.Response.Write(JsonConvert.SerializeObject(new policyResult
    //        {
    //            filename = filename,
    //            data = policyStr
    //        }));
    //    }
    //    protected policy CheckPolicy(HttpContext context)
    //    {
    //        policy p = new policy();
    //        var policyStr = context.Request["policy"];
    //        var de = Crypton.DecryptByKey(policyStr, SECRET_KEY);
    //        p = JsonConvert.DeserializeObject<policy>(de);
    //        var ticks = new DateTime(p.expire);
    //        if (DateTime.Now > ticks)
    //        {
    //            p = new policy
    //            {
    //                expire = 0
    //            };
    //        }
    //        return p;
    //    }
    //    #endregion
    //    public const string UniChars =
    //  "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
    //    public const string UnsignChars =
    //        "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";


    //    public static string UnicodeToKoDau(string s)
    //    {
    //        string retVal = string.Empty;
    //        for (int i = 0; i < s.Length; i++)
    //        {
    //            int pos = UniChars.IndexOf(s[i].ToString());
    //            if (pos >= 0)
    //                retVal += UnsignChars[pos];
    //            else
    //                retVal += s[i];
    //        }
    //        return retVal.ToLower();
    //    }

    //    public static string UnicodeToKoDauAndGach(string s)
    //    {
    //        const string strChar = "abcdefghijklmnopqrstxyzuvxw0123456789- ";
    //        s = UnicodeToKoDau(s.ToLower().Trim());
    //        s = Regex.Replace(s, "[^a-zA-Z0-9]+", "-");
    //        s = Regex.Replace(s, "[-]+", "-");
    //        return s.Trim('-');
    //    }
    //}

    public class policy
    {
        public string filename { get; set; }
        public string user { get; set; }
        public long expire { get; set; }
    }
    public class policyResult
    {
        public string filename { get; set; }
        public string data { get; set; }
    }
    public class FileInfo
    {
        public bool success { get; set; }
        public string name { get; set; }
        public string folder { get; set; }
        public string path { get; set; }
        public string size { get; set; }
        public string ext { get; set; }
        public int error { get; set; }
        public string messages { get; set; }
    }
}

