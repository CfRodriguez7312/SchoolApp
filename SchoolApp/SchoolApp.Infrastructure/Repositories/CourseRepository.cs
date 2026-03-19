using SchoolApp.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolApp.Infrastructure.Repositories
{
    public class CourseRepository : ICourse
    {

        protected readonly SchoolDbContext _context;

        public CourseRepository(SchoolDbContext context)
        {
            _context = context;
        }

    }
}
