namespace Application.Dto.Customer;

public static class CustomerDto
{
    public record Response(long CustomerId, 
        string FirstName, 
        string LastName, 
        string Email, 
        string PhoneNumber);
}