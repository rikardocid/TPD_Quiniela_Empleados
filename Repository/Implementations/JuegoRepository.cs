using ASPNetCore6Identity.Data;
using Microsoft.EntityFrameworkCore;
using Quinela_TPD.Models;
using Quinela_TPD.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Quinela_TPD.Repository.Implementations
{
    public class JuegoRepository : GenericRepository<JuegoModel>, IJuegoRepository
    {
        private readonly ApplicationDbContext context;
        public JuegoRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }
        public List<JuegoModel> GetAllIncludes()
        {
            return this.context.Set<JuegoModel>()
                .Include(i => i.EquipoModel1)
                .Include(iu => iu.EquipoModel2)
                .Include(ie => ie.EstadioModel)
                .Include(im => im.MarcardorModel)
                .ToList();
        }
    }
}