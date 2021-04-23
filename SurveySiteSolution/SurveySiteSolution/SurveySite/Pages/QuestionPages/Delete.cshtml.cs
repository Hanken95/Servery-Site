﻿using System;
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
    public class DeleteModel : PageModel
    {
        private readonly SurveySite.SurveyDBContext _context;

        public DeleteModel(SurveySite.SurveyDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Question Question { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Question.FirstOrDefaultAsync(m => m.Id == id);
            await _context.Answer.ToListAsync();

            if (Question == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await _context.Question.FindAsync(id);
            await _context.Answer.ToListAsync();

            if (Question != null)
            {
                _context.Question.Remove(Question);
                foreach (var answer in Question.Answers)
                {
                    _context.Answer.Remove(answer);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
