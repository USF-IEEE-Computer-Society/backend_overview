using Workshop_Basics.Models;

namespace Workshop_Basics.DummyDatabase;

public class UserDummyDatabase
{
    #region PublicFields
    
    public List<DummyUser> Users { get; set; } = new List<DummyUser>
    {
        new DummyUser { UserId = 1, FirstName = "John", Username = "john_doe", Password = "pass123" },
        new DummyUser { UserId = 2, FirstName = "Jane", Username = "jane_doe", Password = "secure456" },
        new DummyUser { UserId = 3, FirstName = "Alice", Username = "alice_wonder", Password = "alicepass" }
    };
    #endregion

    #region CRUD
    public DummyUser? UpdateUser(int id, string newFirstName, string newUsername, string newPassword)
    {
        DummyUser? user = Users.FirstOrDefault(u => u.UserId == id);
        if (user != null)
        {
            user.FirstName = newFirstName;
            user.Username = newUsername;
            user.Password = newPassword;
        }
        return user;
    }

    public List<DummyUser> GetAllUsers()
    {
        return Users;
    }

    public DummyUser? DeleteUser(int id)
    {
        DummyUser? user = Users.FirstOrDefault(u => u.UserId == id);
        if (user != null)
        {
            Users.Remove(user);
        }
        return user;
    }
    
    
    public DummyUser CreateUser(string firstName, string username, string password)
    {
        int newId = Users.Any() ? Users.Max(u => u.UserId) + 1 : 1;
        DummyUser newUser = new DummyUser { UserId = newId, FirstName = firstName, Username = username, Password = password };
        Users.Add(newUser);
        return newUser;
    }

    public DummyUser? GetUser(string username, string password)
    {
        var user = Users.FirstOrDefault(u => u.Username == username);
        if (user == null || user.Password != password)
            return null;

        return user;
    }
    
    public DummyUser? FindUserById(int id)
    {
        DummyUser? user = Users.FirstOrDefault(u => u.UserId == id);
        return user;
    }
    #endregion
   
}