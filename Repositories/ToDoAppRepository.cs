namespace ToDoAppMinimalAPI.Repositories
{
    public class ToDoAppRepository : BaseRepository<Entities.Task>
    {
        #region Constructor
        
        public ToDoAppRepository(ToDoAppDbContext dbContext) : base(dbContext)
        {
        }
        
        #endregion
    }
}
