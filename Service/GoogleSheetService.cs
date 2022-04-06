using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class GoogleSheetService
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = Utility.XMLConfig_getValue("ApplicationName");
        static readonly string sheet = Utility.XMLConfig_getValue("SheetName");
        static readonly string SpreadsheetId = Utility.XMLConfig_getValue("SpreadsheetId");

        //static readonly string ApplicationName = Const.ApplicationName;
        //static readonly string sheet = Const.SheetName;
        //static readonly string SpreadsheetId = Const.SpreadsheetId;
        static SheetsService service;
        public static void Init()
        {
            GoogleCredential credential = null;
            try
            {
                using (var stream = new FileStream(System.Windows.Forms.Application.StartupPath + "//app_client_secret.json", FileMode.Open, FileAccess.Read))
                {
                    log.Error("Thành công");
                    credential = GoogleCredential.FromStream(stream)
                        .CreateScoped(Scopes);
                }

                service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
        public static void ReadSheet()
        {
            // Specifying Column Range for reading...
            var range = $"{sheet}!A:E";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(SpreadsheetId, range);
            // Ecexuting Read Operation...
            var response = request.Execute();
            // Getting all records from Column A to E...
            IList<IList<object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    // Writing Data on Console...
                    Console.WriteLine("{0} | {1} | {2} | {3} | {4} ", row[0], row[1], row[2], row[3], row[4]);
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
        }
        public static void AddRow(List<object> oblist)
        {
            // Specifying Column Range for reading...
            var range = $"{sheet}!A:E";
            var valueRange = new ValueRange();
            // Data for another Student...
            //var oblist = new List<object>() { "Harry", "822220", "77", "62", "98" };
            valueRange.Values = new List<IList<object>> { oblist };
            // Append the above record...
            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = appendRequest.Execute();
        }
        public static void AddRow(List<object> oblist, string idx)
        {
            // Specifying Column Range for reading...
            var range = $"{sheet}!A"+ idx +":E" + idx;
            var valueRange = new ValueRange();
            // Data for another Student...
            //var oblist = new List<object>() { "Harry", "822220", "77", "62", "98" };
            valueRange.Values = new List<IList<object>> { oblist };
            // Append the above record...
            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = appendRequest.Execute();
        }
        public static void ClearData(int totalrow)
        {

            try
            {
                for (int i = 2; i <= totalrow + 1; i++)
                {
                    RemoveRow(i.ToString());
                }
            }
            catch(Exception ex) { log.Error(ex); }
        }
        public static void ClearAllData(int totalrow)
        {

            try
            {
                List<IList<object>> l = new List<IList<object>>();
                for (int i = 0; i < totalrow; i++)
                {
                    l.Add(new List<object>() { "", "", "", "", "", "", "" });
                }
                var range = $"{sheet}!A2:G" + totalrow + 1;
                var valueRange = new ValueRange();
                valueRange.Values = l;
                // Performing Update Operation...
                var updateRequest = service.Spreadsheets.Values.Update(valueRange, SpreadsheetId, range);
                updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
                var appendReponse = updateRequest.Execute();
            }
            catch (Exception ex) { log.Error(ex); }
        }
        public static void RemoveRow(string rowindex)
        {
            try
            {
                // Setting Cell Name...
                var range = $"{sheet}!A"+ rowindex + ":G" + rowindex;
            var valueRange = new ValueRange();
            // Setting Cell Value...
            var oblist = new List<object>() { "", "", "", "", "", "", "" };
            valueRange.Values = new List<IList<object>> { oblist };
            // Performing Update Operation...
            var updateRequest = service.Spreadsheets.Values.Update(valueRange, SpreadsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = updateRequest.Execute();
            }
            catch (Exception ex) { log.Error(ex); }
        }
        public static void UpdateCell()
        {
            // Setting Cell Name...
            var range = $"{sheet}!A2:G2";
            var valueRange = new ValueRange();
            // Setting Cell Value...
            var oblist = new List<object>() { "", "", "", "", "", "", ""};
            valueRange.Values = new List<IList<object>> { oblist };
            // Performing Update Operation...
            var updateRequest = service.Spreadsheets.Values.Update(valueRange, SpreadsheetId, range);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = updateRequest.Execute();
        }
        public static void AddListRow(DataTable dt)
        {
            List<object> lObject = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lObject = new List<object>();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    lObject.Add(dt.Rows[i][j]);
                }
                AddRow(lObject);
            }
        }
        public static void AddListRow_v2(DataTable dt)
        {
            List<IList<object>> l = new List<IList<object>>();
            List<object> lObject = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lObject = new List<object>();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    lObject.Add(dt.Rows[i][j]);
                }
                l.Add(lObject);                
            }
            var range = $"{sheet}!A2:E" + dt.Rows.Count + 1;
            var valueRange = new ValueRange();
            valueRange.Values = l;
            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = appendRequest.Execute();
        }
    }
}
