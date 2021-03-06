using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotChocolate.AspNetCore.Authorization;

namespace api.Models
{
    [Authorize]
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string FusionProjectId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Evaluation> Evaluations { get; set; }
    }

    public class Evaluation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ProjectId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public Progression? Progression { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual Project Project { get; set; }
    }

    public class Participant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string EvaluationId { get; set; }
        [Required]
        public string AzureUniqueId { get; set; }
        [Required]
        public Organization? Organization { get; set; }
        [Required]
        public Role? Role { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public virtual Evaluation Evaluation { get; set; }
    }

    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string EvaluationId { get; set; }
        [Required]
        public string QuestionTemplateId { get; set; }
        [Required]
        public Organization? Organization { get; set; }
        public string Text { get; set; }
        public string SupportNotes { get; set; }
        [Required]
        public Barrier? Barrier { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Action> Actions { get; set; }
        public virtual Evaluation Evaluation { get; set; }
        public virtual QuestionTemplate QuestionTemplate { get; set; }
    }

    public class QuestionTemplate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public Status? Status { get; set; }
        [Required]
        public Organization? Organization { get; set; }
        public string Text { get; set; }
        public string SupportNotes { get; set; }
        [Required]
        public Barrier? Barrier { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }

    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string QuestionId { get; set; }
        [Required]
        public Progression? Progression { get; set; }
        public Severity? Severity { get; set; }
        public string Text { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public Participant AnsweredBy { get; set; }
        public virtual Question Question { get; set; }
    }

    public class Action
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string QuestionId { get; set; }
        public Participant AssignedTo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority? Priority { get; set; }
        public bool OnHold { get; set; }
        public DateTime DueDate { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public Participant CreatedBy { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual Question Question { get; set; }
    }

    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        [Required]
        public string ActionId { get; set; }
        public string Text { get; set; }
        public Participant CreatedBy { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public virtual Action Action { get; set; }
    }

    public enum Status
    {
        Active, Inactive
    }

    public enum Progression
    {
        Nomination, Preparation, Alignment, Workshop, FollowUp
    }

    public enum Barrier
    {
        GM, PS1, PS2, PS3, PS4, PS5, PS6, PS7, PS12, PS15, PS22
    }

    public enum Organization
    {
        Commissioning, Construction, Engineering, PreOps, All
    }

    public enum Role
    {
        Participant, Facilitator, ReadOnly
    }

    public enum Severity
    {
        Low, Limited, High, NA
    }

    public enum Priority
    {
        Low, Medium, High
    }
}
