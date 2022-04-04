using Courselab.Domain.Entities.Courses;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courselab.Domain.Entities.Students
{
    public class RegistratedCourse
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; }
        public DateTime StartedDate { get; set; } = DateTime.Now;
        public bool IsFinished { get; set; } = false;
        public DateTime? FinishedDate { get; set; } = null;
    }
}