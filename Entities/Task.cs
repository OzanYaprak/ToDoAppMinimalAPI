using System.ComponentModel.DataAnnotations;

namespace ToDoAppMinimalAPI.Entities
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        public string? Description { get; set; }

        public bool IsCompleted { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "CreatedBy geçerli bir kullanıcı ID olmalıdır.")]
        public int? CreatedBy { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "LastModifiedBy geçerli bir kullanıcı ID olmalıdır.")]
        public int? LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
