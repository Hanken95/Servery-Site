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
    public class IndexModel : PageModel
    {
        private readonly DatabaseLogic _databaseLogic;

        public IndexModel(SurveyDBContext context)
        {
            _databaseLogic = new DatabaseLogic(context);
        }

        public IList<Question> Questions { get;set; }

        public async Task OnGetAsync()
        {
            Questions = await _databaseLogic.GetAllQuestions();
        }
    }
}
