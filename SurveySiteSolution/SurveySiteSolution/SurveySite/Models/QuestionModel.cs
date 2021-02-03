using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveySite.Models
{
    public enum QuestionType { Text, Radio, MultipleChoice }
    public class QuestionModel
    {
        [Key]
        public int Id { get; set; }
        public QuestionType QuestionType { get; set; }
        public string Question { get; set; }
        public List<string> Answers { get; set; }

    }
}
