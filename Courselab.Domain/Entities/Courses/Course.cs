using Courselab.Domain.Commons;
using Courselab.Domain.Entities.Authors;
using Courselab.Domain.Enums;
using Courselab.Domain.Localization;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courselab.Domain.Entities.Courses
{
    public class Course : Auditable, ILocalizationName
    {
        /// <summary>
        /// multilingual names
        /// </summary>
        [JsonIgnore]
        public string NameUz { get; set; }
        [JsonIgnore]
        public string NameRu { get; set; }
        [JsonIgnore]
        public string NameEn { get; set; }

        //name in choosen language
        [NotMapped]
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
        public CourseType Type { get; set; }

        public Guid AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }
    }
}
