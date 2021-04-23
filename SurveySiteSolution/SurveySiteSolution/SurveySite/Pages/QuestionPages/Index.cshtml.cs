using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SurveySite;
using SurveySite.Models;

namespace SurveySite.Pages.QuestionPages
{
    public class IndexModel : PageModel
    {
        private readonly SurveySite.SurveyDBContext _context;

        public IndexModel(SurveySite.SurveyDBContext context)
        {
            _context = context;
        }

        public IList<Question> Question { get;set; }

        public async Task OnGetAsync()
        {
            Question = await _context.Question.ToListAsync();
            await _context.Answer.ToListAsync();
        }
    }
}
