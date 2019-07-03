using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormatPathTransfer.RuleRealization
{
    public class ShotNoLastSubstitution
    {
        //替换形如{shot:4:x}这样的字符串，结果为106xxxx（shotNo = 1063210）
        public static string ReplaceOnePart(string formatString, int shotNo)
        {
            string result;

            string replaceString;

            int firstColonIndex = formatString.IndexOf(':');
            int secondColonIndex = formatString.IndexOf(':', firstColonIndex + 1);
            int endIndex = formatString.Length - 1;

            if (secondColonIndex - firstColonIndex == 1)
            {
                result = shotNo.ToString();
                return result;
            }

            int lastSubstitution = int.Parse(formatString.Substring(firstColonIndex + 1, secondColonIndex - firstColonIndex - 1));
            result = ((int)(shotNo / Math.Pow(10, lastSubstitution))).ToString();

            replaceString = formatString.Substring(secondColonIndex + 1, formatString.Length - secondColonIndex - 2);

            for (int i = 0; i < lastSubstitution; i++)
            {
                result += replaceString;
            }

            return result;
        }
    }

}
