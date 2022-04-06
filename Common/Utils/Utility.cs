using MI.Entity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Utils
{

    public static class IQueryableExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderBy(ToLambda<T>(propertyName));
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderByDescending(ToLambda<T>(propertyName));
        }

        private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }
    }


    public class Utility
    {
        public IHostingEnvironment Eviroment;
        public static string ErrorMessage = "Cập nhật thất bại.";
        public static string SuccessMessage = "Cập nhật thành công.";
        public static string DefaultLang = "vi-VN";
        public static DateTime DateMerge = DateTime.MinValue;

        public static string fomatDate = "dd/MM/yyyy HH:mm";
        public static string DecodeHtml(string html)
        {

            return HttpUtility.HtmlDecode(html);
        }

        public static Dictionary<string, object> ConvertDictionary(string dic)
        {
            var result = new Dictionary<string, object>();
            try
            {
                result = JsonConvert.DeserializeObject<Dictionary<string, object>>(dic);
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public static List<string> SplitStr(List<string> SplitString)
        {
            List<string> lst = new List<string>();

            foreach (var item in SplitString)
            {
                var str = item.ToLower();
                var split = str.Split(" ");
                var split2 = TiengVietKhongDau(str).Split(" ");
                for (int i = 0; i < split.Length; i++)
                {
                    lst.Add(string.Join(" ", split.Skip(i).Take(split.Length)));
                    lst.Add(string.Join(" ", split2.Skip(i).Take(split.Length)));
                }
            }
            return lst.Distinct().ToList();
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            const string pattern = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

            return Regex.IsMatch(email, pattern);
        }

        public static string TagImg(string strSrc, string alt = "", string strClass = "", string strId = "", string attr = "")
        {
            return $"<img src='{strSrc}' alt='{alt}' class='{strClass} id='{strId}' {attr} />";
        }




        public static string SplitName(string url)
        {
            if (!String.IsNullOrEmpty(url))
                url = String.Join(" ", SplitStringToList(url, new string[] { " " }).Select(x => x.Substring(0, 1)));
            else
                url = "E B";
            return url;
        }

        public static KeyValuePair<bool, string> AutoCheckSlug(string title, string url)
        {
            KeyValuePair<bool, string> result = new KeyValuePair<bool, string>(false, "Không được để trống tiêu đề");
            if (!String.IsNullOrWhiteSpace(title))
            {
                if (!String.IsNullOrEmpty(url))
                {

                    url = ConvertTextToLink(url);
                    result = new KeyValuePair<bool, string>(true, url);
                }
                else
                {
                    url = ConvertTextToLink(title);
                    result = new KeyValuePair<bool, string>(true, url);

                }
            }
            return result;
        }

        public static string GenerateSlug(string title)
        {
            var slug = "";
            if (!String.IsNullOrEmpty(title))
            {
                // Change to lower case
                var titleLower = title.ToLower();
                // Letter "e"

                slug = Regex.Replace(titleLower, @"e|é|è|ẽ|ẻ|ẹ|ê|ế|ề|ễ|ể|ệ", "e", RegexOptions.IgnoreCase);
                // Letter "a"
                slug = Regex.Replace(slug, @"a|á|à|ã|ả|ạ|ă|ắ|ằ|ẵ|ẳ|ặ|â|ấ|ầ|ẫ|ẩ|ậ", "a", RegexOptions.IgnoreCase);
                // Letter "o"
                slug = Regex.Replace(slug, @"o|ó|ò|õ|ỏ|ọ|ô|ố|ồ|ỗ|ổ|ộ|ơ|ớ|ờ|ỡ|ở|ợ", "o", RegexOptions.IgnoreCase);
                // Letter "u"
                slug = Regex.Replace(slug, @"u|ú|ù|ũ|ủ|ụ|ư|ứ|ừ|ữ|ử|ự", "u", RegexOptions.IgnoreCase);
                // Letter "d"
                slug = Regex.Replace(slug, @"đ", "d", RegexOptions.IgnoreCase);
                // Trim the last whitespace
                slug = Regex.Replace(slug, @"\s*$", "", RegexOptions.IgnoreCase);
                // Change whitespace to "-"
                slug = Regex.Replace(slug, @"\s+", "-", RegexOptions.IgnoreCase);
            }
            return slug;
        }

        public static string GenerateCode(string phrase)
        {
            string str = RemoveAccent(phrase);
            // invalid chars           
            str = Regex.Replace(str, @"[^a-zA-Z0-9\s-_]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "_"); // hyphens   
            return str;
        }

        public static string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        #region Convert data

        public static string ConvertToString(object value)
        {
            return null == value ? string.Empty : value.ToString();
        }
        public static int ConvertToInt(object value)
        {
            int returnValue;
            if (null == value || !int.TryParse(value.ToString(), out returnValue))
            {
                returnValue = 0;
            }
            return returnValue;
        }
        public static long ConvertToLong(object value)
        {
            long returnValue;
            if (null == value || !long.TryParse(value.ToString(), out returnValue))
            {
                returnValue = 0;
            }
            return returnValue;
        }
        public static short ConvertToShort(object value)
        {
            short returnValue;
            if (null == value || !short.TryParse(value.ToString(), out returnValue))
            {
                returnValue = 0;
            }
            return returnValue;
        }
        public static decimal ConvertToDecimal(object value)
        {
            decimal returnValue;
            if (null == value || !decimal.TryParse(value.ToString(), out returnValue))
            {
                returnValue = 0;
            }
            return returnValue;
        }
        public static double ConvertToDouble(object value)
        {
            double returnValue;
            if (null == value || !double.TryParse(value.ToString(), out returnValue))
            {
                returnValue = 0;
            }
            return returnValue;
        }
        public static DateTime ConvertToDateTime(object value)
        {
            DateTime returnValue;

            //var ci = new CultureInfo("en-US");
            //var dtf = new DateTimeFormatInfo { ShortDatePattern = "dd/MM/yyyy", ShortTimePattern = "HH:mm:ss" };
            //ci.DateTimeFormat = dtf;
            //System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            if (null == value || !DateTime.TryParse(value.ToString(), out returnValue))
            {
                returnValue = DateTime.MinValue;
            }
            return returnValue;
        }

        public static TimeSpan ConvertToTimeSpan(object value)
        {
            TimeSpan timeSpan;
            if (null == value || !TimeSpan.TryParse(value.ToString(), out timeSpan))
            {
                timeSpan = TimeSpan.MinValue;
            }
            return timeSpan;

        }

        public static bool ConvertToBoolean(object value)
        {
            bool returnValue;
            if (null == value || !bool.TryParse(value.ToString(), out returnValue))
            {
                returnValue = false;
            }
            return returnValue;
        }

        public static byte ConvertToByte(object value)
        {
            byte returnValue;
            if (null == value || !byte.TryParse(value.ToString(), out returnValue))
            {
                returnValue = 0;
            }
            return returnValue;
        }

        #endregion

        #region Old functions

        public static long ConvertToInt(object obj, long defaultValue = 0)
        {
            long result;
            return long.TryParse(obj.ToString(), out result) ? result : defaultValue;
        }

        public static int ConvertToInt(object obj, int defaultValue = 0)
        {
            int result;
            return int.TryParse(obj.ToString(), out result) ? result : defaultValue;
        }

        public static string JavaScriptSring(string input)
        {
            input = input.Replace("'", "\\u0027");
            input = input.Replace("\"", "\\u0022");
            return input;
        }

        public const string UniChars =
       "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
        public const string UnsignChars =
            "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";


        public static string UnicodeToKoDau(string s)
        {
            string retVal = string.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                int pos = UniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += UnsignChars[pos];
                else
                    retVal += s[i];
            }
            return retVal.ToLower();
        }

        public static string ConvertToUnsignChar(string inputString)
        {
            return ConvertTextToLink(inputString, " ");
        }

        public static string UnicodeToKoDauAndGach(string s)
        {
            const string strChar = "abcdefghijklmnopqrstxyzuvxw0123456789- ";
            s = UnicodeToKoDau(s.ToLower().Trim());

            //string sReturn = "";
            //for (int i = 0; i < s.Length; i++)
            //{
            //    if (strChar.IndexOf(s[i]) > -1)
            //    {
            //        if (s[i] != ' ')
            //            sReturn += s[i];
            //        else if (i > 0 && s[i - 1] != ' ' && s[i - 1] != '-')
            //            sReturn += "-";
            //        else
            //            sReturn += "-";
            //    }
            //}
            //return sReturn.Replace("--", "-").ToLower();

            s = Regex.Replace(s, "[^a-zA-Z0-9]+", "-");
            s = Regex.Replace(s, "[-]+", "-");
            return s.Trim('-');
        }

        #endregion

        #region new functions

        public static string AddSlash(string input)
        {
            var str = !string.IsNullOrEmpty(input) ? input.Trim() : "";
            if (str != "")
            {
                str = str.Replace("'", "'").Replace("\"", "\\\"");
            }
            return str;
        }

        public static string ReplaceSpaceToPlus(string input)
        {
            return string.IsNullOrEmpty(input) ? input : Regex.Replace(input, "\\s+", "+", RegexOptions.IgnoreCase);
        }

        public static string ConvertCurentce(string sNumber)
        {
            var num = 3;
            var num2 = 0;
            for (int i = 1; i <= (sNumber.Length / 3); i++)
            {
                if ((num + num2) < sNumber.Length)
                {
                    sNumber = sNumber.Insert((sNumber.Length - num) - num2, ".");
                }
                num += 3;
                num2++;
            }
            return sNumber;
        }

        public static string ConvertTextToLink(string inputString, params string[] paternList)
        {
            if (inputString == "")
            {
                return "";
            }
            var input = inputString.Trim().ToLower();
            var replacement = "-";
            if (paternList.Length > 0)
            {
                replacement = paternList[0];
            }
            var str = input;
            str = Regex.Replace(str, "\x00e1|\x00e0|ạ|ả|\x00e3|ă|ắ|ằ|ặ|ẳ|ẵ|\x00e2|ấ|ầ|ậ|ẩ|ẫ", "a");
            str = Regex.Replace(str, "đ|Đ", "d");
            str = Regex.Replace(str, "\x00f3|\x00f2|ọ|ỏ|\x00f5|ơ|ớ|ờ|ợ|ở|ỡ|\x00f4|ố|ồ|ộ|ổ|ỗ", "o");
            str = Regex.Replace(str, "\x00fa|\x00f9|ụ|ủ|ũ|ư|ứ|ừ|ự|ử|ữ", "u");
            str = Regex.Replace(str, "\x00e9|\x00e8|ẹ|ẻ|ẽ|\x00ea|ế|ề|ệ|ể|ễ", "e");
            str = Regex.Replace(str, "\x00ed|\x00ec|ị|ỉ|ĩ", "i");
            str = Regex.Replace(str, "\x00fd|ỳ|ỵ|ỷ|ỹ", "y");
            str = Regex.Replace(str, "\"|'|_+|\\.+", string.Empty);

            str = Regex.Replace(str, @"[^\w\-]", " ");
            str = Regex.Replace(str, @"-$|-+|\s+", replacement);
            return str;
        }

        public static string ConvertTextToUnsignName(string inputString, params string[] paternList)
        {
            if (inputString == "")
            {
                return "";
            }
            var input = inputString.Trim().ToLower();
            var replacement = " ";
            if (paternList.Length > 0)
            {
                replacement = paternList[0];
            }
            var str = Regex.Replace(input, "^[0-9]+", string.Empty);
            str = Regex.Replace(str, "\x00e1|\x00e0|ạ|ả|\x00e3|ă|ắ|ằ|ặ|ẳ|ẵ|\x00e2|ấ|ầ|ậ|ẩ|ẫ", "a");
            str = Regex.Replace(str, "đ|Đ", "d");
            str = Regex.Replace(str, "\x00f3|\x00f2|ọ|ỏ|\x00f5|ơ|ớ|ờ|ợ|ở|ỡ|\x00f4|ố|ồ|ộ|ổ|ỗ", "o");
            str = Regex.Replace(str, "\x00fa|\x00f9|ụ|ủ|ũ|ư|ứ|ừ|ự|ử|ữ", "u");
            str = Regex.Replace(str, "\x00e9|\x00e8|ẹ|ẻ|ẽ|\x00ea|ế|ề|ệ|ể|ễ", "e");
            str = Regex.Replace(str, "\x00ed|\x00ec|ị|ỉ|ĩ", "i");
            str = Regex.Replace(str, "\x00fd|ỳ|ỵ|ỷ|ỹ", "y");
            str = Regex.Replace(str, "\"|'|_+|\\.+", string.Empty);

            str = Regex.Replace(str, @"[^\w\-]", " ");
            str = Regex.Replace(str, @"-$|-+|\s+", replacement);
            return str;
        }

        public static string QuoteString(string inputString)
        {
            var str = inputString.Trim();
            if (str != "")
            {
                str = str.Replace("'", "''");
            }
            return str;
        }

        public static string RemoveStrHtmlTags(string inputString)
        {
            var input = inputString.Trim();
            if (input != "")
            {
                input = Regex.Replace(input, @"<(.|\n)*?>", string.Empty);
            }
            return input;
        }

        public static string ReplaceSpecialCharater(string inputString)
        {
            inputString = inputString.Trim();
            inputString = inputString.Replace(@"\", @"\\");
            inputString = inputString.Replace("\"", "&quot;");
            inputString = inputString.Replace("“", "&ldquo;");
            inputString = inputString.Replace("”", "&rdquo;");
            inputString = inputString.Replace("‘", "&lsquo;");
            inputString = inputString.Replace("’", "&rsquo;");
            inputString = inputString.Replace("'", "&#39;");
            return inputString;
        }

        public static string WrapString(string inputString, int length)
        {
            var str = string.Empty;
            var startIndex = 0;
            for (var i = (int)Math.Ceiling(inputString.Length / ((double)length)); (startIndex < inputString.Length) && (i > 1); i--)
            {
                str = str + inputString.Substring(startIndex, length) + ' ';
                startIndex += length;
            }
            return (str + inputString.Substring(startIndex));
        }


        public enum Browser { MozilaFirefox = 1, InternetExplorer = 2, AppleSafari = 3, NetScapeOpera = 4, Other = 5 }

        public static bool DetectBrowser(Browser browser, string browserType)
        {
            int result;
            switch (browserType.ToLower())
            {
                case "ie":
                    result = (int)Browser.InternetExplorer;
                    break;
                case "firefox":
                    result = (int)Browser.MozilaFirefox;
                    break;
                case "safari":
                    result = (int)Browser.AppleSafari;
                    break;
                default:
                    result = (int)Browser.Other;
                    break;
            }
            return (int)browser == result;
        }

        public static string CreateMD5Checksum(string data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] checksumBytes = md5.ComputeHash(Encoding.Unicode.GetBytes(data));
            return BitConverter.ToString(checksumBytes).Replace("-", string.Empty);
        }

        public static string GenerateRandomString(int length)
        {
            //Initiate objects & vars    
            var random = new Random();
            var randomString = "";

            //Loop ‘length’ times to generate a random number or character
            for (var i = 0; i < length; i++)
            {
                var randNumber = random.Next(1, 3) == 1 ? random.Next(97, 123) : random.Next(48, 58);

                //append random char or digit to random string
                randomString = randomString + (char)randNumber;
            }
            //return the random string
            return randomString;
        }

        public static string GetMACAddress()
        {
            var nics = NetworkInterface.GetAllNetworkInterfaces();
            var sMacAddress = string.Empty;
            foreach (var adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    var properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

        /// <summary>
        /// Compares two instances of System.DateTime and returns an integer that indicates
        /// where the dateFrom earlier or same as or later the dateTo.<br />
        /// <example>
        /// dateFrom = new DateTime(2009, 8, 1, 0, 0, 0);
        /// dateTo = new DateTime(2009, 8, 1, 12, 0, 0);
        /// result = 1 (dateFrom is earlier than dateTo)
        /// </example> 
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns>integer</returns>
        public static int CompareDiffirenceDate(DateTime dateFrom, DateTime dateTo)
        {
            var date1 = new DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day, dateFrom.Hour, dateFrom.Minute, 0);
            var date2 = new DateTime(dateTo.Year, dateTo.Month, dateTo.Day, dateTo.Hour, dateTo.Minute, 0);
            return DateTime.Compare(date1, date2);
        }

        public static double DateDiff(DateTime dateFrom, DateTime dateTo, string instant)
        {
            var diff = dateTo - dateFrom;
            double result = 0;
            switch (instant.ToLower())
            {
                case "d":
                    result = diff.TotalDays;
                    break;
                case "h":
                    result = diff.TotalHours;
                    break;
                case "m":
                    result = diff.TotalMinutes;
                    break;
                case "s":
                    result = diff.TotalSeconds;
                    break;
            }
            return result;
        }

        public static int CountWords(string stringInput)
        {
            if (string.IsNullOrEmpty(stringInput)) return 0;
            // Replaces all HTML tags
            stringInput = RemoveStrHtmlTags(stringInput);
            // Counts all words
            var collection = Regex.Matches(stringInput, @"[\S]+");
            // Returns number words.
            return collection.Count;
        }

        public static long SetPublishedDate(DateTime dateTime)
        {
            const string formatDate = "yyyyMMddHHmmssFFF";
            var dt = dateTime;
            if (dt <= DateTime.MinValue)
            {
                dt = DateTime.Now;
            }

            var dateToString = dt.ToString(formatDate);
            while (dateToString.Length < formatDate.Length)
            {
                dateToString += "0";
            }
            return long.Parse(dateToString);
        }

        public static long SetPublishedDate()
        {
            const string format = "yyyyMMddHHmmssFFF";
            var stringNewsId = DateTime.Now.ToString(format);
            while (stringNewsId.Length < format.Length)
            {
                stringNewsId += "0";
            }
            return long.Parse(stringNewsId);
        }

        /// <summary>
        /// compress script content
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string StripWhitespace(string content)
        {
            var lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var emptyLines = new StringBuilder();
            foreach (var s in lines.Select(line => line.Trim()).Where(s => s.Length > 0 && !s.StartsWith("//")))
            {
                emptyLines.AppendLine(s.Trim());
            }
            content = emptyLines.ToString();

            // remove C styles comments
            content = Regex.Replace(content, "/\\*.*?\\*/", String.Empty, RegexOptions.Compiled | RegexOptions.Singleline);
            // remove line comments
            //content = Regex.Replace(content, "//.*\r\n", String.Empty, RegexOptions.Compiled | RegexOptions.Singleline);
            var reg = new Regex("//([^/]+)//([^/]+)[\r\t\n]+", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            const string http = "http:";
            content = reg.Replace(content, delegate (System.Text.RegularExpressions.Match m)
            {
                var length = m.Groups[1].Value.Length;
                if (length > http.Length)
                {
                    var h = m.Groups[1].Value.Substring(length - http.Length);
                    return h.Equals(http) ? m.Groups[0].Value : m.Groups[1].Value;
                }
                return m.Groups[1].Value;
            });
            //// trim left
            content = Regex.Replace(content, "^\\s*", String.Empty, RegexOptions.Compiled | RegexOptions.Multiline);
            //// trim right
            content = Regex.Replace(content, "\\s*[\\r\\n]", "\r\n", RegexOptions.Compiled | RegexOptions.ECMAScript);
            // remove whitespace beside of left curly braced
            content = Regex.Replace(content, "\\s*{\\s*", "{", RegexOptions.Compiled | RegexOptions.ECMAScript);
            // remove whitespace beside of coma
            content = Regex.Replace(content, "\\s*,\\s*", ",", RegexOptions.Compiled | RegexOptions.ECMAScript);
            // remove whitespace beside of semicolon
            content = Regex.Replace(content, "\\s*;\\s*", ";", RegexOptions.Compiled | RegexOptions.ECMAScript);
            // remove newline after keywords
            content = Regex.Replace(content, "\\r\\n(?<=\\b(abstract|boolean|break|byte|case|catch|char|class|const|continue|default|delete|do|double|else|extends|false|final|finally|float|for|function|goto|if|implements|import|in|instanceof|int|interface|long|native|new|null|package|private|protected|public|return|short|static|super|switch|synchronized|this|throw|throws|transient|true|try|typeof|var|void|while|with)\\r\\n)", " ", RegexOptions.Compiled | RegexOptions.ECMAScript);
            content = Regex.Replace(content, @"[\n\r]+\s*", string.Empty); // space
            return content;
        }

        public static string SubWordInString(string inputString, int maxWord)
        {
            if (string.IsNullOrEmpty(inputString)) return inputString;
            inputString = Regex.Replace(inputString, "\\s+", " ");
            string[] words = Regex.Split(inputString, " ");
            if (words.Length <= maxWord) return inputString;
            else
            {
                inputString = string.Empty;
                for (int i = 0; i < maxWord; i++)
                {
                    inputString += words[i] + " ";
                }
                return inputString.Trim() + "...";
            }
        }

        #endregion

        public static string BuildZone(List<int> lstObj, Dictionary<int, string> dic)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in lstObj)
            {
                if (dic.ContainsKey(item))
                {
                    sb.Append(dic[item] + "<br/>");
                }
            }

            return sb.ToString();
            ;
        }

        public static string GetLinkImage(IFormFile file_Image, IHostingEnvironment Eviroment, string ForderPath = "Slider")
        {
            string DirectoryPath = "uploads";
            if (file_Image != null)
            {
                string uploadPath = Path.Combine(Eviroment.WebRootPath, DirectoryPath);
                Directory.CreateDirectory(Path.Combine(uploadPath, ForderPath));
                string filename = $"{Path.GetFileNameWithoutExtension(file_Image.FileName)}_{DateTime.Now.ToString("yyyyMMddhhmmss")}{Path.GetExtension(file_Image.FileName)}";
                filename = bodauTiengViet(filename);
                if (filename.Contains('\\'))
                {
                    filename = filename.Split('\\').Last();
                }
                using (FileStream fs = new FileStream(Path.Combine(uploadPath, ForderPath, filename), FileMode.Create))
                {
                    file_Image.CopyToAsync(fs);
                }
                return $"/{DirectoryPath}/{ForderPath}/{filename}";
            }
            return "";
        }

        public static string TiengVietKhongDau(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string bodauTiengViet(string str)
        {
            str = str.ToLower();
            str = Regex.Replace(str, "[áàảãạăắằẳẵặâấầẩẫậ]", "a");
            str = Regex.Replace(str, "[óòỏõọôồốổỗộơớờởỡợ]", "o");
            str = Regex.Replace(str, "[éèẻẽẹêếềểễệ]", "e");
            str = Regex.Replace(str, "[íìỉĩịIÍĨÌỈĨ]", "i");
            str = Regex.Replace(str, "[úùủũụưứừửữự]", "u");
            str = Regex.Replace(str, "[ýỳỷỹỵ]", "y");
            str = Regex.Replace(str, "[dđ]", "d");
            str = Regex.Replace(str, " ", "-");
            str = str.Replace("/", "-");
            return str;
        }
        public static string DomainImage(string image)
        {
            if (!String.IsNullOrEmpty(image))
            {
                if (image.StartsWith("http"))
                    return image;
                else
                    return $"{Settings.AppSettings.FoderImg}{image}";
            }
            else
                return image;
        }
        public static string BaseDomain(string url)
        {
            if (!String.IsNullOrEmpty(url))
            {
                if (url.StartsWith("http"))
                    return url;
                else
                    return $"{Settings.AppSettings.BaseDomain}{url}";
            }
            else
                return url;
        }

        public static string RemoveDomainImage(string image)
        {
            if (!String.IsNullOrEmpty(image))
            {
                return image.Replace(Settings.AppSettings.FoderImg, "");
            }
            else
                return image;
        }
        public static List<string> SplitStringToList(string input, IEnumerable<string> splitChars = null, bool isRemoveEmpty = true, bool chuanHoaItem = false)
        {
            List<string> res = new List<string>();
            try
            {
                if (!String.IsNullOrWhiteSpace(input))
                {
                    if (splitChars == null || splitChars.Count() <= 0)
                        splitChars = new List<string>() { "," };
                    if (isRemoveEmpty)
                        res = input.Split(splitChars.ToArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                    else
                        res = input.Split(splitChars.ToArray(), StringSplitOptions.None).ToList();

                }
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public static List<int> SplitStringToListInt(string input, List<string> splitChars = null)
        {
            List<int> res = new List<int>();
            try
            {
                if (!String.IsNullOrWhiteSpace(input))
                {
                    if (splitChars == null || splitChars.Count <= 0)
                        splitChars = new List<string>() { Environment.NewLine, "\r\n", "\n", "\t", ",", ";", " " };
                    string[] strArr = input.Split(splitChars.ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
                    res = Array.ConvertAll(strArr, int.Parse).ToList();
                }
            }
            catch (Exception ex)
            {
                //log.Error($"{input} {ex}");
            }
            return res;
        }

        public static bool SendMail(string body, string emailCC, string emailmain)
        {
            List<string> lstMailCC = SplitStringToList(emailCC);
            var mailAdress = Settings.AppSettings.GetByKeyMail("UserName");
            var passWord = Settings.AppSettings.GetByKeyMail("Password");
            using (MailMessage mail = new MailMessage())
            {
                try
                {
                    mail.From = new MailAddress(mailAdress);
                    mail.To.Add(emailmain);
                    mail.Subject = "Thông báo Website Enterbuy.vn";
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    foreach (var item in lstMailCC)
                    {
                        mail.CC.Add(item);
                    }

                    using (SmtpClient smtp = new SmtpClient(Settings.AppSettings.GetByKeyMail("Host"), int.Parse(Settings.AppSettings.GetByKeyMail("MailPort"))))
                    {
                        smtp.Credentials = new NetworkCredential(mailAdress, passWord);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                    return true;
                }
                catch (Exception ex)
                {

                    return false;
                }
            }
        }
    }

    //public class WebHelper
    //{
    //    public static string Version()
    //    {
    //        return "1.1";
    //    }
    //}
}
