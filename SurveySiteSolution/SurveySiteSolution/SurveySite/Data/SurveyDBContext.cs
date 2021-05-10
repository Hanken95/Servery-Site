using Microsoft.EntityFrameworkCore;
using SurveySite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveySite
{
    public class SurveyDBContext : DbContext
    {
        public SurveyDBContext (DbContextOptions<SurveyDBContext> options)
            : base(options)
        {
        }

        public DbSet<Survey> Survey { get; set; }

        public DbSet<Question> Question { get; set; }

        public DbSet<Answer> Answer { get; set; }

        
    }
}
