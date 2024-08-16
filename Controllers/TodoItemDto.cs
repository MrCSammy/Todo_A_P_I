namespace Todo_A_P_I.Controllers
{
    public class TodoItemDto
    {
        public string Title { get; set; }      // Allow user to specify a title
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }

}
