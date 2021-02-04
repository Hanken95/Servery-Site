using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveySite.Models
{
    public enum QuestionType { Text, Radio, MultipleChoice }
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public QuestionType QuestionType { get; set; }
        public string QuestionString { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Survey> Surveys { get; set; }
    }
}
