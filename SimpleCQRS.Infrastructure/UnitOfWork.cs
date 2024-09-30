using Microsoft.EntityFrameworkCore;
using SimpleCQRS.Domain.Interfaces;
using SimpleCQRS.Infrastructure.Data;

namespace SimpleCQRS.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SimpleCqrsContext _context;


        public UnitOfWork(SimpleCqrsContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();

        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

    }
}
