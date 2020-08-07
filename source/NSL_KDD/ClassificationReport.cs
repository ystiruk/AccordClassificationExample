using Accord.Math;
using Accord.Statistics.Analysis;
using System;
using System.Data;
using System.Linq;
using System.Text;

namespace NSL_KDD
{
    public class ClassificationReport
    {
        public double[] Precision { get; }
        public double[] Recall { get; }
        public double[] FScore { get; }
        public int[] Support { get; }
        public double Accuracy { get; }

        public readonly int classes;

        public static ClassificationReport GetReportFromConfusionMatrix(GeneralConfusionMatrix confusionMatrix)
        {
            return new ClassificationReport(
                confusionMatrix.Precision,
                confusionMatrix.Recall,
                confusionMatrix.PerClassMatrices.Select(m => m.FScore).ToArray<double>(),
                confusionMatrix.PerClassMatrices.Select(m => m.TruePositives).ToArray<int>(),
                confusionMatrix.Accuracy);
        }

        private ClassificationReport(double[] precision, double[] recall, double[] fScore, int[] support, double accuracy)
        {
            Precision = precision;
            Recall = recall;
            FScore = fScore;
            Support = support;
            Accuracy = accuracy;

            classes = precision.Length;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("i Precision Recall FScore Support");

            for (int i = 0; i < classes; i++)
                sb.AppendLine($"{i} {Math.Round(Precision[i], 2)} {Math.Round(Recall[i], 2)} {Math.Round(FScore[i], 2)} {Support[i]}");

            sb.AppendLine("Accuracy: " + Accuracy);
            return sb.ToString();
        }
    }
}
