using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockchain.Tests
{
    [TestClass()]
    public class ChainTests
    {
        [TestMethod()]
        public void ChainTest()
        {
            var chain = new Chain();
            chain.Add("Code blog", "Admin");
            Assert.AreEqual(2, chain.Blocks.Count);
            Assert.AreEqual("Code blog", chain.Last.Data);
        }
    }
}