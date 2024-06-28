using E.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace E.Domain.Models
{
    public class News : BaseEntity
    {
        [Display(Name ="Tiêu đề ")]
        public string Title { get; set; }
        [Display(Name = "Nội dung")]
        public string Content { get; set; }
        [Display(Name = "Người đăng")]
        public string Author { get; set; }
        public List<string>? Image { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
