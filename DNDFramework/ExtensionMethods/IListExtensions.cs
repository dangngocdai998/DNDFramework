using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmplitudeNS.MiniJSON;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Linq;
using Newtonsoft.Json;

namespace DNDFramework
{
    public static class IListExtensions
    {
        /// <summary>
        /// Shuffle the list in place using the Fisher-Yates method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static string CorrectString(this string input)
        {
            if (String.IsNullOrEmpty(input))
                return null;
            return input.Replace("\r", string.Empty);
        }
        /// <summary>
        /// Return a random item from the list.
        /// Sampling with replacement.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RandomItem<T>(this IList<T> list)
        {
            if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list");
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        /// <summary>
        /// Removes a random item from the list, returning that item.
        /// Sampling without replacement.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T RemoveRandom<T>(this IList<T> list)
        {
            if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot remove a random item from an empty list");
            int index = UnityEngine.Random.Range(0, list.Count);
            T item = list[index];
            list.RemoveAt(index);
            return item;
        }

        public static string ToJson<T>(this T obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public static string A<T>(this string list)
        {
            return "";
        }
        private static string[] longExponents =
        {
            "",
            "Thousand",
            "Million",
            "Billion",
            "Trillion",
            "Quadrillion",
            "Quintillion",
            "Sextillion",
            "Septillion",
            "Octillion",
            "Nonillion",
            "Decillion",
            "Undecillion",
            "Duodecillion",
            "Tredecillion",
            "Quattuordecillion",
            "Quindecillion",
            "Sexdecillion",
            "Septendecillion",
            "Octodecillion",
            "Novendecillion",
            "Vigintillion",
            "Unvigintillion",
            "Duovigintillion",
            "Trevigintillion",
            "Quattuorvigintillion",
            "Quinvigintillion",
            "Sexvigintillion",
            "Septenvigintillion",
            "Octovigintillion",
            "Novemvigintillion",
            "Trigintillion",
            "Untrigintillion",
            "Duotrigintillion",
            "Tretrigintillion"
        };

        //  
        // 10^105 - Quattuortrigintillion
        // 10^108 - Quintrigintillion
        // 10^111 - Sextrigintillion
        // 10^114 - Septentrigintillion
        // 10^117 - Octotrigintillion
        // 10^120 - Novemtrigintillion
        //
        // 10^123 - Quadragintillion
        // 10^126 - Unquadragintillion
        // Duoquadragintillion
        //  Trequadragintillion
        //  Quattuorquadragintillion
        // Quinquadragintillion
        // Sexquadragintillion
        // Septenquadragintillion
        // Octoquadragintillion
        // Novemquadragintillion

        private static string[] shortExponents =
            {"", "k", "M", "B", "T", "Q", "aa", "bb", "cc", "dd", "ee", "ff", "gg", "hh", "ii"};


        public static string FormatNumber(this double number, int digits, bool useLongExponents)
        {
            int exponentsCounter = 0;

            while (number / 1000 > 1)
            {
                exponentsCounter++;
                if (exponentsCounter >= longExponents.Length) exponentsCounter = 0;
                number /= 1000;
            }

            string numberAsString;

            switch (digits)
            {
                case 3:
                    numberAsString = string.Format("{0:F3}", number).Contains(".")
                        ? string.Format("{0:F3}", number).TrimEnd('0').TrimEnd('.')
                        : string.Format("{0:F3}", number);
                    break;

                case 2:
                    numberAsString = string.Format("{0:F2}", number).Contains(".")
                        ? string.Format("{0:F2}", number).TrimEnd('0').TrimEnd('.')
                        : string.Format("{0:F2}", number);
                    break;

                case 1:
                    numberAsString = string.Format("{0:F1}", number).Contains(".")
                        ? string.Format("{0:F1}", number).TrimEnd('0').TrimEnd('.')
                        : string.Format("{0:F1}", number);
                    break;

                default:
                    numberAsString = string.Format("{0:F0}", number).Contains(".")
                        ? string.Format("{0:F0}", number).TrimEnd('0').TrimEnd('.')
                        : string.Format("{0:F0}", number);
                    break;
            }

            // currentMoneyExponentCounter = exponentsCounter;
            // currentMoneyExponent = longExponents[exponentsCounter];

            return useLongExponents
                ? numberAsString + " " + longExponents[exponentsCounter]
                : numberAsString + " " + shortExponents[exponentsCounter];
        }
    }
}
//         public static long MilliToSecond<long>(this obj)
//         {
//  return 
//         }
/*  public static T[] ConcatArrays<T>(params T[][] args)
 {
     if (args == null)
         throw new ArgumentNullException();

     var offset = 0;
     var newLength = args.Sum(arr => arr.Length);
     var newArray = new T[newLength];

     foreach (var arr in args)
     {
         Buffer.BlockCopy(arr, 0, newArray, offset, arr.Length);
         offset += arr.Length;
     }

     return newArray;
 } */
