namespace ToDoAppMinimalAPI.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        #region Constructor

        protected readonly ToDoAppDbContext _dbContext;

        protected BaseRepository(ToDoAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Methods

        public virtual TEntity Get(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public virtual List<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public virtual void Create(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
        }

        public virtual void Update(int Id,TEntity entity)
        {
            var existingEntity = _dbContext.Set<TEntity>().Find(Id);
           
            if (existingEntity is not null)
            {
                _dbContext.Set<TEntity>().Update(entity);
                _dbContext.SaveChanges();
            }
        }

        public virtual void Delete(int Id)
        {
            var existingEntity = _dbContext.Set<TEntity>().Find(Id);
            
            if (existingEntity is not null)
            {
                _dbContext.Set<TEntity>().Remove(existingEntity);
                _dbContext.SaveChanges();
            }

        }

        #endregion
    }
}
