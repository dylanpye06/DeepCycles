namespace DeepCycles.Models.ViewModels
{
    public class AdminLogInRequest
    {
        public required string AdminEmail { get; set; }
        public required string AdminPassword { get; set; }
    }
}
