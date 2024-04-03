namespace EscoService
{
    public interface IEscoRepo
    {
        public IQueryable<Esco> GetAll();
        public IQueryable<Esco> GetById(int id);
        public void Create(Esco esco);
        public void Update(Esco esco);
        public void Delete(Esco esco);
    }
}
