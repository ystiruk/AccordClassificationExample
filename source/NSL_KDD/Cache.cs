﻿using Accord.IO;
using Accord.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace NSL_KDD
{
    //Represents cache. 
    //Can store and read back objects of types {DataTable, double[][] and int[]}
    public static class Cache
    {
        public static bool IsCacheExists { get { return Directory.Exists(Settings.PathToDataCache); } }

        public static void LoadFromCache(string fileName, out DataTable dataTable)
        {
            fileName = Path.Combine(Settings.PathToDataCache, fileName);

            using (var reader = new CsvReader(fileName, hasHeaders: true))
            {
                dataTable = reader.ToTable();
                dataTable = dataTable.ChangeTypes(typeof(double));
            }
        }
        public static void LoadFromCache(string fileName, out double[][] jagged)
        {
            fileName = Path.Combine(Settings.PathToDataCache, fileName);
            
            List<double[]> cachedJagged = new List<double[]>();
            using (var reader = new StreamReader(fileName))
            {
                while (!reader.EndOfStream)
                {
                    double[] row = reader.ReadLine()
                        .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => double.Parse(x))
                        .ToArray<double>();

                    cachedJagged.Add(row);
                }
            }

            jagged = new double[cachedJagged.Count][];
            for (int i = 0; i < jagged.Length; i++)
                jagged[i] = cachedJagged[i];
        }
        public static void LoadFromCache(string fileName, out int[] vector)
        {
            fileName = Path.Combine(Settings.PathToDataCache, fileName);

            using (var reader = new StreamReader(fileName))
            {
                var data = reader.ReadToEnd();

                vector = data
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x)).ToArray<int>();
            }
        }
        public static void LoadFromCache(string fileName, out string[] vector)
        {
            fileName = Path.Combine(Settings.PathToDataCache, fileName);

            using (var reader = new StreamReader(fileName))
            {
                var data = reader.ReadToEnd();

                vector = data
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x).ToArray<string>();
            }
        }

        public static void SaveToCache(DataTable dataTable, string fileName)
        {
            fileName = Path.Combine(Settings.PathToDataCache, fileName);

            Directory.CreateDirectory(Settings.PathToDataCache);
            using (File.CreateText(fileName)) { }

            using (CsvWriter writer = new CsvWriter(fileName))
                writer.Write(dataTable);
        }
        public static void SaveToCache(double[][] jagged, string fileName)
        {
            fileName = Path.Combine(Settings.PathToDataCache, fileName);

            Directory.CreateDirectory(Settings.PathToDataCache);
            using (File.CreateText(fileName)) { }

            using (var writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < jagged.Length; i++)
                {
                    for (int j = 0; j < jagged[i].Length; j++)
                        writer.Write(jagged[i][j] + " ");

                    writer.WriteLine();
                }
            }
        }
        public static void SaveToCache<T>(T[] array, string fileName)
        {
            fileName = Path.Combine(Settings.PathToDataCache, fileName);

            Directory.CreateDirectory(Settings.PathToDataCache);
            using (File.CreateText(fileName)) { }

            using (var writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < array.Length; i++)
                    writer.Write(array[i] + " ");
            }
        }
    }
}
