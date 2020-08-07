using Accord.MachineLearning.Bayes;
using Accord.MachineLearning.Performance;
using Accord.Math;
using Accord.Math.Optimization;
using Accord.Statistics.Analysis;
using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Models.Regression;
using Accord.Statistics.Models.Regression.Fitting;
using Accord.Statistics.Visualizations;
using NSL_KDD;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace NSL_KDD_GUI
{
    public partial class Form1 : Form
    {
        private Data dataSet;

        private TrainTestSplit<double[][]> xSubset = new TrainTestSplit<double[][]>();
        private TrainTestSplit<int[]> ySubset = new TrainTestSplit<int[]>();

        private NaiveBayes<NormalDistribution> naiveBayes;
        private GeneralConfusionMatrix naiveBayesMatrix;
        
        private MultinomialLogisticRegression logisticRegression;
        private GeneralConfusionMatrix logisticRegressionMatrix;

        private bool isDataLoaded, isModelsLearned;

        private const string nbLabelText = "Naive Bayes";
        private const string logRegLabelText = "Multinomial Logistic Regression";

        public Form1()
        {
            InitializeComponent();

            trainDataRadioButton.Checked = true;
        }

        #region Event handlers

        private void dataRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!isDataLoaded)
            {
                Utilities.InfoMessageBox("Please load data or wait until data is loaded first.");
                return;
            }
            if (!isModelsLearned)
            {
                Utilities.InfoMessageBox("Please press 'Learn models' button or wait until models are learned.");
                return;
            }

            TestModels();
        }

        private void featuresListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isDataLoaded)
            {
                Utilities.InfoMessageBox("Please load data or wait until data is loaded first.");
                return;
            }

            var selectedFeature = featuresListBox.SelectedItem as string;
            var featureIndex = dataSet.dataFull.Columns.IndexOf(selectedFeature);

            Scatterplot plot = new Scatterplot(selectedFeature);
            plot.Compute(dataSet.X_Test.GetColumn(featureIndex), dataSet.Y_Test.Select(x => (double)x).ToArray<double>());
            scatterplotView.Scatterplot = plot;
        }

        private async void prepareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isDataLoaded)
            {
                Utilities.InfoMessageBox("Data is already loaded.");
                return;
            }

            if (!Utilities.ConfirmMessageBox("Loading data can take up to 2 minutes. Continue?"))
                return;

            toolStripStatusLabel.Text = "Loading data...";

            dataSet = new Data();

            var loadTask = Task.Factory.StartNew(() =>
            {
                Utils.PrepareData(out dataSet.X_Train, out dataSet.X_Test, out dataSet.Y_Train, out dataSet.Y_Test, out dataSet.Y_Labels);
                Cache.LoadFromCache("df_full_encoded.txt", out dataSet.dataFull);
            });
            await loadTask;

            isDataLoaded = true;
            toolStripStatusLabel.Text = "Data loaded.";

            featuresListBox.Items.Clear();
            featuresListBox.Items.AddRange(dataSet.dataFull.GetHeaders());
        }

        private async void learnAllButton_Click(object sender, EventArgs e)
        {
            if (!isDataLoaded)
            {
                Utilities.InfoMessageBox("Please load data or wait until data is loaded first.");
                return;
            }

            if (!Utilities.ConfirmMessageBox("Learning models can take up to 2 minutes. Continue?"))
                return;

            Accord.Statistics.Tools.Center(dataSet.X_Train, inPlace: true);
            Accord.Statistics.Tools.Standardize(dataSet.X_Train, inPlace: true);
            Accord.Statistics.Tools.Center(dataSet.X_Test, inPlace: true);
            Accord.Statistics.Tools.Standardize(dataSet.X_Test, inPlace: true);

            double[][] XKnownTrainSet, XKnownTestSet;
            int[] YKnownTrainSet, YKnownTestSet;

            Utils.SplitTrainTest(dataSet.X_Train, dataSet.Y_Train, out XKnownTrainSet, out XKnownTestSet, out YKnownTrainSet, out YKnownTestSet);

            xSubset.Training = XKnownTestSet;
            xSubset.Testing = dataSet.X_Test;
            ySubset.Training = YKnownTestSet;
            ySubset.Testing = dataSet.Y_Test;

            toolStripStatusLabel.Text = "Learning models...";

            var learnTask = Task.Factory.StartNew(() =>
            {
                naiveBayes = LearnNB(XKnownTrainSet, YKnownTrainSet);
                logisticRegression = LearnLogReg(XKnownTrainSet, YKnownTrainSet);
            });
            await learnTask;

            isModelsLearned = true;
            toolStripStatusLabel.Text = "Learning done";

            TestModels();
        }
        #endregion

        private NaiveBayes<NormalDistribution> LearnNB(double[][] XKnownTrainSet, int[] YKnownTrainSet)
        {
            var NBLearning = new NaiveBayesLearning<NormalDistribution>();
            NBLearning.Options.InnerOption = new NormalOptions { Regularization = 1e-6, };

            return NBLearning.Learn(XKnownTrainSet, YKnownTrainSet);
        }
        private MultinomialLogisticRegression LearnLogReg(double[][] XKnownTrainSet, int[] YKnownTrainSet)
        {
            var LogRegLearning = new MultinomialLogisticLearning<BroydenFletcherGoldfarbShanno>();
            return LogRegLearning.Learn(XKnownTrainSet, YKnownTrainSet);
        }

        private GeneralConfusionMatrix Test(NaiveBayes<NormalDistribution> nb, double[][] x_test, int[] y_expected, out int[] y_predicted)
        {
            y_predicted = nb.Decide(x_test);
            var nb_conf = new GeneralConfusionMatrix(y_expected, y_predicted);

            return nb_conf;
        }
        private GeneralConfusionMatrix Test(MultinomialLogisticRegression logReg, double[][] x_test, int[] y_expected, out int[] y_predicted)
        {
            y_predicted = logReg.Decide(x_test);
            var logReg_conf = new GeneralConfusionMatrix(y_expected, y_predicted);

            return logReg_conf;
        }
        
        private void TestModels()
        {
            int[] nb_y_predicted;
            int[] logReg_y_predicted;

            //Test
            if (trainDataRadioButton.Checked)
            {
                logisticRegressionMatrix = Test(logisticRegression, xSubset.Training, ySubset.Training, out logReg_y_predicted);
                naiveBayesMatrix = Test(naiveBayes, xSubset.Training, ySubset.Training, out nb_y_predicted);
            }
            else
            {
                logisticRegressionMatrix = Test(logisticRegression, xSubset.Testing, ySubset.Testing, out logReg_y_predicted);
                naiveBayesMatrix = Test(naiveBayes, xSubset.Testing, ySubset.Testing, out nb_y_predicted);
            }

            //Show matrices
            var nbAccuracy = Math.Round(naiveBayesMatrix.Accuracy, 4);
            nbLabel.Text = $"{nbLabelText} (Accuracy: {nbAccuracy})";
            nbDataGridView.DataSource = Utilities.ReportToDataTable(ClassificationReport.GetReportFromConfusionMatrix(naiveBayesMatrix));

            var logRegAccuracy = Math.Round(logisticRegressionMatrix.Accuracy, 4);
            logRegLabel.Text = $"{logRegLabelText} (Accuracy: {logRegAccuracy})";
            logRegDataGridView.DataSource = Utilities.ReportToDataTable(ClassificationReport.GetReportFromConfusionMatrix(logisticRegressionMatrix));

            //Show plots
            if (trainDataRadioButton.Checked)
            {
                CreateResultScatterplot(nbZedGraphControl, xSubset.Training, ySubset.Training, nb_y_predicted);
                CreateResultScatterplot(logRegZedGraphControl, xSubset.Training, ySubset.Training, logReg_y_predicted);
            }
            else
            {
                CreateResultScatterplot(nbZedGraphControl, xSubset.Testing, ySubset.Testing, nb_y_predicted);
                CreateResultScatterplot(logRegZedGraphControl, xSubset.Testing, ySubset.Testing, logReg_y_predicted);
            }
        }

        private void CreateResultScatterplot(ZedGraphControl zgc, double[][] x_inputs, int[] y_expected, int[] y_predicted)
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.Fill = new Fill(Color.WhiteSmoke);
            myPane.CurveList.Clear();

            myPane.Title.Text = "Quality";
            myPane.XAxis.Title.Text = "Samples";
            myPane.YAxis.Title.Text = "Predicted - Expected";

            // Classification problem
            PointPairList listMiss = new PointPairList();
            PointPairList listHits = new PointPairList();
            for (int i = 0; i < y_predicted.Length; i++)
            {
                if (y_predicted[i] != y_expected[i])
                    listMiss.Add(i, 2);
                else
                    listHits.Add(i, -2);
            }

            LineItem myCurve1 = myPane.AddCurve("Miss", listMiss, Color.Red, SymbolType.Diamond);
            myCurve1.Line.IsVisible = false;
            myCurve1.Symbol.Border.IsVisible = false;
            myCurve1.Symbol.Fill = new Fill(Color.Red);

            LineItem myCurve2 = myPane.AddCurve("Hits", listHits, Color.Green, SymbolType.Diamond);
            myCurve2.Line.IsVisible = false;
            myCurve2.Symbol.Border.IsVisible = true;
            myCurve2.Symbol.Fill = new Fill(Color.Green);

            zgc.AxisChange();
            zgc.Invalidate();
        }
    }
}
