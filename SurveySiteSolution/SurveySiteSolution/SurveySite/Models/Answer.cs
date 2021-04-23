using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SurveySite.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Answer")]
        public string AnswerString { get; set; }
        public Question Question { get; set; }
    }
}