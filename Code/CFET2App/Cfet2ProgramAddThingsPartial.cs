using Jtext103.CFET2.CFET2App.ExampleThings;
using Jtext103.CFET2.Core;
using Jtext103.CFET2.NancyHttpCommunicationModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jtext103.CFET2.Core.BasicThings;
using Nancy.Conventions;
using Nancy;
using Jtext103.CFET2.Core.Middleware.Basic;
using Jtext103.CFET2.CFET2App.DynamicLoad;
using DataServer;
using TagManager;
using TagServer;
using ShotNoWatcher;
using JTextDAQDataFileOperator.HDF5;

namespace Jtext103.CFET2.CFET2App
{
    public partial class Cfet2Program : CFET2Host
    {
        private void AddThings()
        {
            //If you don't want dynamic load things, please comment out the line below
            var loader = new DynamicThingsLoader(this);

            //------------------------------Pipeline------------------------------//
            MyHub.Pipeline.AddMiddleware(new ResourceInfoMidware());
            MyHub.Pipeline.AddMiddleware(new NavigationMidware());

            //------------------------------Nancy HTTP通信模块------------------------------//
            var nancyCM = new NancyCommunicationModule(new Uri("http://localhost:8002"));
            MyHub.TryAddCommunicationModule(nancyCM);

            //you can add Thing by coding here

            //------------------------------Custom View------------------------------//
            var customView = new CustomViewThing();
            MyHub.TryAddThing(customView, "/", "customView", "./CustomView");

            //you can add Thing by coding here

            var dataServer = new DataServerThing(@"./ConfigFile/BasePath.json");
            dataServer.dataFileFactoty = new HDF5DataFileFactory();
            MyHub.TryAddThing(dataServer, "/", "dataServer");

            var tagManager = new TagManagerThing();
            MyHub.TryAddThing(tagManager, "/", "tag", @"./ConfigFile/Database.json");

            var tagServer = new TagServerThing();
            MyHub.TryAddThing(tagServer, "/", "tagServer");

            var shotWatcher = new ShotNoWatcherThing();
            MyHub.TryAddThing(shotWatcher, "/", "shotWatcher", @"./ConfigFile/ShotNoWatcher.json");
        }
    }
}
