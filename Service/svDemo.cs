using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Service
{
    partial class svDemo : ServiceBase
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        System.Timers.Timer timer = new System.Timers.Timer();
        System.Timers.Timer timerKH = new System.Timers.Timer();        
        public svDemo()
        {
            InitializeComponent();
                                
        }        
        protected override void OnStart(string[] args)
        {
            log.Info("START SERVICE");
            GoogleSheetService.Init();
           
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
           
        }

        protected override void OnStop()
        {
            log.Info("STOP SERVICE");
        }
    }
}
