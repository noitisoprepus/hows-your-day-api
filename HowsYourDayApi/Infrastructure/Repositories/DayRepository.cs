using HowsYourDayApi.Domain.Interfaces;
using HowsYourDayApi.Infrastructure.Persistence;

namespace HowsYourDayApi.Infrastructure.Repositories
{
    public class DayRepository : IDayRepository
    {
        private readonly HowsYourDayAppDbContext _context;

        public DayRepository(HowsYourDayAppDbContext context)
        {
            _context = context;
        }
    }
}
