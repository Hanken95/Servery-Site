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
    public class CompleteSurveyModel : PageModel
    {
        private readonly SurveySite.SurveyDBContext _context;

        public CompleteSurveyModel(SurveySite.SurveyDBContext context)
        {
            _context = context;
        }

        public Survey Survey { get; set; }

        public async Task<IActionResult> OnGetAsync(int? surveyId)
        {
            if (surveyId == null)
            {
                return NotFound();
            }

            Survey = await _context.Survey.FirstOrDefaultAsync(m => m.Id == surveyId);
            await _context.Question.ToListAsync();
            await _context.Answer.ToListAsync();

            if (Survey == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
