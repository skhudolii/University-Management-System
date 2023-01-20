using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.Entities;
using University.Core.Repositories;
using University.Infrastructure.Data;

namespace University.Infrastructure.Repositories
{
    public class AcademicEmployeesRepository : IAcademicEmployeesRepository
    {
        private readonly UniversityDbContext _context;

        public AcademicEmployeesRepository(UniversityDbContext context)
        {
            _context = context;
        }

        public void Add(AcademicEmployee academicEmployee)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AcademicEmployee>> GetAll()
        {
            var result = await _context.AcademicEmployees.ToListAsync();
            return result;
        }

        public AcademicEmployee GetById(int id)
        {
            throw new NotImplementedException();
        }

        public AcademicEmployee Update(int id, AcademicEmployee academicEmployee)
        {
            throw new NotImplementedException();
        }
    }
}
