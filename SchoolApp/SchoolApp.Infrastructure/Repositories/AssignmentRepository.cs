using SchoolApp.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolApp.Infrastructure.Repositories
{
    public class AssignmentRepository : IAssignment
    {

        protected readonly SchoolDbContext _context;

        public AssignmentRepository(SchoolDbContext context)
        {
            _context = context;
        }

    }
}
