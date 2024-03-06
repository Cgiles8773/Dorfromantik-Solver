using Solver;
using System.Diagnostics;

namespace TileTester
{
    [TestClass]
    public class TileTests
    {
        /// <summary>
        /// The Section category tests the constructor for the Section class
        /// </summary>
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
        /// <summary>
        /// The list constructor requires 7 sections
        /// </summary>
        [TestMethod, TestCategory("Constructor")]
        public void TestListConstructorThrowsTooMany()
        {
            List<Section> sections =
            [
                new Section(),
                new Section(),
                new Section(),
                new Section(),
                new Section(),
                new Section(),
                new Section(),
                new Section() ///Eighth element
            ];
            Assert.ThrowsException<ArgumentException>(()=>new Tile(sections));
        }
        /// <summary>
        /// The list constructor requires 7 sections
        /// </summary>
        [TestMethod, TestCategory("Constructor")]
        public void TestListConstructorThrowsTooFew()
        {
            List<Section> sections =
            [
                new Section(),
                new Section()
            ];
            Assert.ThrowsException<ArgumentException>(() => new Tile(sections));
        }
        /// <summary>
        /// The subtile constructor only assigns the subtile
        /// </summary>
        [TestMethod, TestCategory("Constructor")]
        public void TestSubtileConstructor()
        {
            Tile Tile = new Tile("House", 4);
            Assert.IsTrue(Tile.Subtile.Typing.Equals("HOUSE"));
            Assert.AreEqual(Tile.Subtile.Count, 4);
            List<Section> list = (List<Section>)Tile.ToList();
            for (int i = 1; i < 7; i++)
            {
                Assert.IsTrue(list[i].Typing.Equals(""));
            }
        }
        /// <summary>
        /// An empty tile returns an empty list
        /// </summary>
        [TestMethod, TestCategory("ToList")]
        public void TestEmptyToList()
        {
            Tile Tile = new Tile();
            foreach (Section segment in Tile.ToList())
            {
                Assert.AreEqual(0, segment.Count);
                Assert.IsTrue(segment.Typing.Equals(""));
            }
        }
        /// <summary>
        /// A partial tile returns a partially full list
        /// </summary>
        [TestMethod, TestCategory("ToList")]
        public void TestPartialToList()
        {
            List<Section> sections =
            [
                new Section("Forest"),
                new Section("Forest"),
                new Section(),
                new Section(),
                new Section("Forest"),
                new Section(),
                new Section(),
            ];
            Tile Tile = new Tile(sections);
            List<Section> list = (List<Section>)Tile.ToList();
            Assert.AreEqual(7, list.Count);
            Assert.IsTrue(list.ElementAt(0).Typing.Equals("FOREST"));
            Assert.IsTrue(list.ElementAt(1).Typing.Equals("FOREST"));
            Assert.IsTrue(list.ElementAt(2).Typing.Equals(""));
            Assert.IsTrue(list.ElementAt(3).Typing.Equals(""));
            Assert.IsTrue(list.ElementAt(4).Typing.Equals("FOREST"));
            Assert.IsTrue(list.ElementAt(5).Typing.Equals(""));
            Assert.IsTrue(list.ElementAt(6).Typing.Equals(""));
        }
        /// <summary>
        /// A full tile returns a full list
        /// </summary>
        [TestMethod, TestCategory("ToList")]
        public void TestToList()
        {
            List<Section> sections =
            [
                new Section("Forest"),
                new Section("Forest"),
                new Section("Forest"),
                new Section("Forest"),
                new Section("Forest"),
                new Section("Forest"),
                new Section("Forest"),
            ];
            Tile Tile = new Tile(sections);
            List<Section> list = (List<Section>)Tile.ToList();
            Assert.AreEqual(7, list.Count);
            foreach ( var section in sections ) 
            {
                Assert.IsTrue(section.Typing.Equals("FOREST"));
            }
        }
    }
}