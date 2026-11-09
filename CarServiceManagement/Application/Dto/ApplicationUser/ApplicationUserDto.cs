namespace Application.Dto.ApplicationUser;

public static class ApplicationUserDto
{
    public record RegisterNewUserDto(
        string UserName, 
        string FirstName, 
        string LastName, 
        string Email,
        string Password,
        string ConfirmPassword,
        string PhoneNumber);

    public record Response(
        long UserId,
        string UserName, 
        string FirstName, 
        string LastName, 
        string Email,
        string PhoneNumber);

}