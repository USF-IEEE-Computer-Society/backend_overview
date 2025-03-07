using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace Workshop_Basics.Database.Entities;

public class User
{
    public int UserId { get; set; }
   
    public string FirstName { get; set; }
    
  
    public string LastName { get; set; }
    
   
    public string Nickname { get; set; }
   
    
    public string Email { get; set; }
    
   
    public string Password { get; set; }

    [NotMapped] public string FullName => FirstName + " " + LastName;  //Not mapped
    
    //navigational properties
    public List<Post>? Posts { get; set; }
    public Admin? Admin { get; set; }
}