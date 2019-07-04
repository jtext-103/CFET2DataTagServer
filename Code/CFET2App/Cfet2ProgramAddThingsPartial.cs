using Jtext103.CFET2.CFET2App.ExampleThings;
using Jtext103.CFET2.Core;
using Jtext103.CFET2.NancyHttpCommunicationModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewCopy;
using JTextDAQDataFileOperator.HDF5;
using DataServer;
using TagManager;
using TagServer;
using ShotNoWatcher;

namespace Jtext103.CFET2.CFET2App
{
    public partial class Cfet2Program : CFET2Host
    {
        private void AddThings()
        {
            //If you don't want dynamic load things, please comment out the line below
            //var loader = new DynamicThingsLoader(this);

            //you can add Thing by coding here

            //nancy HTTP
            var nancyCM = new NancyCommunicationModule(new Uri("http://localhost:8002"));
            MyHub.TryAddCommunicationModule(nancyCM);

            //拷贝视图文件夹
            //var myViewsCopyer = new ViewCopyer();
            //myViewsCopyer.StartCopy();
            //var myContentCopyer = new ViewCopyer(null, "Content");
            //myContentCopyer.StartCopy();

            var dataServer = new DataServerThing(@"AllConfig.json");
            dataServer.dataFileFactoty = new HDF5DataFileFactory();
            MyHub.TryAddThing(dataServer, "/", "dataServer");

            var tagManager = new TagManagerThing();
            MyHub.TryAddThing(tagManager, "/", "tag", @"AllConfig.json");

            var tagServer = new TagServerThing();
            MyHub.TryAddThing(tagServer, "/", "tagServer");

            var shotWatcher = new ShotNoWatcherThing();
            MyHub.TryAddThing(shotWatcher, "/", "shotWatcher", @"AShotWatcherConfig.json");
        }
    }
}
