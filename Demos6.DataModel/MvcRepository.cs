using Demo6.Classes;
using Demos6.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Collections;

namespace WebApp.Models
{
    public class MvcRepository
    {
        private string name;

        private NinjaContext Context
        {
            get
            {
                var db = new NinjaContext(name);

                db.Database.Log = x => System.Diagnostics.Debug.WriteLine(x);

                return db;
            }
        }

        static MvcRepository()
        {
            Database.SetInitializer<NinjaContext>(new DbInitializer());
        }

        public MvcRepository(string name)
        {
            this.name = name;
        }

        public IEnumerable<Ninja> GetNinjasWithClan()
        {
            using (var db = this.Context)
            {
                return db.Ninjas.AsNoTracking().Include(x => x.Clan).ToList();
            }
        }

        public Ninja GetNinjaWithEquipment(int id)
        {
            using (var db = this.Context)
            {
                return db.Ninjas.AsNoTracking().Include(x => x.EquipmentOwned).SingleOrDefault(x => x.Id == id);
            }
        }

        public Ninja GetNinjaWithEquipmentAndClan(int id)
        {
            using (var db = this.Context)
            {
                return db.Ninjas.AsNoTracking().Include(x => x.EquipmentOwned).Include(x => x.Clan).SingleOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<Clan> GetClans()
        {
            using (var db = this.Context)
            {
                return db.Clans.AsNoTracking().ToList();
            }
        }

        public void EditNinja(Ninja ninja)
        {
            using (var db = this.Context)
            {
                db.Entry(ninja).State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public IEnumerable<NinjaEquipment> GetNinjaEquipmentList(int ninjaId)
        {
            using (var db = this.Context)
            {
                return db.Equipment.Where(x => x.NinjaId == ninjaId).ToList();
            }
        }
    }
}