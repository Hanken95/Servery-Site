using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SurveySite;
using SurveySite.Models;

namespace SurveySite.Pages.AnswerPages
{
    public class CreateModel : PageModel
    {
        private readonly SurveySite.SurveyDBContext _context;

        public CreateModel(SurveySite.SurveyDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Answer Answer { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Answer.Add(Answer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
