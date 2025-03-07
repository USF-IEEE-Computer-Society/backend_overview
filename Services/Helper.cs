using System.Text.RegularExpressions;
using Workshop_Basics.Database.Entities;
using Workshop_Basics.Models;

namespace Workshop_Basics.Services;

public class Helper
{
    public bool CheckUserSignUp(UserSignup? user)
    {
        if (user == null)
        {
            return false;
        }

        // Check nickname (min 3 characters)
        if (string.IsNullOrWhiteSpace(user.nickname) || user.nickname.Length < 3)
        {
            return false;
        }

        // Check email is not null and matches a basic email pattern
        if (string.IsNullOrWhiteSpace(user.email))
        {
            return false;
        }

        // Basic regex for email validation
        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(user.email, emailPattern))
        {
            return false;
        }

        if (user.password.Length < 5 || user.password.Length > 50)
        {
            return false;
        }

        return true;
    }
    
    public bool CheckPost(Post? post)
    {
        if (post == null)
        {
            return false;
        }

        
        if (string.IsNullOrWhiteSpace(post.Content) || post.Content.Length < 5)
        {
            return false;
        }
        
        //Todo: Try to add UserID checker 

        return true;
    }
    
    //Todo: do user credential check and use it in controllers 

}