using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace TagManager
{
    public class ManagerConfig
    {
        /// <summary>
        /// MongoDB数据库地址
        /// </summary>
        public string DatabaseHost { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DatabaseName { get; set; }

        public ManagerConfig(string path)
        {
            JsonConvert.PopulateObject(File.ReadAllText(path, Encoding.Default), this);
        }
    }
}
