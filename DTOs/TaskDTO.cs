using System.ComponentModel.DataAnnotations;

namespace ToDoAppMinimalAPI.DTOs
{
    public abstract record TaskDTO
    {
        [Key]
        public int Id { get; init; }

        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
        public string Title { get; init; } = string.Empty;

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        public string? Description { get; init; }

        public bool IsCompleted { get; init; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; init; }

        [DataType(DataType.Date)]
        public DateTime? DueDate { get; init; }

        [Range(1, int.MaxValue, ErrorMessage = "CreatedBy geçerli bir kullanıcı ID olmalıdır.")]
        public int? CreatedBy { get; init; }

        [Range(1, int.MaxValue, ErrorMessage = "LastModifiedBy geçerli bir kullanıcı ID olmalıdır.")]
        public int? LastModifiedBy { get; init; }

        public bool IsDeleted { get; init; } = false;
    }
}
