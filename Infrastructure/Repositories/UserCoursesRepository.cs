using Infrastructure.Contexts;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserCoursesRepository(DataContext context) : Repo<UserCoursesEntity>(context)
    {
        private readonly DataContext _context = context;
    }
}
