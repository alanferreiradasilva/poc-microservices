namespace MicroShopPOC.Tests
{
    public class Class1
    {
        [Test]
        public async Task Test1()
        {
            var result = true;
            await Assert.That(result).IsTrue();
        }
    }
}
