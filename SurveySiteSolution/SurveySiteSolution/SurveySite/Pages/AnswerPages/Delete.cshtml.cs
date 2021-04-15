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
    public class DeleteModel : PageModel
    {
        private readonly SurveySite.SurveyDBContext _context;

        public DeleteModel(SurveySite.SurveyDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Answer Answer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Answer = await _context.Answer.FirstOrDefaultAsync(m => m.Id == id);

            if (Answer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Answer = await _context.Answer.FindAsync(id);

            if (Answer != null)
            {
                _context.Answer.Remove(Answer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
