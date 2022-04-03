using Courselab.Domain.Commons;
using Courselab.Domain.Entities.Authors;
using Courselab.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courselab.Domain.Entities.Courses
{
    public class Course : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public CourseType Type { get; set; }
        [JsonIgnore]
        public Guid AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }
    }
}
