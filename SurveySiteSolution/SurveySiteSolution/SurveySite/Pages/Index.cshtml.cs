using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SurveySite.Models;

namespace SurveySite.Pages.SurveyPages
{
    public class IndexModel : PageModel
    {
        private readonly DatabaseLogic _databaseLogic;

        public IndexModel(SurveyDBContext context)
        {
            _databaseLogic = new DatabaseLogic(context);
        }

        public List<Survey> Surveys { get; set; } = new List<Survey>();

        public async Task OnGetAsync()
        {
            Surveys = await _databaseLogic.GetAllSurveys();
            Page();
        }

        public async Task OnPostSeedDataBase()
        {
            _databaseLogic.SeedDatabase();
            Surveys = await _databaseLogic.GetAllSurveys();
            Page();
        }
    }
}
