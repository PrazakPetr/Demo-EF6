using Demos6.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new TestRepository();

            // repository.CreateFirstClan();
            //repository.CreateNinja();

            //   repository.CreateMultipleNinjas();

            //   repository.SimpleNinjaQuery();

            // repository.QueryAndUpdateNinja();

            // repository.QueryAndUpdateNinjaDisconnected();

            //repository.FindNinja();

            // repository.ExecuteQuery();

            // repository.DeleteNinja();

            // repository.DeleteNinjaDisconnected();

            // repository.InsertNinjaWithEquipment();

            // repository.SimpleNinjaGraphQuery();

             repository.ExplicitNinjaGraphQuery();

            // repository.LazyNinjaGraphQuery();

            //  repository.ProjectionQuery();

            // repository.LocalQuery();

          //  repository.ExploreMetadata();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Done");
            Console.ReadKey();
            Console.WriteLine("Click");
        }
    }
}
