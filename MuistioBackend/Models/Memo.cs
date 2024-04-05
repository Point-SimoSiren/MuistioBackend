using System.ComponentModel.DataAnnotations;

namespace MuistioBackend.Models
{
    public class Memo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Note { get; set; }
        [Required]
        public bool Important { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
