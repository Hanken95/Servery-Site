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
    public class DeleteModel : PageModel
    {
        private readonly DatabaseLogic _databaseLogic;

        public DeleteModel(SurveyDBContext context)
        {
            _databaseLogic = new DatabaseLogic(context);
        }

        [BindProperty]
        public Survey Survey { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Survey = await _databaseLogic.GetSurvey(id);
            await _databaseLogic.GetAllQuestions();

            if (Survey == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Survey == null)
            {
                return NotFound();
            }

            await _databaseLogic.DeleteSurvey(Survey);

            return RedirectToPage("/Index");
        }
    }
}
