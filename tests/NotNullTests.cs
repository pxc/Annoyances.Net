using NUnit.Framework;

namespace Annoyances.Net.Tests
{
    [TestFixture]
    public static class NotNullTests
    {
        [Test]
        public static void TestCreateWithNullExpectException()
        {
            Assert.That(() => NotNull<object>.Create(null), Throws.Exception);
        }

        [Test]
        public static void TestCreateWithNotNullExpectSuccess()
        {
            Assert.That(() => NotNull<object>.Create(new object()), Throws.Nothing);
        }

        [Test]
        public static void TestImplicitConversionWithNotNullExpectSuccess()
        {
            NotNull<string> nn = NotNull<string>.Create("test");            

            Assert.That(((string)nn).Length, Is.EqualTo(4));
        }

        [Test]
        public static void TestValueWithNotNullExpectSuccess()
        {
            NotNull<string> nn = NotNull<string>.Create("test");

            Assert.That(nn.Value.Length, Is.EqualTo(4));
        }
    }
}
