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
    public class CreateModel : PageModel
    {
        private readonly DatabaseLogic _databaseLogic;

        public CreateModel(SurveyDBContext context)
        {
            _databaseLogic = new DatabaseLogic(context);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Questions = await _databaseLogic.GetAllQuestions();
            return Page();
        }

        [BindProperty]
        public Answer Answer { get; set; }

        public List<Question> Questions { get; set; }

        public int? QuestionId { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (QuestionId != null)
            {
                await _databaseLogic.CreateAnswer(Answer, QuestionId);
            }
            else
            {
                await _databaseLogic.CreateAnswer(Answer);
            }

            return RedirectToPage("./Index");
        }
    }
}
