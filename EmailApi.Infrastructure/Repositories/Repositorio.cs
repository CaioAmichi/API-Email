using EmailApi.Application;
using EmailApi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using EmailApi.Domain.IRepository;
using EmailApi.Infrastructure.Contexto;

namespace EmailApi.Infrastructure.Repositories
{
    public abstract class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : Entidade, new()
    {
        protected readonly DBContexto Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repositorio(DBContexto db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public IEnumerable<TEntity> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AsNoTracking().Where(predicate).ToList();
        }

        public virtual TEntity ObterPrimeiroRegistro()
        {
            return DbSet.FirstOrDefault();
        }

        public virtual TEntity ObterPorId(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual List<TEntity> ObterTodos()
        {
            return DbSet.ToList();
        }

        public virtual void Adicionar(TEntity entidade)
        {
            if (ExisteRegistro(entidade))
            {
                throw new Exception(string.Format("Já existe um registro com este ID [{0}] no banco de dados", entidade.Id));
            }
            DbSet.Add(entidade);
            SaveChanges();
        }

        public virtual void AdicionarLista(List<TEntity> listaEntidade)
        {
            DbSet.AddRange(listaEntidade);
            SaveChanges();
        }

        public virtual void AtualizarLista(List<TEntity> listaEntidade)
        {
            DbSet.UpdateRange(listaEntidade);
            SaveChanges();
        }
        public virtual void Atualizar(TEntity entidade)
        {
            Atualizar(entidade, true);
        }


        public virtual void Atualizar(TEntity entidade, bool validarRegistro)
        {
            if (validarRegistro && !ExisteRegistro(entidade))
            {
                throw new Exception("Registro não encontrado no banco de dados.");
            }

            DbSet.Update(entidade);
            SaveChanges();
        }

        public virtual void Remover(Guid id)
        {
            if (!ExisteRegistro(id))
            {
                throw new Exception("Registro não encontrado no banco de dados.");
            }

            DbSet.Remove(new TEntity { Id = id });
            SaveChanges();
        }

        public virtual void RemoverTodos()
        {
            DbSet.RemoveRange(DbSet.AsNoTracking());
            SaveChanges();
        }

        public void RemoverLista(Expression<Func<TEntity, bool>> predicate)
        {
            var aux = DbSet.AsNoTracking().Where(predicate);
            DbSet.RemoveRange(aux);
            SaveChanges();
        }

        public virtual bool ExisteRegistro(Guid id)
        {
            return ExisteRegistro(new TEntity { Id = id });
        }

        public virtual bool ExisteRegistro(TEntity entidade)
        {
            return Buscar(c => c.Id == entidade.Id).Any();
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}