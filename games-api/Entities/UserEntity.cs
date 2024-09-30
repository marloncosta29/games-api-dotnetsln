using System.ComponentModel.DataAnnotations;

namespace GamesAPI.Entities
{
    public class UserEntity {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Hash { get; set; }
        public string Role { get; set; }
    }
}