using System;
using Xunit;
using ChatScriptBridge;

namespace ChatScriptBridgeTest
{
    public class CSTests
    {
        private ChatScriptClient _cs;

        public CSTests()
        {
            _cs = new ChatScriptClient("GOD", "localhost");
        }

        [Fact]
        public void IntroductionToConversation()
        {
            var result = _cs.GetBotResponse("");
            bool stuff = result.Equals("Initialise User") || result.Equals("Restart User");
            Assert.True(stuff, "Response should be canned!");
        }

        [Fact]
        public void TestAge()
        {
            var result = _cs.GetBotResponse("what age are you");
            var response = "I predate time itself.";

            Assert.Equal(response, result);
        }
    }
}
