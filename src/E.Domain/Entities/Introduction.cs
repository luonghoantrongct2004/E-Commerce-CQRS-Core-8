using E.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace E.Domain.Models
{
    public class Introduction : BaseEntity
    {
        [Display(Name = "Tiêu đề ")]
        public string Title { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name = "Ảnh")]
        public List<string>? ImageUrl { get; set; }
        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; }
    }
}
