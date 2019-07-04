using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JtextEpcisPvClient;
using Jtext103.CFET2.Core;
using Jtext103.CFET2.Core.Attributes;
using System.Threading;

namespace ShotNoWatcher
{
    /// <summary>
    /// 监控中控EPCIS，如果炮号变了，调用一个CFET方法
    /// </summary>
    public class ShotNoWatcherThing : Thing, IDisposable
    {
        private EPCISConfig config;

        JtextEpicsClient myJtextEpcisClient;

        private int uptodataShotNo;
        private int uptodataCtrlVal;

        Thread uploadingThread;

        public override void TryInit(object initObj)
        {
            config = new EPCISConfig((string)initObj);
            RestartEpcisClient();
        }

        private void Uploading()
        {
            while (true)
            {
                //初始无效值
                uptodataShotNo = -1;
                uptodataCtrlVal = -255;

                //更新炮号，double check
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        int shotNo1 = Convert.ToInt32(myJtextEpcisClient.GetPV(config.ShotNoPvName));
                        int shotNo2 = Convert.ToInt32(myJtextEpcisClient.GetPV(config.ShotNoPvName));
                        int shotNo3 = Convert.ToInt32(myJtextEpcisClient.GetPV(config.ShotNoPvName));
                        if (shotNo1 == shotNo2 && shotNo2 == shotNo3)
                        {
                            uptodataShotNo = shotNo1;
                            break;
                        }
                    }
                    catch (Exception exc)
                    {
                        continue;
                    }
                }

                if (config.IsControlPvOn)
                {
                    //更新中控，double check
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {
                            int ctrlVal1 = Convert.ToInt32(myJtextEpcisClient.GetPV(config.ControlPvName));
                            int ctrlVal2 = Convert.ToInt32(myJtextEpcisClient.GetPV(config.ControlPvName));
                            int ctrlVal3 = Convert.ToInt32(myJtextEpcisClient.GetPV(config.ControlPvName));
                            if (ctrlVal1 == ctrlVal2 && ctrlVal2 == ctrlVal3)
                            {
                                uptodataCtrlVal = ctrlVal1;
                                break;
                            }
                        }
                        catch (Exception exc)
                        {
                            continue;
                        }
                    }
                }

                if (uptodataCtrlVal == config.ControlPvValue || config.IsControlPvOn == false)
                {
                    if (uptodataShotNo >= config.ShotNoLowerLimit && uptodataShotNo <= config.ShotNoUpperLimit)
                    {
                        MyHub.TryInvokeSampleResourceWithUri(config.CFETPathToUploadShotNo, new object[1] { uptodataShotNo });
                    }
                }

                Thread.Sleep(config.UploadTimeIntervalInSecond * 1000);
            }
        }

        /// <summary>
        /// 当EPCIS宕机后使用，此方法会造成约0.5M的内存泄漏
        /// </summary>
        [Cfet2Method]
        public void RestartEpcisClient()
        {
            try
            {
                uploadingThread.Abort();
            }
            catch { }
            myJtextEpcisClient = new JtextEpicsClient(config.PvNames);
            uploadingThread = new Thread(Uploading);
            uploadingThread.Start();

        }

        [Cfet2Status]
        public int UptodataShotNo()
        {
            return uptodataShotNo;
        }

        [Cfet2Status]
        public int UptodataCtrlVal()
        {
            return uptodataCtrlVal;
        }

        [Cfet2Status]
        public bool IsControlPvOn()
        {
            return config.IsControlPvOn;
        }

        public void Dispose()
        {
            uploadingThread.Abort();
        }
    }
}
