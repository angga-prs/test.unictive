using System.Text.Json.Serialization;

namespace test.unictive.Models
{
    public class UserHobby
    {
        public int UserId { get; set; }
        public int HobbyId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public Hobby Hobby { get; set; }
    }

    public class UserHobbyDto
    {
        public int HobbyId { get; set; }
        public string HobbyName { get; set; }
    }
}
