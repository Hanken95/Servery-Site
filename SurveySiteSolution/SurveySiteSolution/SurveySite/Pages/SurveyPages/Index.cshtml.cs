using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SurveySite.Models;

namespace SurveySite.Pages.SurveyPages
{
    public class IndexModel : PageModel
    {
        private readonly SurveyDBContext _context;

        public IndexModel(SurveyDBContext context)
        {
            _context = context;
        }

        public List<Survey> Surveys { get;set; }

        public List<Question> Questions { get; set; }


        public async Task OnGetAsync()
        {
            Surveys = await _context.Survey.ToListAsync();
            Questions = await _context.Question.ToListAsync();
        }

        public async Task OnPostSeedDataBase()
        {
            _context.SeedDatabase();
            Surveys = await _context.Survey.ToListAsync();
            Page();
        }
    }
}
