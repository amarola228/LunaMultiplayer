﻿using LunaCommon.Message;
using LunaCommon.Message.Base;
using LunaCommon.Message.Data.Vessel;
using LunaCommon.Message.Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LMP.Tests
{
    [TestClass]
    public class MessageStoreTest
    {
        private static readonly ServerMessageFactory Factory = new ServerMessageFactory();
        
        [TestMethod]
        public void TestMsgMessageStore()
        {
            var msg1 = Factory.CreateNew<VesselSrvMsg, VesselPositionMsgData>();
            Assert.AreEqual(0, MessageStore.GetMessageCount(typeof(VesselSrvMsg)));
            Assert.AreEqual(0, MessageStore.GetMessageDataCount(typeof(VesselPositionMsgData)));

            var msg2 = Factory.CreateNew<VesselSrvMsg, VesselPositionMsgData>();
            Assert.AreEqual(0, MessageStore.GetMessageCount(typeof(VesselSrvMsg)));
            Assert.AreEqual(0, MessageStore.GetMessageDataCount(typeof(VesselPositionMsgData)));

            //Set first message as "used"
            msg1.Recycle();

            Assert.AreEqual(1, MessageStore.GetMessageCount(typeof(VesselSrvMsg)));
            Assert.AreEqual(1, MessageStore.GetMessageDataCount(typeof(VesselPositionMsgData)));
            //If we retrieve a new message the first one should be reused
            var msg3 = Factory.CreateNew<VesselSrvMsg, VesselPositionMsgData>();
            
            msg2.Recycle();
            msg3.Recycle();

            var msg4 = Factory.CreateNew<VesselSrvMsg, VesselPositionMsgData>();
            Assert.AreEqual(3, MessageStore.GetMessageCount(typeof(VesselSrvMsg)));
            Assert.AreEqual(3, MessageStore.GetMessageDataCount(typeof(VesselPositionMsgData)));
        }
    }
}
