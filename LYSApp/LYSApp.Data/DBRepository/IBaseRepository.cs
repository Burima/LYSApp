using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace LYSApp.Data.DBRepository
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// This method uses lambda expressions to allow the calling code to specify a filter condition and a column to order the results by, and a string parameter lets the caller provide a comma-delimited list of navigation properties for eager loading
        /// </summary>
        /// <param name="filter">Provide a lambda expression based on the T type, and this expression will return a bool value. Ex: student => student.LastName == "Smith"</param>
        /// <param name="orderBy">Provide a lambda expression, but the input to the expression is an IQueryable object for the T type. The expression will return an ordered version of that IQueryable object. Ex: q => q.OrderBy(s => s.LastName)</param>
        /// <param name="includeProperties">It applies the eager-loading expressions after parsing the comma-delimited list</param>
        /// <returns></returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");

        /// <summary>
        /// Gets all the entities
        /// </summary>
        /// <returns>All entities</returns>
        IEnumerable<T> Get();

        /// <summary>
        /// Get all entities matching the predication
        /// </summary>
        /// <param name="predicate">The filter clause</param>
        /// <returns>All entities matching the predicate</returns>
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get an entity with the matching ID
        /// </summary>
        /// <param name="id">Entity ID object</param>
        /// <returns>An entity matching with the ID</returns>
        T Get(object id);

        /// <summary>
        /// Set based on where condition
        /// </summary>
        /// <param name="predicate">The predicate</param>
        /// <returns>The records matching the given condition</returns>
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Allows LINQ syntax
        /// </summary>
        /// <returns>An IQueryable object.</returns>
        IQueryable<T> Query();

        /// <summary>
        /// Determines if there are any entities matching the predicate
        /// </summary>
        /// <param name="predicate">The filter clause</param>
        /// <returns>True if a match was found</returns>
        bool Any(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns the first entity that matches the predicate
        /// </summary>
        /// <param name="predicate">The filter clause</param>
        /// <returns>An entity matching the predicate</returns>
        T First(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Returns the first entity that matches the predicate else null
        /// </summary>
        /// <param name="predicate">The filter clause</param>
        /// <returns>An entity matching the predicate else null</returns>
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Insert a given entity to the context
        /// </summary>
        /// <param name="entity">The entity to add to the context</param>
        void Insert(T entity);

        /// <summary>
        /// Update a given entity to the context
        /// </summary>
        /// <param name="entity">The entity to update to the context</param>
        void Update(T entity);

        /// <summary>
        /// Delete an entity from the context by given its ID
        /// </summary>
        /// <param name="id">An entity matching with the ID</param>
        void Delete(object id);

        /// <summary>
        /// Deletes a given entity from the context
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        void Delete(T entity);
    }
}
