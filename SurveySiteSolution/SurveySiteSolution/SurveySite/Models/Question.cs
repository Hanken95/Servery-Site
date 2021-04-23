using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [DisplayName("Type of Question")]
        public QuestionType QuestionType { get; set; }

        [DisplayName("Question")]
        public string QuestionString { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
        public Survey Survey { get; set; }
    }
}
