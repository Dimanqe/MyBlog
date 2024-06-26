﻿using Microsoft.EntityFrameworkCore.Infrastructure;
using MyBlog.DAL.Repositories.Interfaces;

namespace MyBlog.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BlogDbContext _context;

    private Dictionary<Type, object> _repositories;

    public UnitOfWork(BlogDbContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
    }

    public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class
    {
        if (_repositories == null) _repositories = new Dictionary<Type, object>();

        if (hasCustomRepository)
        {
            var customRepo = _context.GetService<IRepository<TEntity>>();
            if (customRepo != null) return customRepo;
        }

        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<TEntity>(_context);

        return (IRepository<TEntity>)_repositories[type];
    }

    public int SaveChanges(bool ensureAutoHistory = false)
    {
        throw new NotImplementedException();
    }
}