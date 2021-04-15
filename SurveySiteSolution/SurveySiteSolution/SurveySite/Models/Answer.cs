using System.ComponentModel.DataAnnotations;

namespace SurveySite.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public string AnswerString { get; set; }
        public Question Question { get; set; }
    }
}