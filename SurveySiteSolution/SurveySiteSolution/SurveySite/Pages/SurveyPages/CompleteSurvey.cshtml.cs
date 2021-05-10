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
        private readonly DatabaseLogic _databaseLogic;

        public CompleteSurveyModel(SurveyDBContext context)
        {
            _databaseLogic = new DatabaseLogic(context);
        }

        [BindProperty]
        public Survey Survey { get; set; }

        [BindProperty]
        public Question Question { get; set; }

        public async Task<IActionResult> OnGetAsync(int? surveyId)
        {
            if (surveyId == null)
            {
                return NotFound();
            }

            Survey = await _databaseLogic.GetSurvey(surveyId);
            await _databaseLogic.GetAllQuestions();

            if (Survey == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _databaseLogic.RemoveQuestionFromSurvey(Question.Id, Survey.Id);

            return RedirectToPage(new { surveyId = Survey.Id });
        }
    }
}
