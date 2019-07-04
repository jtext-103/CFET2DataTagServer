using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShotNoWatcher
{
    public class EPCISConfig
    {
        /// <summary>
        /// 炮号PV名
        /// </summary>
        public string ShotNoPvName { get; set; }

        /// <summary>
        /// 炮号更新上限，超过不调用更新方法
        /// </summary>
        public int ShotNoUpperLimit { get; set; }

        /// <summary>
        /// 炮号更新下限，超过不调用更新方法
        /// </summary>
        public int ShotNoLowerLimit { get; set; }

        /// <summary>
        /// 监控另一个PV值，避免ShotPvName在非实验期间变了造成炮号误改
        /// </summary>
        public string ControlPvName { get; set; }

        /// <summary>
        /// 当PV[ControlPvName] == ControlPvValue时，ShotPvName的值有用
        /// </summary>
        public int ControlPvValue { get; set; }

        /// <summary>
        /// 更新炮号的CFET方法路径，此方法应支持一个int参数
        /// </summary>
        public string CFETPathToUploadShotNo { get; set; }

        /// <summary>
        /// 更新间隔，也就是多久调用一次CFETPathToUploadShotNo
        /// </summary>
        public int UploadTimeIntervalInSecond { get; set; } 

        /// <summary>
        /// 上面两个PV名，对用户不可见
        /// </summary>
        public string[] PvNames { get; set; }

        /// <summary>
        /// ControlPvName是否有效，无效时相当于没有Control那一套。对用户不可见
        /// </summary>
        public bool IsControlPvOn { get; set; }


        public EPCISConfig(string configFilePath)
        {
            JsonConvert.PopulateObject(File.ReadAllText(configFilePath, Encoding.Default), this);
            if(ControlPvName != null && ControlPvName != "")
            {
                PvNames = new string[2] { ShotNoPvName, ControlPvName };
                IsControlPvOn = true;
            }
            else
            {
                PvNames = new string[1] { ShotNoPvName };
                IsControlPvOn = false;
            }
        }
    }
}