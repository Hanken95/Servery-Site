using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SurveySite.Models;
using SurveySite;
using Microsoft.EntityFrameworkCore;

namespace SurveySite.Pages.AnswerPages
{
    public class CreateMultipleAnswersModel : PageModel
    {
        private readonly SurveyDBContext _context;

        public CreateMultipleAnswersModel(SurveyDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Survey Survey { get; set; } = new Survey();

        [BindProperty]
        public Question Question { get; set; }

        [BindProperty]
        public Answer Answer { get; set; }

        [BindProperty]
        public int NumberOfAnswersLeft { get; set; }

        [BindProperty]
        public int? NumberOfQuestionsLeft { get; set; }

        public async Task<IActionResult> OnGetAsync(int? numberOfQuestionsLeft, int numberOfAnswers, int? questionId)
        {
            if (questionId == null)
            {
                return NotFound();
            }

            Question = await _context.Question.FirstOrDefaultAsync(q => q.Id == questionId);
            await _context.Survey.ToListAsync();
            if (Question.Survey != null)
            {
                Survey = await _context.Survey.FirstOrDefaultAsync(s => s.Id == Question.Survey.Id);
            }


            NumberOfAnswersLeft = numberOfAnswers;
            NumberOfQuestionsLeft = numberOfQuestionsLeft;

            if (Question == null)
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

            Question.Answers.Add(Answer);
            _context.Attach(Question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionExists(Question.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            NumberOfAnswersLeft -= 1;
            if (NumberOfAnswersLeft > 0)
            {
                return RedirectToPage(
                    new { 
                    numberOfAnswers = NumberOfAnswersLeft, 
                    numberOfQuestionsLeft = NumberOfQuestionsLeft, 
                    questionId = Question.Id });
            }
            else if (NumberOfQuestionsLeft == null)
            {
                return RedirectToPage(
                    "/QuestionPages/Details",
                    new
                    {
                        id = Question.Id
                    });
            }
            else if (NumberOfQuestionsLeft > 0)
            {
                return RedirectToPage(
                    "/QuestionPages/CreateMultipleQuestions", 
                    new {
                    numberOfQuestionsLeft = NumberOfQuestionsLeft,
                    surveyId = Survey.Id });
            }
            return RedirectToPage("/SurveyPages/CompleteSurvey",
                new {
                    surveyId = Survey.Id
                });
            
        }

        private bool QuestionExists(int id)
        {
            return _context.Question.Any(e => e.Id == id);
        }
    }
}

