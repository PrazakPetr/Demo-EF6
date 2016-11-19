using Demo6.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos6.DataModel
{
    class DbInitializer : DropCreateDatabaseAlways<NinjaContext>
    {
        protected override void Seed(NinjaContext context)
        {
            var clan = new Clan { ClanName = "Clan1" };
            context.Clans.Add(clan);

            clan.Ninjas.Add(new Ninja
            {
                Name = "Ninja1#1",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1984, 1, 1),
                EquipmentOwned = new List<NinjaEquipment>
                {
                    new NinjaEquipment()
                    {
                        Name = "Muscles",
                        Type = EquipmentType.Tool
                    }
                }
            });

            clan.Ninjas.Add(new Ninja
            {
                Name = "Ninja2#1",
                ServedInOniwaban = false,
                DateOfBirth = new DateTime(1984, 1, 2),
                EquipmentOwned = new List<NinjaEquipment>
                {
                    new NinjaEquipment()
                    {
                        Name = "Muscles",
                        Type = EquipmentType.Tool
                    },
                    new NinjaEquipment
                    {
                        Name = "Spunk",
                        Type = EquipmentType.Weapon
                    }
                }
            });

            clan = new Clan { ClanName = "Clan2" };
            context.Clans.Add(clan);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
