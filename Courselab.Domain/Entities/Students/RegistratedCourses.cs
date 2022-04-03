using Courselab.Domain.Entities.Courses;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courselab.Domain.Entities.Students
{
    public class RegistratedCourses
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }
        public Guid CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }
        public DateTime StartedDate { get; set; } = DateTime.Now;
        public bool IsFinished { get; set; } = false;
        public DateTime? FinishedDate { get; set; } = null;
    }
}