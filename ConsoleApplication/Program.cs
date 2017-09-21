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
            //SimpleNinjaQueries();
            //QueryAndUpdateNinja();
            //RetrieveDataWithFind();
            //RetrieveDataWithStoredProc();
            DeleteNinja();


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

        private static void QueryAndUpdateNinja()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.FirstOrDefault();
                context.SaveChanges();  // No update queries will be made if there is no changes

                ninja.ServedInOniwaban = !ninja.ServedInOniwaban;
                context.SaveChanges();  // Will make a update query, because there is changes
            }
        }

        private static void QueryAndUpdateNinjaDisconnected()
        {
            Ninja ninja;

            // Context A
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninja = context.Ninjas.FirstOrDefault();
            }

            // Change made out of contexts
            ninja.ServedInOniwaban = !ninja.ServedInOniwaban;

            // Context B
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                context.SaveChanges();                      // Don't know about the change in ninja

                // Option 1
                context.Ninjas.Add(ninja);
                context.SaveChanges();

                // Option 2
                context.Ninjas.Attach(ninja);
                context.Entry(ninja).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private static void RetrieveDataWithFind()
        {
            var keyValue = 4;

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninja = context.Ninjas.Find(keyValue);          // After, the context have Ninjas are in memory
                Console.WriteLine("After Find#1: " + ninja.Name);

                var sameNinja = context.Ninjas.Find(keyValue);      // We don't query the database, here
                Console.WriteLine("After Find#2: " + ninja.Name);
            }
        }

        private static void RetrieveDataWithStoredProc()
        {
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;

                var ninjas = context.Ninjas.SqlQuery("exec GetOldNinjas");
                foreach (var ninja in ninjas)
                {
                    Console.WriteLine(ninja.Name);
                }
            }
        }

        private static void DeleteNinja()
        {
            // Scenario 1 - Simple delete operation
            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                var ninja = context.Ninjas.FirstOrDefault();
                context.Ninjas.Remove(ninja);
            }


            // Scenario 2 - Disconnected
            Ninja ninjaa;

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                ninjaa = context.Ninjas.FirstOrDefault();
            }

            using (var context = new NinjaContext())
            {
                context.Database.Log = Console.WriteLine;
                //context.Ninjas.Remove(ninja);                         // Will crash, the context don't know about the other ninja context

                //context.Ninjas.Attach(ninja);                         // Solution 1
                //context.Ninjas.Remove(ninja);

                context.Entry(ninjaa).State = EntityState.Deleted;      // Solution 2
                context.SaveChanges();
            }
        }
    }
}
