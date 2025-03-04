﻿using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcc.Nolvus.Core.Interfaces;

namespace Vcc.Nolvus.Services.Updater
{
    public class UpdaterService : IUpdaterService
    {
        public string UpdaterExe
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NolvusUpdater.exe");
            }
        }
        public string Version
        {
            get
            {
                string Version = FileVersionInfo.GetVersionInfo(UpdaterExe).ProductVersion;
                return Version.Substring(0, Version.LastIndexOf('.'));
            }
        }
        public bool IsOlder(string LatestVersion)
        {
            string[] v1List = LatestVersion.Split(new char[] { '.' });
            string[] v2List = Version.Split(new char[] { '.' });

            for (int i = 0; i < v1List.Length; i++)
            {
                int _v1 = System.Convert.ToInt16(v1List[i]);
                int _v2 = System.Convert.ToInt16(v2List[i]);

                if (_v1 > _v2)
                {
                    return true;
                }
                else if (_v1 < _v2)
                {
                    return false;
                }
            }

            return false;
        }
        public bool Installed
        {
            get
            {
                return File.Exists(UpdaterExe);
            }
        }
        public async Task Launch()
        {
            var Tsk = Task.Run(() => 
            {                
                Process DashboardProcess = new Process();
                DashboardProcess.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                DashboardProcess.StartInfo.FileName = UpdaterExe;
                DashboardProcess.StartInfo.Arguments = "1";
                DashboardProcess.Start();
            });

            await Tsk;
        }
    }
}
