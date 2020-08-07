using System;
using System.IO;

namespace NSL_KDD
{
    /// <summary>
    /// Класс с настройками
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Путь до директории с исходными данными для моделей
        /// </summary>
        public static string PathToData = Path.Combine(Environment.CurrentDirectory, @"..\..\..\data\nslkdd");

        /// <summary>
        /// Путь до директории с кешированными данными, подготовленными для обучения моделей
        /// </summary>
        public static string PathToDataCache = Path.Combine(Environment.CurrentDirectory, @"..\..\..\data\nslkdd\cache");
    }
}
