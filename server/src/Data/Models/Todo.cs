using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Data.Models
{
    [Table("Todos")]
    public class Todo
    {
        [Key]
        public int Id {get; set;}

        public string Text {get; set;}

        public bool Done {get; set;}
    }
}
