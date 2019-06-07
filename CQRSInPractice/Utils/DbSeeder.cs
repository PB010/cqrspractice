using Logic.Utils;
using Microsoft.EntityFrameworkCore;

namespace API.Utils
{
    public static class DbSeeder
    {
        public static void SeedDb(StudentsDbContext context)
        {
            context.Database.Migrate();
        }
    }
}
