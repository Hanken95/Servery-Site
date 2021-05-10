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
        private readonly DatabaseLogic _databaseLogic;

        public CreateMultipleAnswersModel(SurveyDBContext context)
        {
            _databaseLogic = new DatabaseLogic(context);
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

            Question = await _databaseLogic.GetQuestion(questionId);
            await _databaseLogic.GetAllSurveys();
            if (Question.Survey != null)
            {
                Survey = await _databaseLogic.GetSurvey(Question.Survey.Id);
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

            await _databaseLogic.CreateAnswer(Answer, Question.Id);

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

        public IActionResult OnPostAddAnswer()
        {
            NumberOfAnswersLeft++;
            return RedirectToPage(
                    new
                    {
                        numberOfAnswers = NumberOfAnswersLeft,
                        numberOfQuestionsLeft = NumberOfQuestionsLeft,
                        questionId = Question.Id
                    });
        }

        public IActionResult OnPostOneFewerAnswer()
        {
            NumberOfAnswersLeft--;
            if (NumberOfAnswersLeft < 1)
            {
                if (NumberOfQuestionsLeft == null)
                {
                    return RedirectToPage(
                    "/QuestionPages/Details", new { id = Question.Id });
                }
                else if (NumberOfQuestionsLeft < 1)
                {
                    return RedirectToPage("/SurveyPages/CompleteSurvey", new { surveyId = Survey.Id });
                }
                else
                {
                    return RedirectToPage(
                    "/QuestionPages/CreateMultipleQuestions",
                    new
                    {
                        numberOfQuestionsLeft = NumberOfQuestionsLeft,
                        surveyId = Survey.Id
                    });
                }
            }
            return RedirectToPage(
                    new
                    {
                        numberOfAnswers = NumberOfAnswersLeft,
                        numberOfQuestionsLeft = NumberOfQuestionsLeft,
                        questionId = Question.Id
                    });
        }
    }
}

