using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Fund
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Замените на свою строку подключения
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Your ConnectionString"));
        }
    }
}
