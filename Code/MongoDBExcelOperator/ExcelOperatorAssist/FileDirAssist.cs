using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBExcelOperator
{
    public static class FileDirAssist
    {
        /// <summary>
        /// 递归获取所有子文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<DirectoryInfo> GetAllChildDirs(string path)
        {
            List<DirectoryInfo> list = new List<DirectoryInfo>();
            DirectoryInfo root = new DirectoryInfo(path);
            DeepSearchChildDirs(root, list);
            return list;
        }

        private static void DeepSearchChildDirs(DirectoryInfo root, List<DirectoryInfo> list)
        {
            foreach (DirectoryInfo dir in root.GetDirectories())
            {
                list.Add(dir);
                DeepSearchChildDirs(dir, list);
            }
        }

        /// <summary>
        /// 此目录和子目录下是否有.csv文件
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static bool IsThisDirHasCsv(DirectoryInfo root)
        {
            foreach(FileInfo file in root.GetFiles())
            {
                if (file.Extension.ToLower() == ".csv")
                {
                    return true;
                }
            }

            foreach (DirectoryInfo dir in root.GetDirectories())
            {
                IsThisDirHasCsv(dir);
            }

            return false;
        }
    }
}
