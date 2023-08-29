﻿using MapMusic.Common;
using MapMusic.Entities;

namespace MapMusic.DataAccess
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly MapMusicContext Context;

        public BaseRepository(MapMusicContext context)
        {
            this.Context = context;
        }

        public IQueryable<TEntity> Get()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public TEntity Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entitty)
        {
            Context.Set<TEntity>().Update(entitty);

            return entitty;
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }
    }
}