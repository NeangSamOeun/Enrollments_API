public class ForgotPasswordDTO
{
    public string Email { get; set; }
}

public class ResetPasswordDTO
{
    public string Email { get; set; }
    public string OtpCode { get; set; }
    public string NewPassword { get; set; }
}
