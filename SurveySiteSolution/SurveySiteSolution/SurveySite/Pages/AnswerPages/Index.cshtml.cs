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
    public class IndexModel : PageModel
    {
        private readonly DatabaseLogic _databaseLogic;

        public IndexModel(SurveyDBContext context)
        {
            _databaseLogic = new DatabaseLogic(context);
        }

        public IList<Answer> Answers { get;set; }

        public async Task OnGetAsync()
        {
            Answers = await _databaseLogic.GetAllAnswers();
            await _databaseLogic.GetAllQuestions();
        }
    }
}
