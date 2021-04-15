using Microsoft.EntityFrameworkCore;
using SurveySite.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveySite
{
    public class SurveyDBContext : DbContext
    {
        public SurveyDBContext (DbContextOptions<SurveyDBContext> options)
            : base(options)
        {
        }

        public DbSet<Survey> Survey { get; set; }

        public DbSet<Question> Question { get; set; }

        public DbSet<Answer> Answer { get; set; }

        public void SeedDatabase()
        {
            RemoveRange(Survey.ToList());
            RemoveRange(Question.ToList());
            RemoveRange(Answer.ToList());
            SaveChanges();
            var survey1 = new Survey
            {
                SurveyName = "Feelings",
                Questions = new List<Question>
                    {
                        new Question
                        {
                            QuestionString = "How angry are you?",
                            QuestionType = QuestionType.Radio,
                            Answers = new List<Answer>
                            {
                                new Answer
                                {
                                    AnswerString = "Not at all"
                                },
                                new Answer
                                {
                                    AnswerString = "Kind of angry"
                                },
                                new Answer
                                {
                                    AnswerString = "Angry"
                                },
                                new Answer
                                {
                                    AnswerString = "Very angry"
                                },
                            }
                        },
                        new Question
                        {
                            QuestionString = "What are you sad about?",
                            QuestionType = QuestionType.MultipleChoice,
                            Answers = new List<Answer>
                            {
                                new Answer
                                {
                                    AnswerString = "My work"
                                },
                                new Answer
                                {
                                    AnswerString = "My lovelife"
                                },
                                new Answer
                                {
                                    AnswerString = "My friends"
                                },
                                new Answer
                                {
                                    AnswerString = "I'm not sad about anything"
                                },
                            }
                        },
                        new Question
                        {
                            QuestionString = "How happy are you?",
                            QuestionType = QuestionType.Radio,
                            Answers = new List<Answer>
                            {
                                new Answer
                                {
                                    AnswerString = "Not happy at all"
                                },
                                new Answer
                                {
                                    AnswerString = "Kind of happy"
                                },
                                new Answer
                                {
                                    AnswerString = "Happy"
                                },
                                new Answer
                                {
                                    AnswerString = "Very happy"
                                },
                            }
                        }
                    }
            };
            Survey.Add(survey1);
            SaveChanges();
        }
    }
}
