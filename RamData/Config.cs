using Enterbuy.Data.SqlServer.Dao;
using Enterbuy.Data.SqlServer.Dao.Interfaces;
using Enterbuy.Data.SqlServer.ModelDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RamData
{

    public class UtilsConfig
    {
        public static string AutoKey(string key)
        {
            return $"{key}x{Utils.Utility.DefaultLang}";
        }
    }
    public interface IConfig
    {
        string GetTimeShowPhone();
        string UrlChat();
        string GetByName(string key);
    }
    public class Config : IConfig
    {
        public static int TimeCache = 5;

        public static string Time = Utils.Settings.AppSettings.GetByKey("TimeCache");

        public IConfigDao _configDao;
        public Config(IConfigDao configDao)
        {
            _configDao = configDao;
        }

        static Dictionary<string, ConfigView> _DicConfig { get; set; }
        public static DateTime LastestLoadWebsite = new DateTime(2000, 01, 01);
        public Dictionary<string, ConfigView> DicConfig
        {
            get
            {
                if (!int.TryParse(Time, out TimeCache))
                {
                    TimeCache = 5;
                }
                if (_DicConfig == null || _DicConfig.Count <= 0 || LastestLoadWebsite.AddMinutes(TimeCache) < DateTime.Now)
                {
                    _DicConfig = _configDao.GetAll().ToDictionary(p => $"{p.ConfigName}x{p.LanguageCode.Trim()}", p => p);
                    LastestLoadWebsite = DateTime.Now;
                }
                return _DicConfig;
            }
        }


        public string GetTimeShowPhone()
        {
            string time1 = DicConfig.ContainsKey(UtilsConfig.AutoKey("Time1")) ? DicConfig[UtilsConfig.AutoKey("Time1")].Content : "00:00";
            string time2 = DicConfig.ContainsKey(UtilsConfig.AutoKey("Time2")) ? DicConfig[UtilsConfig.AutoKey("Time2")].Content : "00:00";
            TimeSpan sp1 = TimeSpan.Parse(time1);
            TimeSpan sp2 = TimeSpan.Parse(time2);
            if (DateTime.Now.TimeOfDay > sp1 && DateTime.Now.TimeOfDay < sp2)
            {
                return DicConfig.ContainsKey(UtilsConfig.AutoKey("Phone1")) ? DicConfig[UtilsConfig.AutoKey("Phone1")].Content : "Tạm nghỉ";
            }
            else
            {
                return DicConfig.ContainsKey(UtilsConfig.AutoKey("Phone2")) ? DicConfig[UtilsConfig.AutoKey("Phone2")].Content : "Tạm nghỉ";
            }
        }
        public string UrlChat()
        {
            return DicConfig.ContainsKey(UtilsConfig.AutoKey("Chat")) ? DicConfig[UtilsConfig.AutoKey("Chat")].Content : "";
        }

        public string GetByName(string key)
        {
            return DicConfig.ContainsKey(UtilsConfig.AutoKey(key)) ? DicConfig[UtilsConfig.AutoKey(key)].Content : "";
        }
    }

    public interface IProperties
    {
        string GetText(int id, string prosition, string lang);
    }
    public class Properties : IProperties
    {
        
        public Properties()
        {

        }
        
        public string GetText(int id,string prosition,string lang)
        {
            throw new NotImplementedException();
        }
    }
}
