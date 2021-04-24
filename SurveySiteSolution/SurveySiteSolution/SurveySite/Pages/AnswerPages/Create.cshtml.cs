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
        private readonly SurveySite.SurveyDBContext _context;

        public CreateModel(SurveySite.SurveyDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Questions = await _context.Question.ToListAsync();
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
                await _context.Question.ToListAsync();
                var question = await _context.Question.FirstOrDefaultAsync(s => s.Id == QuestionId);
                question.Answers.Add(Answer);
            }
            else
            {
                await _context.Answer.AddAsync(Answer);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
