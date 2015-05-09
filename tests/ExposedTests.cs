using NUnit.Framework;

namespace Annoyances.Net.Tests
{

// don't warn about unused private fields
#pragma warning disable 169
#pragma warning disable 414

// ReSharper disable InconsistentNaming
// ReSharper disable ValueParameterNotUsed
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Local

    /// <summary>
    /// A class to test <seealso cref="Exposed"/> with.
    /// </summary>
    public class SomeClass
    {
        private bool m_private = true;
        protected bool m_protected = true;
        public bool m_public = true;

        private bool PrivateGetter
        {
            get { return true; }
        }

        protected bool ProtectedGetter
        {
            get { return true; }
        }

        public bool PublicGetter
        {
            get { return true; }
        }

        private bool PrivateProperty { get; set; }

        protected bool ProtectedProperty { get; set; }

        public bool PublicProperty { get; set; }

        private bool PrivateMethod()
        {
            return true;
        }

        protected bool ProtectedMethod()
        {
            return true;
        }

        public bool PublicMethod()
        {
            return true;
        }

        private bool this[int index]
        {
            get { return true; }
            set { }
        }
    }

// ReSharper restore UnusedParameter.Local
// ReSharper restore UnusedMember.Local
// ReSharper restore ValueParameterNotUsed
// ReSharper restore InconsistentNaming
#pragma warning restore 414
#pragma warning restore 169

    [TestFixture]
    public class ExposedTests
    {
        /// <summary>
        /// An exposed version of <see cref="SomeClass"/>, reset before each test.
        /// </summary>
        private dynamic m_exposed;

        [SetUp]
        public void SetUp()
        {
            m_exposed = new Exposed(new SomeClass());
        }

        [Test]
        public void TextExposedWithPrivateFieldExpectAccess()
        {
            Assert.That(m_exposed.m_private, Is.True);
        }

        [Test]
        public void TextExposedWithProtectedFieldExpectAccess()
        {
            Assert.That(m_exposed.m_protected, Is.True);
        }

        [Test]
        public void TextExposedWithPublicFieldExpectAccess()
        {
            Assert.That(m_exposed.m_public, Is.True);
        }

        [Test]
        public void TextExposedWithPrivateFieldSetterExpectAccess()
        {
            Assert.That(() => m_exposed.m_private = true, Throws.Nothing);
        }

        [Test]
        public void TextExposedWithProtectedFieldSetterExpectAccess()
        {
            Assert.That(() => m_exposed.m_protected = true, Throws.Nothing);
        }

        [Test]
        public void TextExposedWithPublicFieldSetterExpectAccess()
        {
            Assert.That(() => m_exposed.m_public = true, Throws.Nothing);
        }

        [Test]
        public void TestExposedWithPrivateGetterExpectAccess()
        {
            Assert.That(m_exposed.PrivateGetter, Is.True);
        }

        [Test]
        public void TestExposedWithProtectedGetterExpectAccess()
        {
            Assert.That(m_exposed.ProtectedGetter, Is.True);
        }

        [Test]
        public void TestExposedWithPublicGetterExpectAccess()
        {
            Assert.That(m_exposed.PublicGetter, Is.True);
        }

        [Test]
        public void TestExposedWithPrivatePropertyExpectAccess()
        {
            m_exposed.PrivateProperty = true;
            Assert.That(m_exposed.PrivateProperty, Is.True);
        }

        [Test]
        public void TestExposedWithProtectedPropertyExpectAccess()
        {
            m_exposed.ProtectedProperty = true;
            Assert.That(m_exposed.ProtectedProperty, Is.True);
        }

        [Test]
        public void TestExposedWithPublicPropertyExpectAccess()
        {
            m_exposed.PublicProperty = true;
            Assert.That(m_exposed.PublicProperty, Is.True);
        }

        [Test]
        public void TestExposedWithPrivateMethodExpectAccess()
        {
            Assert.That(m_exposed.PrivateMethod(), Is.True);
        }

        [Test]
        public void TestExposedWithProtectedMethodExpectAccess()
        {
            Assert.That(m_exposed.ProtectedMethod(), Is.True);
        }

        [Test]
        public void TestExposedWithPublicMethodExpectAccess()
        {
            Assert.That(m_exposed.PublicMethod(), Is.True);
        }

        [Test]
        public void TestExposedWithPrivateIndexerGetterExpectAccess()
        {
            Assert.That(m_exposed[0], Is.True);
        }

        [Test]
        public void TestExposedWithPrivateIndexerSetterExpectAccess()
        {
            Assert.That(() => m_exposed[0] = true, Throws.Nothing);
        }
    }
}
