using EventsHub.DAL.Entities;
using EventsHub.DAL.Entities.Concert;
using EventsHub.DAL.Entities.Film;
using EventsHub.DAL.Entities.Theatre;
using EventsHub.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EventsHub.DAL.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private DbContext context;

        private FilmRepository filmRepository;
        private TheatreRepository theatreRepository;
        private ConcertRepository concertRepository;
        private UserRepository userRepository;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public FilmRepository FilmRepository
        {
            get
            {
                if (filmRepository == null)
                    filmRepository = new FilmRepository(context.Set<Film>());
                return filmRepository;
            }
        }

        public TheatreRepository TheatreRepository
        {
            get
            {
                if (theatreRepository == null)
                    theatreRepository = new TheatreRepository(context.Set<TheatrePlay>());
                return theatreRepository;
            }
        }

        public ConcertRepository ConcertRepository
        {
            get
            {
                if (concertRepository == null)
                    concertRepository = new ConcertRepository(context.Set<Concert>());
                return concertRepository;
            }
        }

        public UserRepository UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(context.Set<User>());
                return userRepository;
            }
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(context.Set<TEntity>());
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
