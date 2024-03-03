using TileClass;

namespace TileTester
{
    [TestClass]
    public class TileTests
    {
        [TestMethod, TestCategory("Section")]
        public void TestSectionConstructor()
        {
            Section section = new Section();
            Assert.IsTrue(section.Typing.Equals(""));
            Assert.IsTrue(section.Count.Equals(0));
        }
        [TestMethod, TestCategory("Section")]
        public void TestSectionConstructor2() 
        {
            Section section = new Section("Plain");
            Assert.IsTrue(section.Typing.Equals("PLAIN"));
            Assert.IsTrue(section.Count.Equals(0));
        }
        [TestMethod, TestCategory("Section")]
        public void TestSectionConstructor3()
        {
            Section section = new Section("Forest");
            Assert.IsTrue(section.Typing.Equals("FOREST"));
            Assert.IsTrue(section.Count.Equals(0));
        }
        [TestMethod, TestCategory("Section")]
        public void TestSectionConstructor4()
        {
            Section section = new Section("Field");
            Assert.IsTrue(section.Typing.Equals("FIELD"));
            Assert.IsTrue(section.Count.Equals(0));
        }
        [TestMethod, TestCategory("Section")]
        public void TestSectionConstructor5()
        {
            Section section = new Section("House");
            Assert.IsTrue(section.Typing.Equals("HOUSE"));
            Assert.IsTrue(section.Count.Equals(0));
        }
        [TestMethod, TestCategory("Section")]
        public void TestSectionConstructor6()
        {
            Section section = new Section("River");
            Assert.IsTrue(section.Typing.Equals("RIVER"));
            Assert.IsTrue(section.Count.Equals(0));
        }
        [TestMethod, TestCategory("Section")]
        public void TestSectionConstructor7()
        {
            Section section = new Section("Track");
            Assert.IsTrue(section.Typing.Equals("TRACK"));
            Assert.IsTrue(section.Count.Equals(0));
        }
        [TestMethod, TestCategory("Section")]
        public void TestSectionConstructor8()
        {
            Section section = new Section("Station");
            Assert.IsTrue(section.Typing.Equals("STATION"));
            Assert.IsTrue(section.Count.Equals(0));
        }
        [TestMethod, TestCategory("Section")]
        public void TestSectionConstructor9()
        {
            Assert.ThrowsException<ArgumentException>(() => new Section("Sation"));
        }
        [TestMethod, TestCategory("Section")]
        public void TestSectionConstructor10()
        {
            Section section = new Section("Station", 10);
            Assert.IsTrue(section.Typing.Equals("STATION"));
            Assert.IsTrue(section.Count.Equals(10));
        }
        [TestMethod, TestCategory("Section")]
        public void TestSetCount()
        {
            Section section = new Section();
            section.SetCount(10);
            Assert.IsTrue(section.Count.Equals(10));
        }
    }
}