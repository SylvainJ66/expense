using Lucca.Domain.Models.IdProvider;

namespace Lucca.Domain.Models.Users;

public sealed class User
{
    public Guid Id { get; }
    public string Firstname { get; }
    public string Name { get; }
    public string Currency { get; }
    
    private User(
        Guid id, 
        string firstname, 
        string name, 
        string currency)
    {
        Id = id;
        Firstname = firstname;
        Name = name;
        Currency = currency;
    }
    
    public static User Create(
        IIdProvider idProvider,
        string firstname, 
        string name, 
        string currency)
    {
        return new User(
            idProvider.NewId(), 
            firstname, 
            name, 
            currency);
    }

    public static User From(
        Guid id, 
        string name, 
        string firstname, 
        string currency)
    {
        return new User(
            id, 
            name, 
            firstname, 
            currency);
    }
}