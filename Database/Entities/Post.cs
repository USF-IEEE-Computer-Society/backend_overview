using Newtonsoft.Json;

namespace Workshop_Basics.Database.Entities;

public class Post
{
   
    public int PostId { get; set; }
    public string? Header { get; set; } //? what does it even mean
    public string Content { get; set; }
    public int UserId { get; set; }
    
    [JsonIgnore]
    public User User { get; set; }
    
}