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

        public async Task AddAsync(AcademicEmployee academicEmployee)
        {
            await _context.AcademicEmployees.AddAsync(academicEmployee);
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AcademicEmployee>> GetAllAsync()
        {
            var result = await _context.AcademicEmployees.ToListAsync();
            return result;
        }

        public async Task<AcademicEmployee> GetByIdAsync(int id)
        {
            var result = await _context.AcademicEmployees.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task<AcademicEmployee> UpdateAsync(int id, AcademicEmployee newAcademicEmployee)
        {
            _context.Update(newAcademicEmployee);
            await _context.SaveChangesAsync();
            return newAcademicEmployee;
        }
    }
}
