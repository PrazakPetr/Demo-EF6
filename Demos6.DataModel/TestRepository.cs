using Demo6.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos6.DataModel
{
    public class TestRepository
    {
        public TestRepository()
        {
            Database.SetInitializer(new NullDatabaseInitializer<NinjaContext>());
        }

        public void CreateNinja()
        {
            var ninja = new Ninja
            {
                Name = "SamsonSan",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(2008, 1, 28),
                ClanId = 1
            };

            var context = new NinjaContext();

            context.Database.Log = Console.WriteLine;
            context.Ninjas.Add(ninja);

            context.SaveChanges();
        }

        public void InsertNinjaWithEquipment()
        {
            var ninja = new Ninja
            {
                Name = "EquipeNinja",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1974, 1, 28),
                ClanId = 1
            };

            using (var context = new NinjaContext())
            {
                ninja.EquipmentOwned.Add(new NinjaEquipment
                {
                    Name = "Muscles",
                    Type = EquipmentType.Tool
                });

                ninja.EquipmentOwned.Add(new NinjaEquipment
                {
                    Name = "Spunk",
                    Type = EquipmentType.Weapon
                });

                context.Ninjas.Add(ninja);

                var test = context.Ninjas.Local.ToList();

                context.SaveChanges();

                test = context.Ninjas.Local.ToList();
            }
        }

        public void LocalQuery()
        {
            using (var context = new NinjaContext())
            {
                var ninjas = context.Ninjas.Local.ToList();

                Console.WriteLine(ninjas.Count);

                context.Ninjas.Where(n => n.Name.StartsWith("E")).ToList();

                ninjas = context.Ninjas.Local.ToList();

                Console.WriteLine(ninjas.Count);

                context.Ninjas.Add(new Ninja
                {
                    Name = "NewNinja",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1974, 12, 28),
                    ClanId = 1
                });

                ninjas = context.Ninjas.Local.ToList();

                Console.WriteLine(ninjas.Count);

                context.Ninjas.FirstOrDefault();

                ninjas = context.Ninjas.Local.ToList();

                Console.WriteLine(ninjas.Count);

            }
        }

        public void ProjectionQuery()
        {
            using (var context = new NinjaContext())
            {
                var ninja = context.Ninjas.Select(x => new
                {
                    x.Name,
                    x.DateOfBirth,
                    x.EquipmentOwned
                }).ToList();
            }
        }

        public void SimpleNinjaGraphQuery()
        {
            using (var context = new NinjaContext())
            {
                var ninja = context.Ninjas.Include(x => x.EquipmentOwned).FirstOrDefault(n => n.Name.StartsWith("Equipe"));
            }
        }

        public void ExplicitNinjaGraphQuery()
        {
            using (var context = new NinjaContext())
            {
                var ninja = context.Ninjas.FirstOrDefault(n => n.Name.StartsWith("Equipe"));

                Console.WriteLine("#1 ninaja {0}; {1}", ninja.Name, ninja.EquipmentOwned.Count);

                context.Entry(ninja).Reference(x => x.Clan).Load();

                context.Entry(ninja).Collection(x => x.EquipmentOwned).Load();

                Console.WriteLine("#2 ninaja {0}; {1}", ninja.Name, ninja.EquipmentOwned.Count);
            }
        }

        public void LazyNinjaGraphQuery()
        {
            using (var context = new NinjaContext())
            {
                Ninja ninja = context.Ninjas.FirstOrDefault(n => n.Name.StartsWith("Equipe"));

                Console.WriteLine(ninja.GetType().AssemblyQualifiedName);

                Console.WriteLine("#1 ninaja {0}; {1}", ninja.Name, ninja.EquipmentOwned.Count);



                // Console.WriteLine("#2 ninaja {0}; {1}", ninja.Name, ninja.EquipmentOwned.Count);
            }
        }

        public void DeleteNinja()
        {
            int key = 0;

            using (var context = new NinjaContext())
            {
                var testNinja = new Ninja
                {
                    Name = "TestNinja",
                    ServedInOniwaban = false,
                    DateOfBirth = new DateTime(1974, 1, 28),
                    ClanId = 1
                };

                context.Ninjas.Add(testNinja);
                context.SaveChanges();

                key = testNinja.Id;
            }

            using (var context = new NinjaContext())
            {
                var removeNinja = context.Ninjas.FirstOrDefault(x => x.Id == key);
                context.Ninjas.Remove(removeNinja);
                context.SaveChanges();
            }
        }

        public void DeleteNinjaDisconnected()
        {
            var testNinja = new Ninja
            {
                Name = "TestNinja",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1974, 1, 28),
                ClanId = 1
            };

            using (var context = new NinjaContext())
            {
                context.Ninjas.Add(testNinja);
                context.SaveChanges();
            }

            using (var context = new NinjaContext())
            {
                // context.Ninjas.Attach(testNinja);
                // context.Ninjas.Remove(testNinja);
                context.Entry(testNinja).State = EntityState.Deleted;

                //var remove = context.Ninjas.Find(testNinja.Id);

                //context.Ninjas.Remove(remove);

                context.SaveChanges();
            }


        }

        public void ExecuteQuery()
        {
            using (var context = new NinjaContext())
            {
                var ninjas = context.Ninjas.SqlQuery("exec GetOldNinjas");

                Console.WriteLine("here");

                foreach (var item in ninjas)
                {
                    Console.WriteLine(item.Name);
                }
            }
        }

        public void FindNinja()
        {
            using (var context = new NinjaContext())
            {
                var key = 5;

                var ninja = context.Ninjas.Find(key);
                Console.WriteLine("First find {0}", ninja.Name);
                var otherNinja = context.Ninjas.Find(key);
                Console.WriteLine("Second find {0}", otherNinja.Name);
            }
        }

        public void QueryAndUpdateNinja()
        {
            using (var context = new NinjaContext())
            {
                var ninja = context.Ninjas.FirstOrDefault();
                ninja.ServedInOniwaban = !ninja.ServedInOniwaban;
                context.SaveChanges();
            }
        }

        public void QueryAndUpdateNinjaDisconnected()
        {
            Ninja ninja = null;

            using (var context = new NinjaContext())
            {
                ninja = context.Ninjas.FirstOrDefault();

            }

            ninja.ServedInOniwaban = !ninja.ServedInOniwaban;

            using (var context = new NinjaContext())
            {
                //context.Ninjas.Attach(ninja);
                context.Entry(ninja).State = EntityState.Modified;
                context.SaveChanges();
            }

        }

        public void SimpleNinjaQuery()
        {
            using (var context = new NinjaContext())
            {
                var name = "Raphael";

                var temp = context.Ninjas.AsEnumerable();

                var ninjas = context.Ninjas.Where(n => n.DateOfBirth >= new DateTime(1984, 1, 1)).OrderBy(n => n.Name).Skip(1).Take(1).ToList();//.FirstOrDefault();//.ToList();

               var nin = temp.Where(n => n.ClanId == 1).ToList();


            }
        }

        public void CreateMultipleNinjas()
        {
            var firstNinja = new Ninja
            {
                Name = "Leonardo",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1984, 1, 28),
                ClanId = 1
            };

            var secondNinja = new Ninja
            {
                Name = "Raphael",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1985, 1, 28),
                ClanId = 1
            };

            var context = new NinjaContext();

            context.Database.Log = Console.WriteLine;
            context.Ninjas.AddRange(new List<Ninja> { firstNinja, secondNinja });

            context.SaveChanges();
        }

        public void CreateFirstClan()
        {
            var clan = new Clan
            {
                ClanName = "Vermont Ninjas",
            };

            using (var context = new NinjaContext())
            {
                context.Clans.Add(clan);

                context.SaveChanges();
            }

        }

        public void ExploreMetadata()
        {
            using (var context = new NinjaContext())
            {
                var o = (context as IObjectContextAdapter).ObjectContext;

                foreach(var entity in o.MetadataWorkspace.GetItems<System.Data.Entity.Core.Metadata.Edm.EntityType>(System.Data.Entity.Core.Metadata.Edm.DataSpace.CSSpace))

                {
                    Console.WriteLine(entity.FullName);

                }
            }
        }
    }
}
