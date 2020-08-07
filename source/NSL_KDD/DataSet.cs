using System.Data;

namespace NSL_KDD
{
    public class Data
    {
        public DataTable dataFull;

        public string[] Y_Labels;
        
        public double[][] X_Train;
        public double[][] X_Test;
        
        public int[] Y_Train;
        public int[] Y_Test;
    }
}
