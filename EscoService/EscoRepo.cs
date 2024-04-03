using Microsoft.EntityFrameworkCore;

namespace EscoService
{
    public class EscoRepo(ApiContext context) : IEscoRepo
    {
        public IQueryable<Esco> GetAll()
        {
            return context.Escos
                .Include(a => a.Customers)
                .AsQueryable();

        }

        public IQueryable<Esco> GetById(int id)
        {
            return context.Escos
                .Include(a => a.Customers)
                .AsQueryable()
                .Where(c => c.ID == id);
        }

        public void Create(Esco esco)
        {
            context.Escos
                .Add(esco);
            context.SaveChanges();
        }

        public void Update(Esco esco)
        {
            context.Escos
                .Update(esco);
            context.SaveChanges();
        }

        public void Delete(Esco esco)
        {
            context.Escos
                .Remove(esco);
            context.SaveChanges();
        }
    }
}
