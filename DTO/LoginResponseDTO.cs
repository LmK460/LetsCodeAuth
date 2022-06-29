namespace MinimalLetsApiAuth.DTO
{
    public class LoginResponseDTO
    {
        public string Message { get; set; }
        public bool Autenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string AccessToken { get; set; }

        public LoginResponseDTO()
        {

        }

        public override string ToString()
            => $"Message: {Message} - Autenticated: {Autenticated}";
    }
}
