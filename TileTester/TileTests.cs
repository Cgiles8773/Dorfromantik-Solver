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
            Assert.ThrowsException<ArgumentException>(() => new Tile(sections));
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
            Assert.IsTrue(Tile.GetSection(0).Typing.Equals("HOUSE"));
            Assert.AreEqual(Tile.GetSection(0).Count, 4);
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
            foreach (var section in sections)
            {
                Assert.IsTrue(section.Typing.Equals("FOREST"));
            }
        }
        [TestMethod, TestCategory("CalculateNeighbors")]
        public void CalculateRightNeighbors()
        {
            int[] neighbor = new int[6];
            for (int i = 0; i < 6; i++)
            {
                //For the values 1-6, calculate their neighbors and store them in the array
                neighbor[i] = Tile.CalculateRightNeighbor(i + 1);
            }
            Assert.AreEqual(neighbor[0], 2); //1 -> 2
            Assert.AreEqual(neighbor[1], 3); //2 -> 3
            Assert.AreEqual(neighbor[2], 4); //3 -> 4
            Assert.AreEqual(neighbor[3], 5); //4 -> 5
            Assert.AreEqual(neighbor[4], 6); //5 -> 6
            Assert.AreEqual(neighbor[5], 1); //6 -> 1
        }
        [TestMethod, TestCategory("CalculateNeighbors")]
        public void CalculateLeftNeighbors()
        {
            int[] neighbor = new int[6];
            for (int i = 0; i < 6; i++)
            {
                //For the values 1-6, calculate their neighbors and store them in the array
                neighbor[i] = Tile.CalculateLeftNeighbor(i + 1);
            }
            Assert.AreEqual(neighbor[0], 6); //1 -> 6
            Assert.AreEqual(neighbor[1], 1); //2 -> 1
            Assert.AreEqual(neighbor[2], 2); //3 -> 2
            Assert.AreEqual(neighbor[3], 3); //4 -> 3
            Assert.AreEqual(neighbor[4], 4); //5 -> 4
            Assert.AreEqual(neighbor[5], 5); //6 -> 5
        }
        [TestMethod, TestCategory("Grouping")]
        public void TestNoGroupTile()
        {
            List<Section> sections =
            [
                new Section("Forest"),
                new Section("House"),
                new Section("Plain"),
                new Section("Field"),
                new Section("Track"),
                new Section("River"),
                new Section("Water"),
            ];
            Tile Tile = new Tile(sections);
            IEnumerable<IEnumerable<Section>> Groups = Tile.GetGroups();
            Assert.IsTrue(Groups.Count() == 7);
            foreach (var grouping in Groups)
            {
                Assert.IsTrue(grouping.Count() == 1);
            }
        }
        [TestMethod, TestCategory("Grouping")]
        public void TestFullGroupTile()
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
            IEnumerable<IEnumerable<Section>> Groups = Tile.GetGroups();
            Assert.IsTrue(Groups.Count() == 1);
            foreach (var grouping in Groups)
            {
                Assert.IsTrue(grouping.Count() == 7);
                foreach (var section in grouping)
                {
                    sections.Remove(section);
                }
            }
            Assert.IsTrue(sections.Count == 0);
        }
        [TestMethod, TestCategory("Grouping")]
        public void TestRingGroupTile()
        {
            List<Section> sections =
                [
                new Section("Plain",0),
                new Section("Forest",1),
                new Section("Forest",2),
                new Section("Forest",3),
                new Section("Forest",4),
                new Section("Forest",5),
                new Section("Forest",6),
            ];
            Tile Tile = new Tile(sections);
            IEnumerable<IEnumerable<Section>> Groups = Tile.GetGroups();
            Assert.IsTrue(Groups.Count() == 2);
            Assert.IsTrue(Groups.ElementAt(0).Count() == 1);
            Assert.IsTrue(Groups.ElementAt(1).Count() == 6);
            foreach (var grouping in Groups)
            {
                foreach (var section in grouping)
                {
                    sections.Remove(section);
                }
            }
            Assert.IsTrue(sections.Count == 0);
        }
        [TestMethod, TestCategory("Grouping")]
        public void TestHalfGroupTile()
        {
            List<Section> sections =
            [
                new Section("Plain",0),
                new Section("Plain",1),
                new Section("Plain",2),
                new Section("Plain",3),
                new Section("Forest",4),
                new Section("Forest",5),
                new Section("Forest",6),
            ];
            Tile Tile = new Tile(sections);
            IEnumerable<IEnumerable<Section>> Groups = Tile.GetGroups();
            Assert.IsTrue(Groups.Count() == 2);
            Assert.IsTrue(Groups.ElementAt(0).Count() == 4);
            Assert.AreEqual(Groups.ElementAt(0).ElementAt(0).Typing, "PLAIN");
            Assert.AreEqual(Groups.ElementAt(1).ElementAt(0).Typing, "FOREST");
            Assert.IsTrue(Groups.ElementAt(1).Count() == 3);
            foreach (var grouping in Groups)
            {
                foreach (var section in grouping)
                {
                    sections.Remove(section);
                }
            }
            Assert.IsTrue(sections.Count == 0);
        }
        [TestMethod, TestCategory("Grouping")]
        public void TestMiscGroupTile()
        {
            List<Section> sections =
            [
                new Section("River",0),
                new Section("House",1),
                new Section("River",2),
                new Section("Field",3),
                new Section("Plain",4),
                new Section("River",5),
                new Section("House",6),
            ];
            Tile Tile = new Tile(sections);
            IEnumerable<IEnumerable<Section>> Groups = Tile.GetGroups();
            Assert.AreEqual(Groups.Count(), 4);
            foreach (var grouping in Groups)
            {
                if (grouping.ElementAt(0).Typing.Equals("RIVER"))
                {
                    Assert.AreEqual(grouping.Count(), 3);
                }
                if (grouping.ElementAt(0).Typing.Equals("HOUSE"))
                {
                    Assert.AreEqual(grouping.Count(), 2);
                }
                if (grouping.ElementAt(0).Typing.Equals("FIELD"))
                {
                    Assert.AreEqual(grouping.Count(), 1);
                }
                if (grouping.ElementAt(0).Typing.Equals("PLAIN"))
                {
                    Assert.AreEqual(grouping.Count(), 1);
                }
            }
            foreach (var grouping in Groups)
            {
                foreach (var section in grouping)
                {
                    sections.Remove(section);
                }
            }
            Assert.IsTrue(sections.Count == 0);
        }
        [TestMethod, TestCategory("Equality")]
        public void TestEquals()
        {
            List<Section> sections =
            [
                new Section("Plain",0),
                new Section("Plain",1),
                new Section("Plain",2),
                new Section("Plain",3),
                new Section("Plain",4),
                new Section("Plain",5),
                new Section("Plain",6),
            ];
            Tile Tile1 = new Tile(sections);
            Tile Tile2 = new Tile(sections);
            Assert.AreEqual(Tile1, Tile2);
        }
        [TestMethod, TestCategory("Equality")]
        public void TestNotEquals()
        {
            List<Section> sections =
            [
                new Section("Plain",0),
                new Section("Plain",1),
                new Section("Plain",2),
                new Section("Plain",3),
                new Section("Plain",4),
                new Section("Plain",5),
                new Section("Plain",6),
            ];
            Tile Tile1 = new Tile(sections);
            Tile Tile2 = new Tile(sections);
            Tile2.SetEdge(0, "Forest", 10);
            Assert.AreNotEqual(Tile1, Tile2);
        }
        [TestMethod, TestCategory("Equality")]
        public void TestHashCode()
        {
            List<Section> sections =
            [
                new Section("Plain",0),
                new Section("Plain",1),
                new Section("Plain",2),
                new Section("Plain",3),
                new Section("Plain",4),
                new Section("Plain",5),
                new Section("Plain",6),
            ];
            Tile Tile = new Tile(sections);
            int HashCode1 = Tile.GetHashCode();
            Tile = new Tile(sections);
            int HashCode2 = Tile.GetHashCode();
            Assert.AreEqual(HashCode2, HashCode1);
        }
        [TestMethod, TestCategory("Rotate")]
        public void Test1RotateClockwise()
        {
            List<Section> sections =
            [
                new Section("Plain",0),
                new Section("Plain",1),
                new Section("Plain",2),
                new Section("Plain",3),
                new Section("Plain",4),
                new Section("Plain",5),
                new Section("Plain",6),
            ];
            Tile Tile = new Tile(sections);
            Tile.RotateClockwise(1);
            List<Section> Sections = Tile.ToList();
            Sections.ElementAt(0).GetHashCode();
            new Section("Plain", 0).GetHashCode();
            Assert.AreEqual(Sections.ElementAt(0), new Section("Plain", 0));
            Assert.AreEqual(Sections.ElementAt(2), new Section("Plain", 1));
            Assert.AreEqual(Sections.ElementAt(3), new Section("Plain", 2));
            Assert.AreEqual(Sections.ElementAt(4), new Section("Plain", 3));
            Assert.AreEqual(Sections.ElementAt(5), new Section("Plain", 4));
            Assert.AreEqual(Sections.ElementAt(6), new Section("Plain", 5));
            Assert.AreEqual(Sections.ElementAt(1), new Section("Plain", 6));
        }
        [TestMethod, TestCategory("Rotate")]
        public void Test2RotateClockwise()
        {
            List<Section> sections =
            [
                new Section("Plain",0),
                new Section("Plain",1),
                new Section("Plain",2),
                new Section("Plain",3),
                new Section("Plain",4),
                new Section("Plain",5),
                new Section("Plain",6),
            ];
            Tile Tile = new Tile(sections);
            Tile.RotateClockwise(2);
            List<Section> Sections = Tile.ToList();
            Sections.ElementAt(0).GetHashCode();
            new Section("Plain", 0).GetHashCode();
            Assert.AreEqual(Sections.ElementAt(0), new Section("Plain", 0));
            Assert.AreEqual(Sections.ElementAt(3), new Section("Plain", 1));
            Assert.AreEqual(Sections.ElementAt(4), new Section("Plain", 2));
            Assert.AreEqual(Sections.ElementAt(5), new Section("Plain", 3));
            Assert.AreEqual(Sections.ElementAt(6), new Section("Plain", 4));
            Assert.AreEqual(Sections.ElementAt(1), new Section("Plain", 5));
            Assert.AreEqual(Sections.ElementAt(2), new Section("Plain", 6));
        }
        [TestMethod, TestCategory("Rotate")]
        public void Test3RotateClockwise()
        {
            List<Section> sections =
            [
                new Section("Plain",0),
                new Section("Plain",1),
                new Section("Plain",2),
                new Section("Plain",3),
                new Section("Plain",4),
                new Section("Plain",5),
                new Section("Plain",6),
            ];
            Tile Tile = new Tile(sections);
            Tile.RotateClockwise(3);
            List<Section> Sections = Tile.ToList();
            Sections.ElementAt(0).GetHashCode();
            new Section("Plain", 0).GetHashCode();
            Assert.AreEqual(Sections.ElementAt(0), new Section("Plain", 0));
            Assert.AreEqual(Sections.ElementAt(4), new Section("Plain", 1));
            Assert.AreEqual(Sections.ElementAt(5), new Section("Plain", 2));
            Assert.AreEqual(Sections.ElementAt(6), new Section("Plain", 3));
            Assert.AreEqual(Sections.ElementAt(1), new Section("Plain", 4));
            Assert.AreEqual(Sections.ElementAt(2), new Section("Plain", 5));
            Assert.AreEqual(Sections.ElementAt(3), new Section("Plain", 6));
        }
        [TestMethod, TestCategory("Rotate")]
        public void Test4RotateClockwise()
        {
            List<Section> sections =
            [
                new Section("Plain",0),
                new Section("Plain",1),
                new Section("Plain",2),
                new Section("Plain",3),
                new Section("Plain",4),
                new Section("Plain",5),
                new Section("Plain",6),
            ];
            Tile Tile = new Tile(sections);
            Tile.RotateClockwise(4);
            List<Section> Sections = Tile.ToList();
            Sections.ElementAt(0).GetHashCode();
            new Section("Plain", 0).GetHashCode();
            Assert.AreEqual(Sections.ElementAt(0), new Section("Plain", 0));
            Assert.AreEqual(Sections.ElementAt(5), new Section("Plain", 1));
            Assert.AreEqual(Sections.ElementAt(6), new Section("Plain", 2));
            Assert.AreEqual(Sections.ElementAt(1), new Section("Plain", 3));
            Assert.AreEqual(Sections.ElementAt(2), new Section("Plain", 4));
            Assert.AreEqual(Sections.ElementAt(3), new Section("Plain", 5));
            Assert.AreEqual(Sections.ElementAt(4), new Section("Plain", 6));
        }
        [TestMethod, TestCategory("Rotate")]
        public void Test5RotateClockwise()
        {
            List<Section> sections =
            [
                new Section("Plain",0),
                new Section("Plain",1),
                new Section("Plain",2),
                new Section("Plain",3),
                new Section("Plain",4),
                new Section("Plain",5),
                new Section("Plain",6),
            ];
            Tile Tile = new Tile(sections);
            Tile.RotateClockwise(5);
            List<Section> Sections = Tile.ToList();
            Sections.ElementAt(0).GetHashCode();
            new Section("Plain", 0).GetHashCode();
            Assert.AreEqual(Sections.ElementAt(0), new Section("Plain", 0));
            Assert.AreEqual(Sections.ElementAt(6), new Section("Plain", 1));
            Assert.AreEqual(Sections.ElementAt(1), new Section("Plain", 2));
            Assert.AreEqual(Sections.ElementAt(2), new Section("Plain", 3));
            Assert.AreEqual(Sections.ElementAt(3), new Section("Plain", 4));
            Assert.AreEqual(Sections.ElementAt(4), new Section("Plain", 5));
            Assert.AreEqual(Sections.ElementAt(5), new Section("Plain", 6));
        }
        [TestMethod, TestCategory("Rotate")]
        public void Test6RotateClockwise()
        {
            List<Section> sections =
            [
                new Section("Plain",0),
                new Section("Plain",1),
                new Section("Plain",2),
                new Section("Plain",3),
                new Section("Plain",4),
                new Section("Plain",5),
                new Section("Plain",6),
            ];
            Tile Tile = new Tile(sections);
            Tile.RotateClockwise(6);
            List<Section> Sections = Tile.ToList();
            Sections.ElementAt(0).GetHashCode();
            new Section("Plain", 0).GetHashCode();
            Assert.AreEqual(Sections.ElementAt(0), new Section("Plain", 0));
            Assert.AreEqual(Sections.ElementAt(1), new Section("Plain", 1));
            Assert.AreEqual(Sections.ElementAt(2), new Section("Plain", 2));
            Assert.AreEqual(Sections.ElementAt(3), new Section("Plain", 3));
            Assert.AreEqual(Sections.ElementAt(4), new Section("Plain", 4));
            Assert.AreEqual(Sections.ElementAt(5), new Section("Plain", 5));
            Assert.AreEqual(Sections.ElementAt(6), new Section("Plain", 6));
        }
        [TestMethod, TestCategory("Rotate")]
        public void Test0RotateClockwise()
        {
            List<Section> sections =
            [
                new Section("Plain",0),
                new Section("Plain",1),
                new Section("Plain",2),
                new Section("Plain",3),
                new Section("Plain",4),
                new Section("Plain",5),
                new Section("Plain",6),
            ];
            Tile Tile = new Tile(sections);
            Tile.RotateClockwise(0);
            List<Section> Sections = Tile.ToList();
            Sections.ElementAt(0).GetHashCode();
            new Section("Plain", 0).GetHashCode();
            Assert.AreEqual(Sections.ElementAt(0), new Section("Plain", 0));
            Assert.AreEqual(Sections.ElementAt(1), new Section("Plain", 1));
            Assert.AreEqual(Sections.ElementAt(2), new Section("Plain", 2));
            Assert.AreEqual(Sections.ElementAt(3), new Section("Plain", 3));
            Assert.AreEqual(Sections.ElementAt(4), new Section("Plain", 4));
            Assert.AreEqual(Sections.ElementAt(5), new Section("Plain", 5));
            Assert.AreEqual(Sections.ElementAt(6), new Section("Plain", 6));
        }
        [TestMethod, TestCategory("Rotate")]
        public void Test8RotateClockwise()
        {
            List<Section> sections =
           [
               new Section("Plain",0),
                new Section("Plain",1),
                new Section("Plain",2),
                new Section("Plain",3),
                new Section("Plain",4),
                new Section("Plain",5),
                new Section("Plain",6),
            ];
            Tile Tile = new Tile(sections);
            Tile.RotateClockwise(8);
            List<Section> Sections = Tile.ToList();
            Sections.ElementAt(0).GetHashCode();
            new Section("Plain", 0).GetHashCode();
            Assert.AreEqual(Sections.ElementAt(0), new Section("Plain", 0));
            Assert.AreEqual(Sections.ElementAt(3), new Section("Plain", 1));
            Assert.AreEqual(Sections.ElementAt(4), new Section("Plain", 2));
            Assert.AreEqual(Sections.ElementAt(5), new Section("Plain", 3));
            Assert.AreEqual(Sections.ElementAt(6), new Section("Plain", 4));
            Assert.AreEqual(Sections.ElementAt(1), new Section("Plain", 5));
            Assert.AreEqual(Sections.ElementAt(2), new Section("Plain", 6));
        }
    }
}