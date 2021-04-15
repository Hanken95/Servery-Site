using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SurveySite;
using SurveySite.Models;

namespace SurveySite.Pages.AnswerPages
{
    public class DetailsModel : PageModel
    {
        private readonly SurveySite.SurveyDBContext _context;

        public DetailsModel(SurveySite.SurveyDBContext context)
        {
            _context = context;
        }

        public Answer Answer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Answer = await _context.Answer.FirstOrDefaultAsync(m => m.Id == id);
            await _context.Question.ToListAsync();

            if (Answer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
