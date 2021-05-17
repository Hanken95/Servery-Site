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
    public class DetailsModel : PageModel
    {
        private readonly DatabaseLogic _databaseLogic;

        public DetailsModel(SurveySite.SurveyDBContext context)
        {
            _databaseLogic = new DatabaseLogic(context);
        }

        [BindProperty]
        public Question Question { get; set; }

        [BindProperty]
        public Survey Survey { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _databaseLogic.GetQuestion(id);
            await _databaseLogic.GetAllAnswers();
            await _databaseLogic.GetAllSurveys();
            Survey = Question.Survey;


            if (Question == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _databaseLogic.RemoveQuestionFromSurvey(Question.Id, Survey.Id);

            //Question = await _databaseLogic.GetQuestion(Question.Id);

            return RedirectToPage(new { id = Question.Id });
        }
    }
}
