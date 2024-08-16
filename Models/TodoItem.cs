using System.ComponentModel.DataAnnotations;

namespace Todo_A_P_I.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        [Required]   
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }

}
