using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Workshop_Basics.Database.Entities;

public class Admin
{
    public int AdminId { get; set; }
    public int UserId { get; set; }
    [JsonIgnore] public User User { get; set; }
}

