using Courselab.Domain.Entities.Courses;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courselab.Domain.Entities.Registraions
{
    public class Registration
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; }
        public DateTime StartedDate { get; set; } = DateTime.Now;
        public bool IsFinished { get; set; } = false;
        public DateTime? FinishedDate { get; set; } = null;

        public void Start(Course course) 
        {
            StartedDate = DateTime.Now;
            CourseId = course.Id;
            Course = course;
            IsFinished = false;
        }
        
        public void Finish() 
        {
            IsFinished = true;
            FinishedDate = DateTime.Now;
        }
    }
}