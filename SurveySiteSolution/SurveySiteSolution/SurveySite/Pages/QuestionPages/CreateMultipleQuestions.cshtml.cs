using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurveySite;
using SurveySite.Models;

namespace SurveySite.Pages.QuestionPages
{
    public class CreateMultipleQuestionsModel : PageModel
    {
        private readonly SurveySite.SurveyDBContext _context;

        public CreateMultipleQuestionsModel(SurveySite.SurveyDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Survey Survey { get; set; }

        [BindProperty]
        public Question Question { get; set; }

        [BindProperty]
        public int? NumberOfQuestionsLeft { get; set; }

        public async Task<IActionResult> OnGetAsync(int? numberOfQuestions,int? surveyId)
        {
            if (surveyId == null)
            {
                return NotFound();
            }

            Survey = await _context.Survey.FirstOrDefaultAsync(m => m.Id == surveyId);

            NumberOfQuestionsLeft = numberOfQuestions;

            if (Survey == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Survey.Questions.Add(Question);
            _context.Attach(Survey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyExists(Survey.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            NumberOfQuestionsLeft -= 1;
            if (NumberOfQuestionsLeft > 0)
            {
                return RedirectToPage(new { numberOfQuestions = NumberOfQuestionsLeft, surveyId = Survey.Id });
            }
            return RedirectToPage("Index");
        }

        private bool SurveyExists(int id)
        {
            return _context.Survey.Any(e => e.Id == id);
        }
    }
}
