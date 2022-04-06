using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Service
{
    public partial class Form1 : Form
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        System.Timers.Timer timer = new System.Timers.Timer();

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            log.Debug("Start Services");
            GoogleSheetService.Init();

            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.AutoReset = false;
            timer.Start();




        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            log.Debug("Start Services ---- ");
            int wait = 500;
            int rownum = 10;
            int maxid = 0;

            string queryCustomer = string.Empty;
            try
            {
                wait = int.Parse(Utility.XMLConfig_getValue("TimeWait"));
                rownum = int.Parse(Utility.XMLConfig_getValue("RowNum"));
                maxid = int.Parse(Utility.XMLConfig_getContent("MaxId"));
                //
                queryCustomer = "select TOP " + rownum + " Id, CONVERT(varchar, CreatedDate, 105) + ' ' + SUBSTRING(CONVERT(varchar, CreatedDate, 100), 13, 8) DateAndTime, CreatedDate, Name, PhoneNumber, Note from customer where id > " + maxid;
                //
                DataTable dtResult = Database.GetData(queryCustomer);
                if (dtResult.Rows.Count > 0)
                {
                    string getMaxId = dtResult.Rows[dtResult.Rows.Count - 1]["Id"].ToString();
                    if (!string.IsNullOrEmpty(getMaxId))
                    {
                        Utility.XMLConfig_setValue("MaxId", getMaxId);
                        GoogleSheetService.AddListRow(dtResult);
                    }
                }
            }
            catch (Exception ex)
            {
                //log.Error(ex);
            }
            finally
            {
                timer.Interval = wait;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
