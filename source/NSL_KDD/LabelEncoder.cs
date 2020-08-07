using Accord.Math;
using System.Data;
using System.Linq;

namespace NSL_KDD
{
    public class LabelEncoder
    {
        private int[] n_Classes;
        
        public string[] Classes { get; private set; }

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
