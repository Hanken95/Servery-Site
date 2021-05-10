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
        private readonly DatabaseLogic _databaseLogic;

        public CreateModel(SurveyDBContext context)
        {
            _databaseLogic = new DatabaseLogic(context);
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

            await _databaseLogic.CreateSurvey(Survey);

            if (NumberOfQuestions > 0)
            {
                return RedirectToPage(
                    "/QuestionPages/CreateMultipleQuestions", 
                    new { numberOfQuestionsLeft = NumberOfQuestions, surveyId = Survey.Id });
            }
            return RedirectToPage(
                    "./Details",
                    new { id = Survey.Id });
        }
    }
}
