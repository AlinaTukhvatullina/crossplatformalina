using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AlinaApp.Models
{
    public class AlinaAppContext : DbContext
    {
        public AlinaAppContext(DbContextOptions<AlinaAppContext> options)
            : base(options)
        {
        }

        public DbSet<Factorys> Factoryses { get; set; }
        public DbSet<Pipe> Pipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //настраиваем связь один ко многим
            modelBuilder.Entity<Pipe>()
            .HasOne(p => p.factorys)
            .WithMany(b => b.pipes)
            .HasForeignKey(p => p.FactoryId);
        }

       
        public string PostPipe(Pipe p)
        {
            if (p.Id != 0)
            {
                return "You cannot assign Id yourself";
            }

            foreach(var f in Factoryses)
            {
                if (f.Id == p.FactoryId)
                {
                    f.pipes.Add(p);
                    SaveChanges();
                    return "Okey";
                }

            }
            return "No factory";
        }

        public IEnumerable<Factorys> GetFactorys()
        {
            //Pipes.Include(p => p.factorys).ToList() ;

            //Factoryses.Include(c => c.pipes).ThenInclude(sc => sc.factorys).ToList();

            return Factoryses.Include(c => c.pipes).ThenInclude(p => p.factorys).ToList() ;
        }

        public IEnumerable<Pipe> GetPipes()
        {
            Factoryses.Include(c => c.pipes).ThenInclude(p => p.factorys).ToList();
            return
                from p in Pipes
                where p.defective == false
                select p;
        }

        public IEnumerable<Pipe> GetDefectivePipes()
        {
            Factoryses.Include(c => c.pipes).ThenInclude(p => p.factorys).ToList();
            return
                from p in Pipes
                where p.defective == true
                select p;
        }
    }
}
