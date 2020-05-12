using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace AlinaApp.Models
{
    public class Pipe
    {
        public long Id { set; get; }
        public double diam { set; get; }
        public double dlina { set; get; }
        public bool defective { get; set; }

        public long FactoryId { set; get; }
        
        public  Factorys factorys { set; get; }
    }
}
