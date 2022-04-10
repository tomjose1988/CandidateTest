using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateTest
{
    public class Service
    {
        private IManager _manager;

        public Service(IManager manager)
        {
            this._manager = manager;
        }
        public void StartService()
        {
            Console.WriteLine("Starting export!");

            this._manager.StartProcessing();

            Console.WriteLine("Processing input files...Press enter to exit processing.");
            Console.ReadLine();
            this._manager.EndProcessing();
            Console.WriteLine("Stopped processing...");
            Console.ReadLine();
        }
    }
}
