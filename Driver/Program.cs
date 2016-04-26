using Shared.interfaces;
using LoadData;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoadData ld = new LoadDataImpl();
            ld.LoadSatelliteData();
        }
    }
}
