using NinjaDomain.Classes;
using NinjaDomain.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());

            //InsertNinja();
            //InsertMultipleNinjas();
            SimpleNinjaQueries();

            Console.ReadKey();
        }

        private static void InsertNinja()
        {
            var ninja = new Ninja {
                Name = "HuySonSan",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1987, 11, 20),
                ClanId = 1
            };

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.Add(ninja);
                context.SaveChanges();
            }
        }

        private static void InsertMultipleNinjas()
        {
            var ninja1 = new Ninja
            {
                Name = "Leonardo",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1984, 1, 1),
                ClanId = 1
            };

            var ninja2 = new Ninja
            {
                Name = "Raphael",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1985, 1, 1),
                ClanId = 1
            };

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                context.Ninjas.AddRange(new List<Ninja> { ninja1, ninja2 });
                context.SaveChanges();
            }

        }

        private static void SimpleNinjaQueries()
        {
            using (var context = new NinjaContext())
            {
                var ninjas = context.Ninjas.ToList();   // Create and Execute a query
                

                // Execute the query by enumerate it
                foreach (var item in context.Ninjas)   // Open a connection
                {
                    Console.WriteLine(item);
                }
                // Close the connection


                var ninja = context.Ninjas
                                        .Where(e => e.DateOfBirth >= new DateTime(1987, 1, 1))
                                        .OrderBy(e => e.Name)
                                        .Take(1)
                                        .FirstOrDefault();  // Execute the query here
            }
        }
    }
}
