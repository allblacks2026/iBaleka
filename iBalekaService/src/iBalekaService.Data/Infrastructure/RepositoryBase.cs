using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iBalekaService.Data.Configurations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace iBalekaService.Data.Infastructure
{
    public abstract class RepositoryBase<T> where T:class
    {
        #region Properties
        private IBalekaContext dbContext;
        private readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory
        { get; private set; }

        protected IBalekaContext DbContext
        {
            get { return dbContext ?? (dbContext = DbFactory.Init()); }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }
        #region Implementation
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }
        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }
        #endregion
    }
}
