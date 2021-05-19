using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurveySite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveySite
{
    public class DatabaseLogic
    {
        private readonly SurveyDBContext _context;

        public DatabaseLogic(SurveyDBContext context)
        {
            _context = context;
        }

        public async Task CreateSurvey(Survey survey)
        {
            await _context.Survey.AddAsync(survey);
            await _context.SaveChangesAsync();
        }

        public async Task CreateQuestion(Question question)
        {
            await _context.Question.AddAsync(question);
            await _context.SaveChangesAsync();
        }

        public async Task<Question> CreateQuestion(Question question, int? surveyId)
        {
            var survey = await _context.Survey.FirstOrDefaultAsync(s => s.Id == surveyId);
            await _context.Question.ToListAsync();
            survey.Questions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        public async Task CreateAnswer(Answer answer)
        {
            await _context.Answer.AddAsync(answer);
            await _context.SaveChangesAsync();
        }

        public async Task CreateAnswer(Answer answer, int? questionId)
        {
            var question = await _context.Question.FirstOrDefaultAsync(q => q.Id == questionId);
            await _context.Answer.ToListAsync();
            question.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSurvey(Survey survey)
        {

            _context.Survey.Remove(survey);
            await _context.Answer.ToListAsync();
            await _context.Question.ToListAsync();
            foreach (var question in survey.Questions)
            {
                _context.Question.Remove(question);
                foreach (var answer in question.Answers)
                {
                    _context.Answer.Remove(answer);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestion(Question question)
        {
            _context.Question.Remove(question);
            await _context.Answer.ToListAsync();
            foreach (var answer in question.Answers)
            {
                _context.Answer.Remove(answer);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAnswer(Answer answer)
        {
            _context.Answer.Remove(answer);
            await _context.SaveChangesAsync();
        }

        internal async Task<Question> AddQuestionToSurvey(int questionId, int? surveyId)
        {
            var survey = await _context.Survey.FirstOrDefaultAsync(s => s.Id == surveyId);
            var question = await _context.Question.FirstOrDefaultAsync(q => q.Id == questionId);
            await _context.Question.ToListAsync();
            survey.Questions.Add(question);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObjectExists(surveyId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return question;
        }

        internal async Task<Answer> AddAnswerToQuestion(int answerId, int? questionId)
        {
            var question = await _context.Question.FirstOrDefaultAsync(q => q.Id == questionId);
            var answer = await _context.Answer.FirstOrDefaultAsync(a => a.Id == answerId);
            await GetAllAnswers();
            question.Answers.Add(answer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObjectExists(question))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return answer;
        }

        public async Task<bool> EditSurvey(Survey survey)
        {
            _context.Attach(survey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObjectExists(survey))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public async Task<bool> EditQuestion(Question question)
        {
            _context.Attach(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObjectExists(question))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public async Task<bool> EditAnswer(Answer answer)
        {
            _context.Attach(answer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObjectExists(answer))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public async Task<List<Survey>> GetAllSurveys()
        {
            var list = await _context.Survey.ToListAsync();
            await _context.Question.ToListAsync();
            return list;
        }

        public async Task<List<Question>> GetAllQuestions()
        {
            var list = await _context.Question.ToListAsync();
            await _context.Answer.ToListAsync();
            return list;
        }

        public async Task<List<Answer>> GetAllAnswers()
        {
            return await _context.Answer.ToListAsync();
        }
        
        public async Task<Survey> GetSurvey(int? id)
        {
            return await _context.Survey.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Question> GetQuestion(int? id)
        {
            return await _context.Question.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Answer> GetAnswer(int? id)
        {
            return await _context.Answer.FirstOrDefaultAsync(m => m.Id == id); ;
        }

        public async Task RemoveQuestionFromSurvey(int questionId, int surveyId)
        {
            var survey = await _context.Survey.FirstOrDefaultAsync(s => s.Id == surveyId);
            var question = await _context.Question.FirstOrDefaultAsync(q => q.Id == questionId);
            await _context.Question.ToListAsync();
            survey.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAnswerFromQuestion(int questionId, int answerId)
        {
            var question = await _context.Question.FirstOrDefaultAsync(q => q.Id == questionId);
            var answer = await _context.Answer.FirstOrDefaultAsync(a => a.Id == answerId);
            await _context.Answer.ToListAsync();
            question.Answers.Remove(answer);
            await _context.SaveChangesAsync();
        }

        public void SeedDatabase()
        {
            _context.RemoveRange(_context.Survey.ToList());
            _context.RemoveRange(_context.Question.ToList());
            _context.RemoveRange(_context.Answer.ToList());
            _context.SaveChanges();
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
            _context.Survey.AddRange(survey1, survey2, survey3);
            _context.SaveChanges();
        }

        private bool ObjectExists(object objectToCheck)
        {
            if (objectToCheck is Survey survey)
            {
                return _context.Survey.Any(s => s.Id == survey.Id);
            }
            else if (objectToCheck is Question question)
            {
                return _context.Survey.Any(q => q.Id == question.Id);
            }
            else if (objectToCheck is Answer answer)
            {
                return _context.Survey.Any(a => a.Id == answer.Id);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
