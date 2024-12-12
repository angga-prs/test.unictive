using System.Text.Json.Serialization;

namespace test.unictive.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<UserHobby> UserHobbies { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> Hobbies { get; set; }
    }

    public class UserDtoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserHobbyDto> UserHobbies { get; set; }
    }
}
