using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SurveySite.Models;

namespace SurveySite.Pages.SurveyPages
{
    public class CreateModel : PageModel
    {
        private readonly SurveyDBContext _context;
        public CreateModel(SurveyDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Survey Survey { get; set; }

        [BindProperty]
        public int NumberOfQuestions { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Survey.SurveyName == null)
            {
                return NotFound(); 
            }
            _context.Survey.Add(Survey);
            await _context.SaveChangesAsync();

            return RedirectToPage(
                "/QuestionPages/CreateMultipleQuestions", 
                new { numberOfQuestionsLeft = NumberOfQuestions, surveyId = Survey.Id });
        }
    }
}
