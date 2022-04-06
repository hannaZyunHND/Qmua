using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Service
{
    public class Utility
    {
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
        public static string XMLConfig_getValue(string key)
        {
            DataSet dsResult = new DataSet();
            string XMLFilePath = System.Windows.Forms.Application.StartupPath + "//XmlConfig.xml";
            XmlReader loadedData = XmlReader.Create(XMLFilePath);
            dsResult.ReadXml(loadedData);
            if (dsResult.Tables.Contains("add"))
            {
                DataTable dtResult = dsResult.Tables["add"];
                for (int i = 0; i < dtResult.Rows.Count; i++)
                {
                    if (dtResult.Rows[i]["key"].ToString() == key)
                    {
                        return dtResult.Rows[i]["value"].ToString();
                    }
                }
            }
            return "";
        }
        public static string XMLConfig_getContent(string tag)
        {
            string content = "";
            string XMLFilePath = System.Windows.Forms.Application.StartupPath + "//XmlConfig.xml";
            XmlDocument xml = new XmlDocument();
            xml.Load(XMLFilePath);
            content = xml.SelectSingleNode("//AppSettings//" + tag).InnerText;
            return content;
        }
        public static void XMLConfig_setValue(string tag, string value)
        {            
            string XMLFilePath = System.Windows.Forms.Application.StartupPath + "//XmlConfig.xml"; 
            XmlDocument xml = new XmlDocument();
            xml.Load(XMLFilePath);
            xml.SelectSingleNode("//AppSettings//"+ tag).InnerText = value;
            xml.Save(XMLFilePath);
        }
    }
}
