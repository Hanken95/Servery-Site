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
            var survey2 = new Survey
            {
                SurveyName = "Work",
                Questions = new List<Question>
                    {
                        new Question
                        {
                            QuestionString = "How much do you work?",
                            QuestionType = QuestionType.Radio,
                            Answers = new List<Answer>
                            {
                                new Answer
                                {
                                    AnswerString = "Not at all"
                                },
                                new Answer
                                {
                                    AnswerString = "25%"
                                },
                                new Answer
                                {
                                    AnswerString = "50%"
                                },
                                new Answer
                                {
                                    AnswerString = "50-100%"
                                },
                            }
                        },
                        new Question
                        {
                            QuestionString = "What do you work with?",
                            QuestionType = QuestionType.MultipleChoice,
                            Answers = new List<Answer>
                            {
                                new Answer
                                {
                                    AnswerString = "People"
                                },
                                new Answer
                                {
                                    AnswerString = "Machines"
                                },
                                new Answer
                                {
                                    AnswerString = "Animals"
                                },
                                new Answer
                                {
                                    AnswerString = "Tools"
                                },
                            }
                        },
                        new Question
                        {
                            QuestionString = "How happy are you with your workplace?",
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
                        },
                        new Question
                        {
                            QuestionString = "How much do you earn per month?",
                            QuestionType = QuestionType.Radio,
                            Answers = new List<Answer>
                            {
                                new Answer
                                {
                                    AnswerString = "0-10k"
                                },
                                new Answer
                                {
                                    AnswerString = "10-20k"
                                },
                                new Answer
                                {
                                    AnswerString = "20-30k"
                                },
                                new Answer
                                {
                                    AnswerString = "More than 30k"
                                },
                            }
                        }
                    }
            };
            var survey3 = new Survey
            {
                SurveyName = "Animals",
                Questions = new List<Question>
                    {
                        new Question
                        {
                            QuestionString = "Do you want pets",
                            QuestionType = QuestionType.Radio,
                            Answers = new List<Answer>
                            {
                                new Answer
                                {
                                    AnswerString = "Not at all"
                                },
                                new Answer
                                {
                                    AnswerString = "I want 1 pet"
                                },
                                new Answer
                                {
                                    AnswerString = "I want 2 pets"
                                },
                                new Answer
                                {
                                    AnswerString = "I want more than 2 pets"
                                },
                            }
                        },
                        new Question
                        {
                            QuestionString = "What kind of pet do you want?",
                            QuestionType = QuestionType.MultipleChoice,
                            Answers = new List<Answer>
                            {
                                new Answer
                                {
                                    AnswerString = "Dog"
                                },
                                new Answer
                                {
                                    AnswerString = "Cat"
                                },
                                new Answer
                                {
                                    AnswerString = "Rodent"
                                },
                                new Answer
                                {
                                    AnswerString = "Other"
                                },
                            }
                        }
                    }
            };
            Survey.AddRange(survey1, survey2, survey3);
            SaveChanges();
        }
    }
}
