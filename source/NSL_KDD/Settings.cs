using System;
using System.IO;

namespace NSL_KDD
{
    public static class Settings
    {
        public static string PathToData = Path.Combine(Environment.CurrentDirectory, @"..\..\..\data\nslkdd");

        public static string PathToDataCache = Path.Combine(Environment.CurrentDirectory, @"..\..\..\data\nslkdd\cache");
    }
}
