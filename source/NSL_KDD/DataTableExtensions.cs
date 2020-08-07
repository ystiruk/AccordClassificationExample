using Accord.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NSL_KDD
{
    public static class DataTableExtensions
    {
        public static void AssignHeaders(this DataTable dataTable, string[] headers, bool acceptChanges = false)
        {
            if (dataTable.Columns.Count != headers.Length)
                throw new ArgumentOutOfRangeException(nameof(headers));

            for (int i = 0; i < headers.Length; i++)
                dataTable.Columns[i].ColumnName = headers[i];

            if (acceptChanges)
                dataTable.AcceptChanges();
        }

        public static string[] GetHeaders(this DataTable dataTable)
        {
            List<string> headers = new List<string>();

            for (int i = 0; i < dataTable.Columns.Count; i++)
                headers.Add(dataTable.Columns[i].ColumnName);

            return headers.ToArray();
        }

        public static string Head(this DataTable dataTable, int count = 5)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Join(" ", dataTable.GetHeaders()));

            var firstRows = dataTable.AsEnumerable().Take(count).Select(x => x.ItemArray);
            foreach (var row in firstRows)
                sb.AppendLine(string.Join(" ", row));

            return sb.ToString();
        }

        public static string ValueCounts<T>(this DataTable dataTable, string columnName)
        {
            StringBuilder sb = new StringBuilder();

            var values = dataTable.Columns[columnName].ToArray<T>();

            var items = values
                .GroupBy(x => x)
                .ToDictionary(k => k.Key, v => v.Count())
                .OrderByDescending(x => x.Value);

            foreach (var item in items)
                sb.AppendLine($"{item.Key} {item.Value}");

            return sb.ToString();
        }

        public static int Shape(this DataTable dataTable, int columnIndex)
        {
            return dataTable.Columns[columnIndex].ToArray<string>().Length;
        }

        public static int Shape(this DataTable dataTable, string columnName)
        {
            return dataTable.Columns[columnName].ToArray<string>().Length;
        }

        public static void RemoveRows(this DataTable dataTable, Func<DataRow, bool> predicate, bool acceptChanges = false)
        {
            dataTable.Rows.Cast<DataRow>().Where(predicate).ToList().ForEach(x => x.Delete());

            if (acceptChanges)
                dataTable.AcceptChanges();
        }
        
        public static void SetColumn(this DataTable dataTable, string columnName, int value, bool acceptChanges = false)
        {
            if (!dataTable.Columns.Contains(columnName))
                dataTable.Columns.Add(new DataColumn(columnName, typeof(object)));

            foreach (DataRow row in dataTable.Rows)
                row[columnName] = value;

            if (acceptChanges)
                dataTable.AcceptChanges();
        }

        public static void SetColumn(this DataTable dataTable, string columnName, double[] values, bool acceptChanges = false)
        {
            if (!dataTable.Columns.Contains(columnName))
                dataTable.Columns.Add(new DataColumn(columnName, typeof(object)));

            var colIndex = dataTable.Columns.IndexOf(columnName);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];
                row[colIndex] = values[i];
            }

            if (acceptChanges)
                dataTable.AcceptChanges();
        }

        public static void DropColumn(this DataTable dataTable, string columnName, bool acceptChanges = false)
        {
            if (!dataTable.Columns.Contains(columnName))
                throw new InvalidOperationException(nameof(columnName));

            dataTable.Columns.Remove(columnName);

            if (acceptChanges)
                dataTable.AcceptChanges();
        }

        public static DataTable OneHot(this DataTable dataTable, Type[] columnTypes, bool dropFirst = false)
        {
            DataTable result = new DataTable();
            result.Merge(dataTable);

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                Console.WriteLine($"Processing {i}'th column of {dataTable.Columns.Count}"); //TODO remove

                if (columnTypes[i] == typeof(object))
                {
                    LabelEncoder le = new LabelEncoder();
                    var values = le.FitTransform(dataTable.Columns[i]);

                    var oneHotValues = Matrix.OneHot(values);
                    var oneHotLabels = le.Classes.Select(x => $"{dataTable.Columns[i].ColumnName}_{x}").ToArray<string>();

                    Console.WriteLine($"Shrink with {le.Classes.Length} columns");

                    int j = (dropFirst ? 1 : 0);
                    for (; j < le.Classes.Length; j++)
                    {
                        Console.Write(j + " ");
                        result.SetColumn(oneHotLabels[j], Matrix.GetColumn(oneHotValues, j));
                    }
                }
            }

            for (int i = 0; i < dataTable.Columns.Count; i++)
                if (columnTypes[i] == typeof(object))
                    result.DropColumn(dataTable.Columns[i].ColumnName);

            return result;
        }
        
        public static DataTable ChangeTypes(this DataTable dataTable, Type[] columnTypes)
        {
            if (columnTypes.Length < dataTable.Columns.Count)
                columnTypes = columnTypes.Concatenate(Enumerable.Repeat(typeof(object), dataTable.Columns.Count - columnTypes.Length).ToArray<Type>());

            DataTable result = new DataTable();

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                var columnCopy = new DataColumn(dataTable.Columns[i].ColumnName, columnTypes[i]);
                result.Columns.Add(columnCopy);
            }

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i];

                object[] rowCopy = row.ItemArray;
                for (int j = 0; j < rowCopy.Length; j++)
                    rowCopy[j] = Convert.ChangeType(row.ItemArray[j], columnTypes[j]);
                result.Rows.Add(rowCopy);
            }

            return result;
        }

        public static DataTable ChangeTypes(this DataTable dataTable, Type columnType)
        {
            Type[] types = Enumerable.Repeat(typeof(double), dataTable.Columns.Count).ToArray<Type>();
            return dataTable.ChangeTypes(types);
        }
    }
}
