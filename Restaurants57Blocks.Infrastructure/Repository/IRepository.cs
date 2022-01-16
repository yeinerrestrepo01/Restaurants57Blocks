using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Restaurants57Blocks.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {

        /// Retorna la primera entidad encontrada bajo una condición especificada o null sino encontrara registros       
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);

        /// Retorna un objeto del tipo AsQueryable       
        IQueryable<TEntity> AsQueryable();

        /// Registra una entidad       
        void Insert(TEntity entity);


        /// Registra varias entidades       
        void Insert(IEnumerable<TEntity> entities);

        Task InsertAsync(TEntity entity);

        Task InsertAsync(IEnumerable<TEntity> entities);

        /// Actualiza una entidad      
        void Update(TEntity entity);


        /// Actualiza varias entidades      
        void Update(IEnumerable<TEntity> entities);


        /// Elimina una entidad       
        void Delete(TEntity entity);


        /// Elimina por Id   
        void Delete(object id);


        /// Elimina un conjuto de entidades       
        void Delete(IEnumerable<TEntity> entities);

    }
}
