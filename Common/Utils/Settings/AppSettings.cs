using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Utils.Settings
{
    public class AppSettings
    {
        public static IConfigurationRoot Configuration { get; set; }
        private const string SecretKey = "8vT2A12RBmZ1T5Kf";


        static AppSettings()
        {
            Configuration = ConfigurationHelper.Init();
        }
        public static string GenerateConnectionString(string input)
        {
            return Crypton.EncryptByKey(input, SecretKey);
        }
        public static string ConnectionString
        {
            get
            {
                var Udemo = Configuration.GetConnectionString("DefaultConnection");
                if (String.IsNullOrEmpty(Udemo))
                {
                    Udemo = GetByKey("DefaultConnection");
                }
                return Udemo;


            }
        }
        public static string ConnectionStringSlave
        {
            get
            {
                return Crypton.DecryptByKey(Configuration.GetConnectionString("Slave"), SecretKey);
            }
        }


        public static string GetByKey(string key)
        {
            return Configuration.GetSection("AppSettings")[key] ?? "";
        }
        public static string GetByKeyMail(string key)
        {
            return Configuration.GetSection("Email")[key] ?? "";
        }
        public static bool GetByKeyToBool(string key)
        {
            return Utility.ConvertToBoolean(Configuration.GetSection("AppSettings")[key] ?? "False");
        }
        public static string StorageDomain
        {
            get { return GetByKey("StorageDomain"); }
        }
        public static string Domain
        {
            get { return GetByKey("Domain"); }
        }
        public static string FoderImg
        {
            get { return GetByKey("FoderImg"); }
        }
        public static string BaseDomain
        {
            get { return GetByKey("BaseDomain"); }
        }
        public static string Version
        {
            get { return GetByKey("Version"); }
        }
    }
}
