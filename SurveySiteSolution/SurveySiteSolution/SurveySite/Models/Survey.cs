using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurveySite.Models
{
    public class Survey
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Name of Survey")]
        public string SurveyName { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
