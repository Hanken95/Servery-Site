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
    public class DetailsModel : PageModel
    {
        private readonly SurveySite.SurveyDBContext _context;

        public DetailsModel(SurveySite.SurveyDBContext context)
        {
            _context = context;
        }

        public Question Question { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Question.FirstOrDefaultAsync(m => m.Id == id);
            await _context.Answer.ToListAsync();
            await _context.Survey.ToListAsync();

            if (Question == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
