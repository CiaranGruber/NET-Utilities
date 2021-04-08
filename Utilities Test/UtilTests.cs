using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CG.Utilities.NET;
using System.Linq;
using System.Collections.Generic;

namespace UtilitiesTest
{
    [TestClass]
    public class UtilTests
    {
        [TestMethod]
        public void Test_GetObjSize()
        {
            Assert.AreEqual(2, Util.GetObjSize(new string[] { "Hello", "test" }), "Correct object size of string[] did not work");
            Assert.AreEqual(-1, Util.GetObjSize("Hello"), "Correct object size of string did not work");
            Assert.AreEqual(4, Util.GetObjSize(new object[] { 2, "Hello", new string[][] { new string[] { "Hello" } }, new List<string> { "Test" } }), "Correct object size of object[] did not work");
            Assert.AreNotEqual(5, Util.GetObjSize("Hello"), "Incorrect object size of string did not work");
        }

        [TestMethod]
        public void Test_ListEquality()
        {
            string string1 = "Hello";
            string string2 = "Test";
            string string3 = "Hello";
            string[] list1 = { "Hello", "Test" };
            string[] list2 = { "Hello", "Test" };
            string[][] list3 = { list1, new string[] { "Goodbye", "Testing" } };
            string[][] list4 = { list1, new string[] { "Goodbye", "Testing" } };
            int[,] list5 = { { 3, 5, 6 }, { 4, 5, 3 } };
            int[,] list6 = { { 3, 5, 6 }, { 4, 5, 3 } };
            object[] list7 = { list3, list1, list5, 3, "Tester" };
            object[] list8 = { list3, list1, list5, 3, "Tester" };
            string[] list9 = list4[0];
            string[] list10 = list4[1];

            Assert.IsFalse(Util.TrueEquality(string1, string2), "List Equality between two different strings did not pass as false");
            Assert.IsTrue(Util.TrueEquality(string1, string3), "List Equality between two strings did not pass as true");
            Assert.IsTrue(Util.TrueEquality(list1, list2), "List Equality between two string[]'s did not pass as true");
            Assert.IsTrue(Util.TrueEquality(list3, list4), "List Equality between two string[][]'s did not pass as true");
            Assert.IsTrue(Util.TrueEquality(list5, list6), "List Equality between two int[,]'s did not pass as true");
            Assert.IsTrue(Util.TrueEquality(list7, list8), "List Equality between two object[]'s did not pass as true");
            Assert.IsFalse(Util.TrueEquality(list9, list10), "List Equality between two different string[]'s did not pass as false");
        }

        [TestMethod]
        public void Test_ToReadableString()
        {
            Assert.AreEqual("[\"Hello\", 3]", new object[] { "Hello", 3 }.ToReadableString(), "ToReadableString() between object array did not work correctly");
            Assert.AreEqual("[\"Hello\", \"3\"]", new string[] { "Hello", "3" }.ToReadableString(), "ToReadableString() between string array did not work correctly");
            Assert.AreEqual("[\"Hello\", [\"Test\", \"Int\"], [['c', 'd'], [[4, 5], [233, 54453]]]]", new object[] { "Hello", new List<string> { "Test", "Int" }, new object[] { new char[] { 'c', 'd' }, new int[][] { new int[] { 4, 5 }, new int[] { 233, 54453 } } } }.ToReadableString(), "ToReadableString() between unusual object array did not work correctly");
        }

        [TestMethod]
        public void Test_ReturnRange()
        {
            int[] actual = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }.ReturnRange(3, 5).ToArray();
            int[] expected = new int[] { 3, 4, 5 };
            Assert.IsTrue(Util.TrueEquality(expected, actual), "Test did not work correctly.\n\nExpected:\n" + expected.ToReadableString() + "\n\nActual\n" + actual.ToReadableString());
            actual = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 }.ReturnRange(3, -2).ToArray();
            expected = new int[] { 3, 4, 5, 6 };
            Assert.IsTrue(Util.TrueEquality(expected, actual), "Test did not work correctly.\n\nExpected:\n" + expected.ToReadableString() + "\n\nActual\n" + actual.ToReadableString());
        }

        [TestMethod]
        public void Test_GetRandomString()
        {
            Assert.AreEqual(3, Util.GetRandomString(3).Length, "GetRandomString() did not get the right size");
            string randomString = Util.GetRandomString(10, 100, 54);
            foreach (char randomChar in randomString)
            {
                Assert.IsTrue(randomChar >= 54 && randomChar <= 100, "GetRandomString() did not restrict the range");
            }
        }
    }
}
