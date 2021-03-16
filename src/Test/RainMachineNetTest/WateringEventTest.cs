using NUnit.Framework;
using RainMachineNet;
using RainMachineNet.Event;
using RainMachineNet.Responses;
using RainMachineNet.Support;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace RainMachineNetTest
{
    public class WateringEventTest: WateringNotificationSubscriber<WateringEvent>
    {
        private IRainMaker _rainMaker;

        private bool _watering;

        [SetUp]
        public void Setup()
        {
            _rainMaker = new RainMaker();
        }


        public WateringEventTest()
        {
            _watering=true;
        }

        [Test]
        public async Task Subscription()
        {
            var ret = await _rainMaker.LoginAsync(Constants.NetName, Constants.User, Constants.Password, Constants.DeviceCertId);
            Assert.IsTrue(ret);

            var test = new WateringEventTest();
            var test2 = new WateringEventTest();
            await _rainMaker.Subscribe(test);
            Assert.IsTrue(_rainMaker.IsPolling);
            await _rainMaker.UnSubscribe(test);
            Assert.IsFalse(_rainMaker.IsPolling);
            await _rainMaker.Subscribe(test);
            Assert.IsTrue(_rainMaker.IsPolling);
            await _rainMaker.Subscribe(test2);
            Assert.IsTrue(_rainMaker.IsPolling);
            await _rainMaker.UnSubscribe(test);
            Assert.IsTrue(_rainMaker.IsPolling);
            await _rainMaker.UnSubscribe(test2);
            Assert.IsFalse(_rainMaker.IsPolling);
        }


        [Test]
        public async Task TestPollingNotification()
        {
            var ret = await _rainMaker.LoginAsync(Constants.NetName, Constants.User, Constants.Password);
            Assert.IsTrue(ret);

            var test = new WateringEventTest();
            await _rainMaker.Subscribe(test);
            await _rainMaker.ProgramStart(Constants.TestProgram);
            var rc=await _rainMaker.GetWateringProgram();
            while (test.Waiting)
            {
                Thread.Sleep(500);
            }
            await _rainMaker.UnSubscribe(test);
            await _rainMaker.ProgramStop(Constants.TestProgram);
            Assert.IsInstanceOf<IResponseBase>(rc, "Unexpected object type");
            Assert.IsInstanceOf<ProgramsResponse>(rc, "Unexpected object type");
        }

        public override void OnNext(WateringEvent ev)
        {
            foreach(var e in ev.Watering.zones)
            {
                Debugger.Log(1,"Test", $"Zone {e.uid}-{e.name} is currently {e.state}\r\n");
            }
            _watering=ev.Watering.zones.Any(a=>a.state==RainMachineNet.Model.Shared.WateringState.Running);
            base.OnNext(ev);
        }

        public bool Waiting => _watering;
    }
}
