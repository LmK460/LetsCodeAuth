namespace MinimalLetsApiAuth.Models
{
    public class User
    {
        public Role.RoleType? Role { get; set; }  
        public string? UserName { get; set; }
    }
}
