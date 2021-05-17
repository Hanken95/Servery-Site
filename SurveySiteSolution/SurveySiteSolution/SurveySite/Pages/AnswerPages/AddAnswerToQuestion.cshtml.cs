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

namespace SurveySite.Pages.AnswerPages
{
    public class AddAnswerToQuestionModel : PageModel
    {
        private readonly DatabaseLogic _databaseLogic;

        public AddAnswerToQuestionModel(SurveyDBContext context)
        {
            _databaseLogic = new DatabaseLogic(context);
        }

        public List<Question> Questions { get; set; }

        [BindProperty]
        public int QuestionId { get; set; }

        [BindProperty]
        public Answer Answer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Answer = await _databaseLogic.GetAnswer(id);
            Questions = await _databaseLogic.GetAllQuestions();

            if (Answer == null)
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

            await _databaseLogic.AddAnswerToQuestion(Answer.Id, QuestionId);

            return RedirectToPage("./Details", new { id = Answer.Id });
        }
    }
}
