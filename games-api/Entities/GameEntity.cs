using System;
using System.ComponentModel.DataAnnotations;

namespace GamesAPI.Entities
{
    public class GameEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }

    }
}