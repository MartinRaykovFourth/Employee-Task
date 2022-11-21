using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data
{
    public class Token
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime ExpirationDate { get; set;}
    }
}
