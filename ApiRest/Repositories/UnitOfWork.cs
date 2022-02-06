using ApiRest.Models;
using ApiRest.Models.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace ApiRest.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private DatabaseContext dbContext = new DatabaseContext();
        private GenericRepository<Usuario> usuarioRepository;
        
        public GenericRepository<Usuario> UsuarioRepository
        {
            get
            {
                if (this.usuarioRepository == null)
                    this.usuarioRepository = new GenericRepository<Usuario>(dbContext);
                return this.usuarioRepository;
            }
        }

        public void Save()
        {
            try
            {
                dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex) { throw ex.InnerException; }
            catch (DbUpdateException e) { throw e.InnerException;  }

            catch (Exception ex) { throw ex.InnerException; }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}