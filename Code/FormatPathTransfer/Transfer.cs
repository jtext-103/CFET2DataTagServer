using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormatPathTransfer
{
    /// <summary>
    /// 路径格式转换工具，将一段格式化（带{ }）的路径转换成完整路径
    /// </summary>
    public static class Transfer
    {
        /// <summary>
        /// 将带{}格式的路径根据规则转换成真实路径
        /// </summary>
        /// <param name="formatPath">带{}的原路径</param>
        /// <param name="body">转换参数，如炮号</param>
        /// <param name="rule">转换规则</param>
        /// <returns></returns>
        public static string TransferFormatPathToRealPath(string formatPath, object body, Rule rule)
        {   
            int startIndex = formatPath.IndexOf('{');
            int endIndex;
            string nowPartFormat;
            string nowPartReal;

            string result = formatPath;

            while (startIndex > 0)
            {
                //放这里可以避免一些BUG
                endIndex = formatPath.IndexOf('}', startIndex);
                nowPartFormat = formatPath.Substring(startIndex, endIndex - startIndex + 1);
                switch (rule)
                {
                    case Rule.ShotNoLastSubstitution:
                        {
                            nowPartReal = RuleRealization.ShotNoLastSubstitution.ReplaceOnePart(nowPartFormat, (int)body);
                            break;
                        }
                    default:
                        {
                            throw new Exception("Unknow transfer Rule!");
                        }
                }
                //说不定能一下换掉2个
                result = result.Replace(nowPartFormat, nowPartReal);

                startIndex = formatPath.IndexOf('{', endIndex);
            }

            return result;
        }

        public static string BuildRealPath(object body, Rule rule, char directorySeparatorChar)
        {
            throw new NotImplementedException();
        }
        
    }
}
