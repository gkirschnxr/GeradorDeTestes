namespace GeradorDeTestes.WebApp.Controllers
{
    public interface IUnitOfWork
    {
        public void Commit();
        public void Rollback();
    }
}