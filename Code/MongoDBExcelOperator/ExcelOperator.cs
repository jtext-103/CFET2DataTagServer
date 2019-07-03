using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBExcelOperator
{
    /// <summary>
    /// 给TagManagerThing操作Excal的接口
    /// 注意，所有和文件有关的操作全在这个类中
    /// </summary>
    public class ExcelOperator
    {
        /// <summary>
        /// 通过配置文件夹路径生成一个TagPathGroup以便后续能存入MongoDB
        /// </summary>
        /// <param name="configDirPath">配置文件夹的完整路径，不带结尾分隔符！！！</param>
        /// <returns></returns>
        public TagPathGroup GetNewTagPathGroup(string configDirPath, int versionNo)
        {
            //因为传过来的可能是相对路径，这样可以获取绝对路径
            DirectoryInfo rootDir = new DirectoryInfo(configDirPath);

            var tagPathGroup = new TagPathGroup();

            List<DirectoryInfo> allList = FileDirAssist.GetAllChildDirs(configDirPath);
            allList.Add(rootDir);

            List<DirectoryInfo> realList = new List<DirectoryInfo>();
             
            //递归搜索将只添加目录下有.csv的文件夹
            foreach (var d in allList)
            {
                if(FileDirAssist.IsThisDirHasCsv(d))
                {
                    realList.Add(d);
                }
            }

            //添加文件夹节点信息到Collection
            foreach(var dir in realList)
            {
                string path = dir.FullName.Remove(0, rootDir.Parent.FullName.Length);
                path.Replace(Path.DirectorySeparatorChar, '/');
                var temp = new TagPath();
                temp.Path = path;
                temp.VersionNo = versionNo;
                tagPathGroup.Maps.Add(temp);
            }

            var fileAndTagMaps = new List<TagPath>();

            //添加每个文件夹下的.csv文件和文件中的Tag信息到tagPathCollection.Maps
            foreach (var d in tagPathGroup.Maps)
            {
                string realPath = d.Path.Replace('/', Path.DirectorySeparatorChar);
                realPath = realPath.Insert(0, rootDir.Parent.FullName);

                DirectoryInfo dir = new DirectoryInfo(realPath);
                foreach (FileInfo f in dir.GetFiles())
                {
                    if (f.Extension == ".csv")
                    {                     
                        //转换文件名格式
                        string path = f.FullName.Remove(0, rootDir.Parent.FullName.Length);
                        path.Replace(Path.DirectorySeparatorChar, '/');

                        //添加文件条目
                        var temp = new TagPath();
                        temp.Path = path;
                        temp.VersionNo = versionNo;
                        fileAndTagMaps.Add(temp);

                        //添加文件中Tag条目
                        var tagList = GetTagsFromCsvFile(f, path, versionNo);
                        fileAndTagMaps.AddRange(tagList);
                    }
                }  
            }
            tagPathGroup.Maps.AddRange(fileAndTagMaps);

            CheckIfGroupLegal(tagPathGroup);

            return tagPathGroup;
        }

        private List<TagPath> GetTagsFromCsvFile(FileInfo file, string filePath, int versionNo)
        {
            var list = new List<TagPath>();
            FileStream fs = new FileStream(file.FullName, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);

            //处理第一行
            string head = sr.ReadLine();
            List<string> headList = ExplainOneLine(head);
            if (headList[0] != "Name" || headList[1] != "FormatPath")
            {
                throw new Exception("Incorrect File format! Please check your .csv File and make sure you have Name and FormatPath in right place");
            }

            //处理其它行
            string body;
            List<string> bodyList;
            while((body = sr.ReadLine()) != null)
            {
                bodyList = ExplainOneLine(body);
                TagPath temp = new TagPath()
                {
                    TagName = bodyList[0],
                    TagFormatPath = bodyList[1],
                    Path = filePath,
                    VersionNo = versionNo
            };
                list.Add(temp);
            }

            sr.Close();
            fs.Close();
            return list;
        }

        private List<string> ExplainOneLine(string s)
        {
            s = s.Replace(" ", "");
            List<string> list = new List<string>();
            while (s.Length > 0)
            {
                if (s.Contains(","))
                {
                    list.Add(s.Substring(0, s.IndexOf(',')));
                    s = s.Substring(s.IndexOf(',') + 1);
                }
                else
                {
                    list.Add(s);
                    break;
                }
            }
            return list;
        }

        private void CheckIfGroupLegal(TagPathGroup tagPathGroup)
        {
            var existedTagName = new List<string>();
            foreach (var t in tagPathGroup.Maps) 
            {
                Predicate<string> myPredicate = s => s == t.TagName;
                if(existedTagName.Exists(myPredicate) && t.TagName != null && t.TagName != "")
                {
                    throw new Exception("You can't add two Tag with one TagName!");
                }
                existedTagName.Add(t.TagName);
            }
        }

        /// <summary>
        /// 将一个tagPathGroup导出到一个文件夹，和GetNewTagPathGroup互为逆操作
        /// </summary>
        /// <param name="tagPathGroup"></param>
        /// <param name="configDirPath">导出的位置，注意这里的路径没有root文件夹了</param>
        /// <returns></returns>
        public bool ExportTagPathGroupToDir(TagPathGroup tagPathGroup, string configDirPath)
        {
            if(configDirPath == null)
            {
                configDirPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }
            var rootDir = new DirectoryInfo(configDirPath);
            if (!rootDir.Exists) return false;

            //依次建立文件夹、文件、Tag条目
            var dirList = new List<TagPath>();
            var fileList = new List<TagPath>();
            var tagList = new List<TagPath>();

            foreach(var t in tagPathGroup.Maps)
            {
                if(!t.Path.EndsWith(".csv"))
                {
                    dirList.Add(t);
                    continue;
                }
                if(t.TagName == null || t.TagName == "")
                {
                    fileList.Add(t);
                    continue;
                }
                tagList.Add(t);
            }

            try
            {
                //建立所有文件夹
                foreach (var d in dirList)
                {
                    Directory.CreateDirectory(configDirPath + d.Path);
                }

                //建立所有文件，并写入Tag
                foreach(var f in fileList)
                {
                    var nowTagList = new List<TagPath>();
                    foreach(var t in tagList)
                    {
                        if(t.Path == f.Path)
                        {
                            nowTagList.Add(t);
                        }
                    }
                    WriteACSVFile(configDirPath + f.Path, nowTagList);
                }
            }
            catch
            {
                return false;
            }
            

            return true;
        }

        private void WriteACSVFile(string filePath, List<TagPath> tagList)
        {
            string[] context = new string[tagList.Count + 1];
            context[0] = "Name,FormatPath";
            for(int i = 0; i < tagList.Count; i++)
            {
                context[i + 1] = tagList[i].TagName + "," + tagList[i].TagFormatPath;
            }

            File.WriteAllLines(filePath, context);
        }
    }
}
