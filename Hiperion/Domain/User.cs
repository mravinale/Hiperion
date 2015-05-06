namespace Hiperion.Domain
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

		[MaxLength(10)]
        public string Name { get; set; }

        public string Address { get; set; }
    }
}