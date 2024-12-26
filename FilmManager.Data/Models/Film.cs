using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmManager.Data.Models
{
    public class Film
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required.")]
        [MaxLength(20, ErrorMessage = "Genre cannot exceed 20 characters.")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Director is required.")]
        [MaxLength(30, ErrorMessage = "Director name cannot exceed 30 characters.")]
        public string Director { get; set; } = string.Empty;

        [Required(ErrorMessage = "Release Year is required.")]
        [Range(1900, 2999, ErrorMessage = "Release Year must be between 1900 and 2999.")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
        public double Rating { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }
    }
}
