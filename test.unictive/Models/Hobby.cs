using System.Text.Json.Serialization;

namespace test.unictive.Models
{
    public class Hobby
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<UserHobby> UserHobbies { get; set; }
    }
}
