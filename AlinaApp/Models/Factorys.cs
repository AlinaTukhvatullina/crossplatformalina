using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AlinaApp.Models
{
    public class Factorys
    {
        public long Id { set; get; }
        public string country { set; get; }

        public List<Pipe> pipes { set; get; }
        public Factorys()
        {
            pipes = new List<Pipe>();
        }
    }
}
