using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockchain.Tests
{
    [TestClass()]
    public class BlockTests
    {
        [TestMethod()]
        public void SerializeTest()
        {
            var block = new Block();
            var json = "{\"Created\":\"\\/Date(1578776400000+0300)\\/\",\"Data\":\"Hello, world\",\"Hash\":\"98818ea6fb3fab875e008ede949c0c80ca114abc1c3e69b75c13d5a1cbb1df05\",\"PreviousHash\":\"111111\",\"User\":\"Admin\"}";
            var resultSring = block.Serialize();

            Assert.AreEqual(json, resultSring);

            var resultBlock = Block.Deserialize(resultSring);

            Assert.AreEqual(block.Hash, resultBlock.Hash);
            Assert.AreEqual(block.Created, resultBlock.Created);
            Assert.AreEqual(block.Data, resultBlock.Data);
            Assert.AreEqual(block.PreviousHash, resultBlock.PreviousHash);
            Assert.AreEqual(block.User, resultBlock.User);
        }
    }
}