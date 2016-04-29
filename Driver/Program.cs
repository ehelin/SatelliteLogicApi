using Shared.interfaces;
using LoadData;
using System;

namespace Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ILoadData ld = new LoadDataImpl();
                ld.LoadSatelliteData();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
