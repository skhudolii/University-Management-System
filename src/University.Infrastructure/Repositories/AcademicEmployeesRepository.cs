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
using University.Infrastructure.Repositories.Base;

namespace University.Infrastructure.Repositories
{
    public class AcademicEmployeesRepository : EntityBaseRepository<AcademicEmployee>, IAcademicEmployeesRepository
    {
        public AcademicEmployeesRepository(UniversityDbContext context) : base(context) { }
    }
}
