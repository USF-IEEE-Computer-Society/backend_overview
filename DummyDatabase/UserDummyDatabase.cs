using Workshop_Basics.Models;

namespace Workshop_Basics.DummyDatabase;

public class UserDummyDatabase
{
    private List<DummyUser> Users { get; set; }

    public UserDummyDatabase()
    {
        Users =  new List<DummyUser>
        {
            new DummyUser { UserId = 1, FirstName = "admin", Username = "admin", Password = "admin" },
            new DummyUser { UserId = 2, FirstName = "Luke", Username = "jedi", Password = "theforce" },
            new DummyUser { UserId = 3, FirstName = "Tony", Username = "ironman", Password = "iamironman" },
            new DummyUser { UserId = 4, FirstName = "Frodo", Username = "ringbearer", Password = "onering" },
            new DummyUser { UserId = 5, FirstName = "Neo", Username = "theone", Password = "redpill" },
            new DummyUser { UserId = 6, FirstName = "Ellen", Username = "nostromo", Password = "inspace" },
            new DummyUser { UserId = 7, FirstName = "Bruce", Username = "batman", Password = "darkknight" },
            new DummyUser { UserId = 8, FirstName = "Diana", Username = "wonderwoman", Password = "truthlasso" },
            new DummyUser { UserId = 9, FirstName = "Jack", Username = "captain", Password = "savvy" },
            new DummyUser { UserId = 10, FirstName = "Peter", Username = "spiderman", Password = "withgreatpower" }
        };
    }
    
    

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
   
}