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
    public class DetailsModel : PageModel
    {
        private readonly DatabaseLogic _databaseLogic;

        public DetailsModel(SurveyDBContext context)
        {
            _databaseLogic = new DatabaseLogic(context);
        }

        [BindProperty]
        public Answer Answer { get; set; }

        [BindProperty]
        public Question Question { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Answer = await _databaseLogic.GetAnswer(id);
            await _databaseLogic.GetAllQuestions();
            Question = Answer.Question;


            if (Answer == null)
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

            await _databaseLogic.RemoveAnswerFromQuestion(Question.Id, Answer.Id);

            Answer = await _databaseLogic.GetAnswer(Answer.Id);

            return Page();
        }
    }
}
