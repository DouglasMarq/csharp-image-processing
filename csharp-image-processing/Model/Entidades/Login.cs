using SQLite;

namespace csharp_image_processing.Model.Entidades
{
    [Table("Login")]
    public class Login
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string User { get; set; }

        public string Pass { get; set; }

        public int AccessLevel { get; set; }
    }
}
