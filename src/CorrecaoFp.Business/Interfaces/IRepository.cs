using CorrecaoFp.Business.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

namespace CorrecaoFp.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> ObterTodos();
        Task<TEntity> ObterPorId(int id);
        Task Adicionar(TEntity entity); 
        Task Atualizar(TEntity entity);
        Task Remover(int id);
        Task<int> SaveChanges();
    }
}
