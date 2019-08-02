using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MZC_Gateway.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [AssemblyInitialize]
        public static void InitSerial(TestContext context)
        {
            SpeakerCraftAmplifier.Initialize("COM4");
        }

        [TestMethod]
        public void SendOnCommand_ShouldTurnTheAmpOn()
        {
            MZC_Gateway.SpeakerCraftAmplifier.SendOnCommand(1);
        }

        [TestMethod]
        public void SendOffCommand_ShouldTurnTheAmpOff()
        {
            MZC_Gateway.SpeakerCraftAmplifier.SendOffCommand(1);
        }

        [TestMethod]
        public void SendOnCommand_ShouldTurnTheAmpOnZone0()
        {
            MZC_Gateway.SpeakerCraftAmplifier.SendOnCommand(0);
        }

        [TestMethod]
        public void SendOffCommand_ShouldTurnTheAmpOffZone0()
        {
            MZC_Gateway.SpeakerCraftAmplifier.SendOffCommand(0);
        }

    }
}
