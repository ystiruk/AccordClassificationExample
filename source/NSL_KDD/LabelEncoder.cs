using Accord.Math;
using System.Data;
using System.Linq;

namespace NSL_KDD
{
    /// <summary>
    /// Осуществляет кодирование категориальных признаков в числа от 0 до n_Classes-1
    /// </summary>
    public class LabelEncoder
    {
        /// <summary>
        /// Исходные значения признаков
        /// </summary>
        public string[] Classes { get; private set; }
         
        /// <summary>
        /// Числовые метки признаков
        /// </summary>
        private int[] n_Classes;

        /// <summary>
        /// Обучается на значениях признаков из колонки dataColumn и назначает числовые метки
        /// </summary>
        public int[] FitTransform(DataColumn dataColumn)
        {
            var values = dataColumn.ToArray<string>();

            Classes = values.Distinct().OrderBy(x => x).ToArray<string>();
            n_Classes = Enumerable.Range(0, Classes.Length).ToArray<int>();

            int[] result = new int[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = n_Classes[Classes.IndexOf(values[i])];
            }

            return result;
        }

        /// <summary>
        /// Назначает числовые метки
        /// </summary>
        public int[] Transform(DataColumn dataColumn)
        {
            var values = dataColumn.ToArray<string>();

            int[] result = new int[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                result[i] = n_Classes[Classes.IndexOf(values[i])];
            }

            return result;
        }
    }
}
