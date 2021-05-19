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
    public class CreateMultipleQuestionsModel : PageModel
    {
        private readonly DatabaseLogic _databaseLogic;

        public CreateMultipleQuestionsModel(SurveyDBContext context)
        {
            _databaseLogic = new DatabaseLogic(context);
        }

        [BindProperty]
        public Survey Survey { get; set; }

        [BindProperty]
        public Question Question { get; set; }

        [BindProperty]
        public int? NumberOfQuestionsLeft { get; set; }
        
        [BindProperty]
        public int NumberOfAnswers { get; set; }

        public async Task<IActionResult> OnGetAsync(int? numberOfQuestionsLeft,int? surveyId)
        {
            if (surveyId == null)
            {
                return NotFound();
            }

            Survey = await _databaseLogic.GetSurvey(surveyId);

            NumberOfQuestionsLeft = numberOfQuestionsLeft;

            if (Survey == null)
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

            Question = await _databaseLogic.CreateQuestion(Question, Survey.Id);

            NumberOfQuestionsLeft -= 1;

            if (Question.QuestionType == QuestionType.Text || NumberOfAnswers < 1)
            {
                if (NumberOfQuestionsLeft > 0)
                {
                    return RedirectToPage(
                    new
                    {
                        numberOfQuestionsLeft = NumberOfQuestionsLeft,
                        surveyId = Survey.Id
                    });
                }
                return RedirectToPage("/SurveyPages/CompleteSurvey", new { surveyId = Survey.Id });
            }
            else
            {
                return RedirectToPage("/AnswerPages/CreateMultipleAnswers",
                    new
                    {
                        numberOfQuestionsLeft = NumberOfQuestionsLeft,
                        numberOfAnswers = NumberOfAnswers,
                        questionId = Question.Id
                    });
            }
        }

        public IActionResult OnPostAddQuestion()
        {
            NumberOfQuestionsLeft++;
            return RedirectToPage(
                    new { numberOfQuestionsLeft = NumberOfQuestionsLeft, surveyId = Survey.Id });
        }

        public IActionResult OnPostOneFewerQuestion()
        {
            NumberOfQuestionsLeft--;
            if (NumberOfQuestionsLeft < 1)
            {
                return RedirectToPage("/SurveyPages/CompleteSurvey", new { surveyId = Survey.Id });
            }
            return RedirectToPage(
                    new { numberOfQuestionsLeft = NumberOfQuestionsLeft, surveyId = Survey.Id });
        }
    }
}
