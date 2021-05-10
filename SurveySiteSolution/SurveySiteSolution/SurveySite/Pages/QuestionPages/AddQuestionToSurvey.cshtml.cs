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
    public class AddQuestionToSurveyModel : PageModel
    {
        private readonly DatabaseLogic _databaseLogic;

        public AddQuestionToSurveyModel(SurveyDBContext context)
        {
            _databaseLogic = new DatabaseLogic(context);
        }

        public List<Survey> Surveys { get; set; }

        [BindProperty]
        public int SurveyId { get; set; }

        [BindProperty]
        public Question Question { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _databaseLogic.GetQuestion(id);
            Surveys = await _databaseLogic.GetAllSurveys();

            if (Question == null)
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

            await _databaseLogic.AddQuestionToSurvey(Question.Id, SurveyId);
            return RedirectToPage("./Details", new { id = Question.Id });
        }
    }
}
