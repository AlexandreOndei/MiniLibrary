namespace MiniLibrary.Entity
{
    public class User
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordHashConfirmation { get; set; }
    }
}
