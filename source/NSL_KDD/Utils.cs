using Accord.IO;
using Accord.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace NSL_KDD
{
    public static class Utils
    {
        public static void InferentTypes<T>(IEnumerable<T> values, out Type[] types)
        {
            var array = values.ToArray<T>();

            types = new Type[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                var item = array[i].ToString();

                if (item.Any(x => char.IsLetter(x)))
                    types[i] = typeof(object);
                else if (item.All(x => char.IsDigit(x)))
                    types[i] = typeof(int);
                else if (char.IsDigit(item[0]) && item.Contains('.'))
                    types[i] = typeof(double);
            }
        }

        public static DataTable ConcatDataTables(params DataTable[] tables)
        {
            DataTable result = new DataTable();
            for (int i = 0; i < tables.Length; i++)
                result.Merge(tables[i]);

            return result;
        }

        public static void SplitTrainTest(double[][] X, int[] Y, out double[][] x_train, out double[][] x_test, out int[] y_train, out int[] y_test, int percent = 75, int seed = 17)
        {
            if (X.GetLength(0) != Y.GetLength(0))
                throw new ArgumentOutOfRangeException(nameof(Y));

            Random r = new Random(seed);

            var size = X.GetLength(0);
            var size_train = (int)(size * percent / 100.0);
            var size_test = size - size_train;

            x_train = new double[size_train][];
            x_test = new double[size_test][];
            y_train = new int[size_train];
            y_test = new int[size_test];

            var rowOrder = Enumerable.Range(0, size).OrderBy(x => r.Next()).ToArray<int>();

            for (int i = 0; i < size_train; i++)
            {
                x_train[i] = X[rowOrder[i]];
                y_train[i] = Y[rowOrder[i]];
            }
            for (int i = size_train, j = 0; i < size; i++, j++)
            {
                x_test[j] = X[rowOrder[i]];
                y_test[j] = Y[rowOrder[i]];
            }
        }

        public static void PrepareData(out double[][] x_train, out double[][] x_test, out int[] y_train, out int[] y_test, out string[] y_labels, bool forcePrepare = false)
        {
            if (!forcePrepare && Cache.IsCacheExists)
            {
                Cache.LoadFromCache("x_train.txt", out x_train);
                Cache.LoadFromCache("x_test.txt", out x_test);
                Cache.LoadFromCache("y_train.txt", out y_train);
                Cache.LoadFromCache("y_test.txt", out y_test);
                Cache.LoadFromCache("y_labels.txt", out y_labels);
            }
            else
            {
                //[1] Read data from KDDTrain+.arff
                List<string> data_columns_list = new List<string>();
                Type[] data_columns_types;
                using (var sReader = new StreamReader(File.OpenRead($"{Settings.PathToData}\\KDDTrain+.arff")))
                {
                    sReader.ReadLine(); //skip first line

                    string line;
                    while ((line = sReader.ReadLine()).StartsWith("@attribute"))
                        data_columns_list.Add(line.Split()[1].Replace("'", ""));

                    var firstDataRow = sReader.ReadLine().Split(',');
                    Utils.InferentTypes(firstDataRow, out data_columns_types);
                }
                var data_columns = data_columns_list.ToArray();

                var data_columns_ext = new string[data_columns.Length + 1];
                Array.Copy(data_columns, data_columns_ext, data_columns.Length);
                data_columns_ext[data_columns_ext.Length - 1] = "difficulty";

                //[2] Read train data from file KDDTrain+.txt
                var df_train_reader = new CsvReader($"{Settings.PathToData}\\KDDTrain+.txt", hasHeaders: false);
                DataTable df_train = df_train_reader.ToTable(); //returns all columns with string type and empty headers
                df_train = df_train.ChangeTypes(data_columns_types); //transform types in order to be able to manipulate with them
                df_train.AssignHeaders(data_columns_ext); //add headers
                df_train.Columns.Remove("difficulty");

                //[3] Read test data from file KDDTest+.txt (same actions as above)
                var df_test_reader = new CsvReader($"{Settings.PathToData}\\KDDTest+.txt", hasHeaders: false);
                DataTable df_test = df_test_reader.ToTable();
                df_test = df_test.ChangeTypes(data_columns_types);
                df_test.AssignHeaders(data_columns_ext);
                df_test.Columns.Remove("difficulty");

                //[4] - optional 
                //Console.WriteLine(df_train.Head());
                //Console.WriteLine(df_test.Head());

                //[5] - optional
                //Console.WriteLine(df_train.ValueCounts<string>("class"));
                //Console.WriteLine(df_test.ValueCounts<string>("class"));

                //[6] Find classes existing in both train and test datasets
                var train_set = new HashSet<string>(df_train.Columns["class"].ToArray<string>());
                var test_set = new HashSet<string>(df_test.Columns["class"].ToArray<string>());
                var common_values = train_set.Intersect(test_set).ToArray<string>();
                
                //opt.
                //Console.WriteLine($"Common 'class' labels: {common_values.Length}");
                //Console.WriteLine(string.Join(", ", common_values));

                //[13] - optional
                //Console.WriteLine($"'class' objects count before deletion: train={df_train.Shape("class")}, test={df_test.Shape("class")}");

                //[14] Remove classes that don't present in common for both datasets classes
                df_train.RemoveRows(x => !common_values.Contains(x["class"]), acceptChanges: true);
                df_test.RemoveRows(x => !common_values.Contains(x["class"]), acceptChanges: true);
                
                //opt.
                //Console.WriteLine($"'class' objects count after deletion: train={df_train.Shape("class")}, test={df_test.Shape("class")}");

                //[15] Assign to string classes the corresponding numeric value
                LabelEncoder le = new LabelEncoder();
                y_train = le.FitTransform(df_train.Columns["class"]);
                y_test = le.Transform(df_test.Columns["class"]);
                y_labels = le.Classes;
                //opt.
                //Console.WriteLine(string.Join(", ", le.Classes));

                //[16] Do 'one-hot' encoding of a categorical data
                df_train.SetColumn("train", 1, acceptChanges: true); //add marker column in order to split sets back after one-hot
                df_test.SetColumn("train", 0, acceptChanges: true);

                var df_full = Utils.ConcatDataTables(df_train, df_test); //merge two datasets in one
                df_full.DropColumn("class", acceptChanges: true); //remove 'class' column

                Utils.InferentTypes(df_full.Rows[0].ItemArray, out data_columns_types); //convert types from string to more specific
                var df_full_encoded = df_full.OneHot(data_columns_types, dropFirst: true); //one-hot itself
                
                //split datasets back
                var X_train_encoded = df_full_encoded.AsEnumerable().Where(x => x["train"].ToString() == "1").CopyToDataTable();
                var X_test_encoded = df_full_encoded.AsEnumerable().Where(x => x["train"].ToString() == "0").CopyToDataTable();

                //..and remove marker column
                df_full_encoded.DropColumn("train", acceptChanges: true);
                X_train_encoded.DropColumn("train", acceptChanges: true);
                X_test_encoded.DropColumn("train", acceptChanges: true);

                //convert DataTable to double array - that dramatically increases speed of learning 
                x_train = X_train_encoded.ToJagged();
                x_test = X_test_encoded.ToJagged();

                Cache.SaveToCache(df_full_encoded, "df_full_encoded.txt");
                Cache.SaveToCache(x_train, "x_train.txt");
                Cache.SaveToCache(x_test, "x_test.txt");
                Cache.SaveToCache(y_train, "y_train.txt");
                Cache.SaveToCache(y_test, "y_test.txt");
                Cache.SaveToCache(le.Classes, "y_labels.txt");
            }
        }
    }
}
