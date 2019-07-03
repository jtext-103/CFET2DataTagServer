using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jtext103.CFET2.Core;
using Jtext103.CFET2.Core.Attributes;

namespace TagServer
{
    /// <summary>
    /// 负责接收用户以Tag+Shot方式的数据请求，转换格式之后，将请求转发给DataServerThing，并返回结果
    /// 此Thing完全依赖DataServerThing和TagManagerThing
    /// </summary>
    public class TagServerThing : Thing
    {
        private string tagCurrentShot = @"/tag/currentshotno";
        private string tagRealPath = @"/tag/realpath/";
        private string dataServer = @"/dataserver/";

        [Cfet2Status]
        public double[] Data(string tag, int shot, ulong start = 0, ulong length = 0)
        {
            if (shot == 0)
            {
                throw new NotImplementedException();
                //shot = (int)(MyHub.TryGetResourceSampleWithUri(tagCurrentShot).ObjectVal);
            }

            string realPath = MyHub.TryGetResourceSampleWithUri(tagRealPath + tag + "/" + shot.ToString()).ObjectVal.ToString();

            string dp = dataServer + "Data";
            object[] param = new object[3];
            param[0] = realPath;
            param[1] = start;
            param[2] = length;

            return (double[])(MyHub.TryGetResourceSampleWithUri(dp, param).ObjectVal);
        }

        [Cfet2Status]
        public double[] DataTimeAxis(string tag, int shot, ulong start = 0, ulong length = 0)
        {
            if (shot == 0)
            {
                throw new NotImplementedException();
                //shot = (int)(MyHub.TryGetResourceSampleWithUri(tagCurrentShot).ObjectVal);
            }

            string realPath = MyHub.TryGetResourceSampleWithUri(tagRealPath + tag + "/" + shot.ToString()).ObjectVal.ToString();

            string dp = dataServer + "DataTimeAxis";
            object[] param = new object[3];
            param[0] = realPath;
            param[1] = start;
            param[2] = length;

            return (double[])(MyHub.TryGetResourceSampleWithUri(dp, param).ObjectVal);
        }

        [Cfet2Status]
        public double[] DataComplex(string tag, int shot, ulong start, ulong stride, ulong count, ulong block = 1)
        {          
            if(shot == 0)
            {
                throw new NotImplementedException();
                //shot = (int)(MyHub.TryGetResourceSampleWithUri(tagCurrentShot).ObjectVal);
            }

            string realPath = MyHub.TryGetResourceSampleWithUri(tagRealPath + tag + "/" + shot.ToString()).ObjectVal.ToString();

            string dp = dataServer + "DataComplex";
            object[] param = new object[5];
            param[0] = realPath;
            param[1] = start;
            param[2] = stride;
            param[3] = count;
            param[4] = block;

            return (double[])(MyHub.TryGetResourceSampleWithUri(dp, param).ObjectVal);
        }

        [Cfet2Status]
        public double[] DataComplexTimeAxis(string tag, int shot, ulong start, ulong stride, ulong count, ulong block = 1)
        {
            if (shot == 0)
            {
                throw new NotImplementedException();
                //shot = (int)(MyHub.TryGetResourceSampleWithUri(tagCurrentShot).ObjectVal);
            }

            string realPath = MyHub.TryGetResourceSampleWithUri(tagRealPath + tag + "/" + shot.ToString()).ObjectVal.ToString();

            string dp = dataServer + "DataComplexTimeAxis";
            object[] param = new object[5];
            param[0] = realPath;
            param[1] = start;
            param[2] = stride;
            param[3] = count;
            param[4] = block;

            return (double[])(MyHub.TryGetResourceSampleWithUri(dp, param).ObjectVal);
        }

        [Cfet2Status]
        public double[] DataByTime(string tag, int shot, double startTime, double endTime, ulong stride)
        {
            if (shot == 0)
            {
                throw new NotImplementedException();
                //shot = (int)(MyHub.TryGetResourceSampleWithUri(tagCurrentShot).ObjectVal);
            }

            string realPath = MyHub.TryGetResourceSampleWithUri(tagRealPath + tag + "/" + shot.ToString()).ObjectVal.ToString();

            string dp = dataServer + "DataByTime";
            object[] param = new object[4];
            param[0] = realPath;
            param[1] = startTime;
            param[2] = endTime;
            param[3] = stride;

            return (double[])(MyHub.TryGetResourceSampleWithUri(dp, param).ObjectVal);
        }

        [Cfet2Status]
        public double[] DataByTimeTimeAxis(string tag, int shot, double startTime, double endTime, ulong stride)
        {
            if (shot == 0)
            {
                throw new NotImplementedException();
                //shot = (int)(MyHub.TryGetResourceSampleWithUri(tagCurrentShot).ObjectVal);
            }

            string realPath = MyHub.TryGetResourceSampleWithUri(tagRealPath + tag + "/" + shot.ToString()).ObjectVal.ToString();

            string dp = dataServer + "DataByTimeTimeAxis";
            object[] param = new object[4];
            param[0] = realPath;
            param[1] = startTime;
            param[2] = endTime;
            param[3] = stride;

            return (double[])(MyHub.TryGetResourceSampleWithUri(dp, param).ObjectVal);
        }

        [Cfet2Status]
        public double[] DataByTimeFuzzy(string tag, int shot, double startTime, double endTime, ulong count)
        {
            if (shot == 0)
            {
                throw new NotImplementedException();
                //shot = (int)(MyHub.TryGetResourceSampleWithUri(tagCurrentShot).ObjectVal);
            }

            string realPath = MyHub.TryGetResourceSampleWithUri(tagRealPath + tag + "/" + shot.ToString()).ObjectVal.ToString();

            string dp = dataServer + "DataByTimeFuzzy";
            object[] param = new object[4];
            param[0] = realPath;
            param[1] = startTime;
            param[2] = endTime;
            param[3] = count;

            return (double[])(MyHub.TryGetResourceSampleWithUri(dp, param).ObjectVal);
        }

        [Cfet2Status]
        public double[] DataByTimeFuzzyTimeAxis(string tag, int shot, double startTime, double endTime, ulong count)
        {
            if (shot == 0)
            {
                throw new NotImplementedException();
                //shot = (int)(MyHub.TryGetResourceSampleWithUri(tagCurrentShot).ObjectVal);
            }

            string realPath = MyHub.TryGetResourceSampleWithUri(tagRealPath + tag + "/" + shot.ToString()).ObjectVal.ToString();

            string dp = dataServer + "DataByTimeFuzzyTimeAxis";
            object[] param = new object[4];
            param[0] = realPath;
            param[1] = startTime;
            param[2] = endTime;
            param[3] = count;

            return (double[])(MyHub.TryGetResourceSampleWithUri(dp, param).ObjectVal);
        }

        [Cfet2Status]
        public int Length(string tag, int shot)
        {
            if (shot == 0)
            {
                throw new NotImplementedException();
                //shot = (int)(MyHub.TryGetResourceSampleWithUri(tagCurrentShot).ObjectVal);
            }

            string realPath = MyHub.TryGetResourceSampleWithUri(tagRealPath + tag + "/" + shot.ToString()).ObjectVal.ToString();

            string dp = dataServer + "Length";
            object[] param = new object[1];
            param[0] = realPath;

            return (int)(MyHub.TryGetResourceSampleWithUri(dp, param).ObjectVal);
        }

        [Cfet2Status]
        public double SampleRate(string tag, int shot)
        {
            if (shot == 0)
            {
                throw new NotImplementedException();
                //shot = (int)(MyHub.TryGetResourceSampleWithUri(tagCurrentShot).ObjectVal);
            }

            string realPath = MyHub.TryGetResourceSampleWithUri(tagRealPath + tag + "/" + shot.ToString()).ObjectVal.ToString();

            string dp = dataServer + "SampleRate";
            object[] param = new object[1];
            param[0] = realPath;

            return (double)(MyHub.TryGetResourceSampleWithUri(dp, param).ObjectVal);
        }

        [Cfet2Status]
        public double StartTime(string tag, int shot)
        {
            if (shot == 0)
            {
                throw new NotImplementedException();
                //shot = (int)(MyHub.TryGetResourceSampleWithUri(tagCurrentShot).ObjectVal);
            }

            string realPath = MyHub.TryGetResourceSampleWithUri(tagRealPath + tag + "/" + shot.ToString()).ObjectVal.ToString();

            string dp = dataServer + "StartTime";
            object[] param = new object[1];
            param[0] = realPath;

            return (double)(MyHub.TryGetResourceSampleWithUri(dp, param).ObjectVal);
        }

        [Cfet2Status]
        public double CreateTime(string tag, int shot)
        {
            if (shot == 0)
            {
                throw new NotImplementedException();
                //shot = (int)(MyHub.TryGetResourceSampleWithUri(tagCurrentShot).ObjectVal);
            }

            string realPath = MyHub.TryGetResourceSampleWithUri(tagRealPath + tag + "/" + shot.ToString()).ObjectVal.ToString();

            string dp = dataServer + "CreateTime";
            object[] param = new object[1];
            param[0] = realPath;

            return (double)(MyHub.TryGetResourceSampleWithUri(dp, param).ObjectVal);
        }

        [Cfet2Status]
        public string MetadataJson(string tag, int shot)
        {
            if (shot == 0)
            {
                throw new NotImplementedException();
                //shot = (int)(MyHub.TryGetResourceSampleWithUri(tagCurrentShot).ObjectVal);
            }

            string realPath = MyHub.TryGetResourceSampleWithUri(tagRealPath + tag + "/" + shot.ToString()).ObjectVal.ToString();

            string dp = dataServer + "MetadataJson";
            object[] param = new object[1];
            param[0] = realPath;

            return (string)(MyHub.TryGetResourceSampleWithUri(dp, param).ObjectVal);
        }
    }
}
