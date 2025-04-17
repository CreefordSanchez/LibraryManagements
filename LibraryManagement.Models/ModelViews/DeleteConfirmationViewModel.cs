namespace LibraryManagement.Models.ModelViews
{
    public class DeleteConfirmationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string EntityName { get; set; }
        public string DeleteAction { get; set; }
        public string DeleteController { get; set; }
    }
}