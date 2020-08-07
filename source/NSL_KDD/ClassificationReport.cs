using Accord.Math;
using Accord.Statistics.Analysis;
using System;
using System.Data;
using System.Linq;
using System.Text;

namespace NSL_KDD
{
    /// <summary>
    /// Класс, содержащий основную информацию из матрицы ошибок (confusion matrix)
    /// 
    /// TP - True Positive
    /// FP - False Positive
    /// TN - True Negative
    /// FN - False Negative
    /// </summary>
    public class ClassificationReport
    {
        /// <summary>
        /// Точность. Рассчитывается, как TP/(TP+FP)
        /// </summary>
        public double[] Precision { get; }

        /// <summary>
        /// Полнота.  Рассчитывается, как TP/(TP+FN)
        /// </summary>
        public double[] Recall { get; }

        /// <summary>
        /// Ф-Мера.  Рассчитывается, как 2*Recall*Precision/(Recall+Precision)
        /// </summary>
        public double[] FScore { get; }

        /// <summary>
        /// Количество верно определенных классов. TP
        /// </summary>
        public int[] Support { get; }

        /// <summary>
        /// Аккуратность. (TP+TN)/Total
        /// </summary>
        public double Accuracy { get; }

        /// <summary>
        /// Количество классов.
        /// </summary>
        public readonly int classes;

        /// <summary>
        /// Получает данные из метрицы ошибок и создает объект класса ClassificationReport 
        /// </summary>
        public static ClassificationReport GetReportFromConfusionMatrix(GeneralConfusionMatrix confusionMatrix)
        {
            return new ClassificationReport(
                confusionMatrix.Precision,
                confusionMatrix.Recall,
                confusionMatrix.PerClassMatrices.Select(m => m.FScore).ToArray<double>(),
                confusionMatrix.PerClassMatrices.Select(m => m.TruePositives).ToArray<int>(),
                confusionMatrix.Accuracy);
        }

        /// <summary>
        /// Конструктор класса ClassificationReport
        /// </summary>
        private ClassificationReport(double[] precision, double[] recall, double[] fScore, int[] support, double accuracy)
        {
            Precision = precision;
            Recall = recall;
            FScore = fScore;
            Support = support;
            Accuracy = accuracy;

            classes = precision.Length;
        }

        /// <summary>
        /// Строковое представление объекта класса ClassificationReport
        /// </summary>
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
