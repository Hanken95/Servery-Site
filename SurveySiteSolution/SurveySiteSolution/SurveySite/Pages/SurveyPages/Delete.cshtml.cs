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
    public class DeleteModel : PageModel
    {
        private readonly SurveyDBContext _context;

        public DeleteModel(SurveyDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Survey Survey { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Survey = await _context.Survey.FirstOrDefaultAsync(m => m.Id == id);
            await _context.Question.ToListAsync();

            if (Survey == null)
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

            Survey = await _context.Survey.FindAsync(id);
            await _context.Question.ToListAsync();
            await _context.Answer.ToListAsync();


            if (Survey != null)
            {
                _context.Survey.Remove(Survey);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
