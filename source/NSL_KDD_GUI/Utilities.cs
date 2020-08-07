using NSL_KDD;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSL_KDD_GUI
{
    /// <summary>
    /// Вспомогательные методы
    /// </summary>
    public static class Utilities
    {
        /// <summary>
        /// Преобразует результаты из ClassificationReport в вид удобный для отображения в dataGridView
        /// </summary>
        public static DataTable ReportToDataTable(ClassificationReport report)
        {
            var result = new DataTable();

            result.Columns.Add(new DataColumn("Class", typeof(int)));
            result.Columns.Add(new DataColumn("Precision", typeof(double)));
            result.Columns.Add(new DataColumn("Recall", typeof(double)));
            result.Columns.Add(new DataColumn("FScore", typeof(double)));
            result.Columns.Add(new DataColumn("Support", typeof(double)));

            for (int i = 0; i < report.classes; i++)
            {
                result.Rows.Add(new object[] { i + 1, report.Precision[i], report.Recall[i], report.FScore[i], report.Support[i] });
            }

            return result;
        }

        /// <summary>
        /// Метод отображения окошка предупреждения
        /// </summary>
        public static bool ConfirmMessageBox(string text)
        {
            var result = MessageBox.Show(text, "Important",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);

            return (result == DialogResult.OK);
        }

        /// <summary>
        /// Метод отображения окошка с информацией
        /// </summary>
        public static bool InfoMessageBox(string text)
        {
            var result = MessageBox.Show(text, "Info",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1);

            return (result == DialogResult.OK);
        }
    }
}
