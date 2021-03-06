using System;
using System.Linq;

using HotChocolate;

using api.Context;
using api.Models;

namespace api.Services
{
    public class QuestionService
    {
        private readonly BmtDbContext _context;

        public QuestionService([Service] BmtDbContext context)
        {
            _context = context;
        }

        public Question Create(
            QuestionTemplate template,
            Evaluation evaluation
        )
        {
            DateTime createDate = DateTime.UtcNow;

            Question newQuestion = new Question
            {
                CreateDate = createDate,
                Barrier = template.Barrier,
                Evaluation = evaluation,
                Organization = template.Organization,
                Text = template.Text,
                SupportNotes = template.SupportNotes,
                QuestionTemplate = template,
            };

            _context.Questions.Add(newQuestion);

            _context.SaveChanges();
            return newQuestion;
        }

        public IQueryable<Question> GetAll()
        {
            return _context.Questions;
        }

        public Question GetQuestion(string questionId)
        {
            Question question = _context.Questions.FirstOrDefault(question => question.Id.Equals(questionId));
            if (question == null)
            {
                throw new NotFoundInDBException($"Question not found: {questionId}");
            }
            return question;
        }
    }
}
