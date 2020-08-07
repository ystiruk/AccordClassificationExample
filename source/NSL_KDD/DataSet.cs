using System.Data;

namespace NSL_KDD
{
    /// <summary>
    /// Класс содержит загруженные из кеша данные для удобного доступа из формы
    /// </summary>
    public class Data
    {
        public DataTable dataFull; //полная таблица данных
        public string[] Y_Labels; //метки признаков
        public double[][] X_Train;
        public double[][] X_Test;
        public int[] Y_Train;
        public int[] Y_Test;
    }
}
