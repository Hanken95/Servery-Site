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
    public class CreateModel : PageModel
    {
        private readonly SurveySite.SurveyDBContext _context;

        public CreateModel(SurveySite.SurveyDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Surveys = await _context.Survey.ToListAsync();
            return Page();
        }

        public List<Survey> Surveys { get; set; }

        
        [BindProperty]
        public Question Question { get; set; }
        
        [BindProperty]
        public int NumberOfAnswers { get; set; }

        [BindProperty]
        public int? SurveyId { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (SurveyId != null)
            {
                await _context.Question.ToListAsync();
                var survey = await _context.Survey.FirstOrDefaultAsync(s => s.Id == SurveyId);
                survey.Questions.Add(Question);
            }
            else
            {
                await _context.Question.AddAsync(Question);
            }

            await _context.SaveChangesAsync();

            if (Question.QuestionType != QuestionType.Text)
            {
            return RedirectToPage("/AnswerPages/CreateMultipleAnswers",
                    new
                    {
                        numberOfAnswers = NumberOfAnswers,
                        questionId = Question.Id
                    });
            }
            return RedirectToPage(
                    "/QuestionPages/Details",
                    new
                    {
                        id = Question.Id
                    });

        }
    }
}
