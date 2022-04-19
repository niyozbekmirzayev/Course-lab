using Courselab.Domain.Commons;
using Courselab.Domain.Entities.Users;
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
        public string Image { get; set; }
        public string GuidVideo { get; set; }
        public CourseType Type { get; set; }
        public CourseLevel Level { get; set; }
        public string YouTubePlayListLink { get; set; }
        [JsonIgnore]
        public Guid AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public virtual User Author { get; set; }
        public double? Rating { get; set; }
    }
}
