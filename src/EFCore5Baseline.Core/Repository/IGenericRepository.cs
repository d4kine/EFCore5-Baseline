using System.Text;
using System.Linq;
using System.Collections.Generic;
using System;
using EFCore5Baseline.Common.Models;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EFCore5Baseline.Core.Repositories
{
    public interface IGenericRepository
    {
        IEnumerable<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool readOnly = false) where TEntity : class, IEntity;

        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool readOnly = false) where TEntity : class, IEntity;

        IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool readOnly = false) where TEntity : class, IEntity;

        Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool readOnly = false) where TEntity : class, IEntity;

        TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null,
            bool readOnly = false) where TEntity : class, IEntity;

        Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null,
            bool readOnly = false) where TEntity : class, IEntity;

        bool TryGetOne<TEntity>(
            out TEntity entity,
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null,
            bool readOnly = false) where TEntity : class, IEntity;

        TEntity GetById<TEntity>(object id) where TEntity : class, IEntity;

        ValueTask<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : class, IEntity;

        int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, IEntity;

        Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, IEntity;

        bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, IEntity;

        Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class, IEntity;

        void Create<TEntity>(TEntity entity) where TEntity : class, IEntity;

        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;

        void Delete<TEntity>(object id) where TEntity : class, IEntity;

        void Delete<TEntity>(TEntity entity) where TEntity : class, IEntity;

        void DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity;

        void Save();

        Task SaveAsync();
    }
}