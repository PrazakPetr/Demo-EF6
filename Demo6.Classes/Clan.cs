using System;
using System.Collections.Generic;

namespace Demo6.Classes
{
    public class Clan : IModificationHistory
    {
        public Clan()
        {
            this.Ninjas = new List<Ninja>();
        }

        public int Id { get; set; }
        public string ClanName { get; set; }
        public List<Ninja> Ninjas { get; set; }

        public DateTime DateModified { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsDirty { get; set; }
    }
}