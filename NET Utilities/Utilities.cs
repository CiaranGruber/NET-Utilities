using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CG.Utilities.NET
{
    public static class Util
    {
        private readonly static Random rand = new Random();

        /// <summary>
        /// Finds anythings size. If it is not an IEnumerable it will set it to -1
        /// </summary>
        /// <param name="theObject">The object to be measured</param>
        /// <returns></returns>
        public static int GetObjSize(object theObject)
        {
            int objectSize = -1;
            bool keepGoing = true;
            bool isList = false;

            if (!(theObject is string))
            {
                try
                {
                    object[] thing = ((IEnumerable)theObject).Cast<object>().Select(x => x == null ? x : x.ToString()).ToArray();
                    isList = true;
                }
                catch { }
            }

            if (isList)
            {
                objectSize = 0;
                object[] objectList = ((IEnumerable)theObject).Cast<object>().Select(x => x == null ? x : x.ToString()).ToArray();
                while (keepGoing)
                {
                    try
                    {
                        object testIfWorks = objectList[objectSize];
                        objectSize++;
                    }
                    catch
                    {
                        keepGoing = false;
                    }
                }
            }
            return objectSize;
        }

        /// <summary>
        /// Returns whether two objects are equal in value (including lists)
        /// </summary>
        /// <param name="object1">The first object to compare</param>
        /// <param name="object2">The second object to compare</param>
        /// <returns>Whether the two object's values are equal</returns>
        public static bool TrueEquality(object object1, object object2)
        {
            int objectSize1 = GetObjSize(object1);
            object[] convertedObject1;
            object[] convertedObject2;

            if (objectSize1 != GetObjSize(object2)) return false;
            if (objectSize1 == -1)
            {
                if (!object1.Equals(object2))
                {
                    return false;
                }
                return true;
            }

            convertedObject1 = ((IEnumerable)object1).Cast<object>().ToArray();
            convertedObject2 = ((IEnumerable)object2).Cast<object>().ToArray();

            for (int i = 0; i < objectSize1; i++)
            {
                if (!TrueEquality(convertedObject1[i], convertedObject2[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>Returns a human readable version of any IEnumerables or other objects</summary>
        /// <param name="itemOrList">The item or list to convert</param>
        /// <returns>A human readable version of any IEnumerables or other objects</returns>
        public static string ToReadableString(this object itemOrList)
        {
            string currentString = "";
            int size = GetObjSize(itemOrList);

            if (size > -1)
            {
                currentString += "[";
                for (int i = 0; i < size; i++)
                {
                    currentString += ((IEnumerable)itemOrList).Cast<object>().ToList()[i].ToReadableString();
                    if (i < size - 1)
                    {
                        currentString += ", ";
                    }
                }
                currentString += "]";
            }
            else
            {
                if (itemOrList is string)
                {
                    currentString += "\"";
                }
                else if (itemOrList is char)
                {
                    currentString += "'";
                }
                currentString += itemOrList;
                if (itemOrList is string)
                {
                    currentString += "\"";
                }
                else if (itemOrList is char)
                {
                    currentString += "'";
                }
            }
            return currentString;
        }

        /// <summary>Returns the inclusive range of any IEnumerable</summary>
        /// <typeparam name="T">The type of the IEnumerable</typeparam>
        /// <param name="originalEnumerable">The original IEnumerable</param>
        /// <param name="startNum">The index to start from</param>
        /// <param name="endNum">The index to finish on (negatives count backwards)</param>
        /// <returns>The range of the inputted IEnumerable</returns>
        public static IEnumerable<T> ReturnRange<T>(this IEnumerable<T> originalEnumerable, int startNum, int endNum)
        {
            int index = 0;
            ICollection<T> trimmedCollection = new List<T>();
            if (endNum < 0)
            {
                endNum += originalEnumerable.Count();
            }
            foreach (T obj in originalEnumerable)
            {
                if (index > endNum)
                {
                    break;
                }
                else if (index >= startNum)
                {
                    trimmedCollection.Add(obj);
                }
                index++;
            }
            return trimmedCollection;
        }

        /// <summary>Converts any char enumberable to a string</summary>
        /// <param name="charArray">The character array</param>
        /// <returns>The finalised string</returns>
        public static string ArrayToString(this IEnumerable<char> charArray)
        {
            string returning = "";
            foreach (char character in charArray)
            {
                returning += character;
            }
            return returning;
        }

        /// <summary>Gets a string populated with random characters</summary>
        /// <param name="size">Maximum size of the string</param>
        /// <param name="maxValue">Highest value the character can be</param>
        /// <param name="minValue">Lowest value the character can be</param>
        /// <returns>A string populated with random characters</returns>
        public static string GetRandomString(int size, int maxValue = char.MaxValue, int minValue = 0)
        {
            string randomisedString = "";
            for (int i = 0; i < size; i++)
            {
                randomisedString += (char)rand.Next(minValue, maxValue);
            }
            return randomisedString;
        }
    }
}
