using System.ComponentModel.DataAnnotations;

namespace Lazar_Horatiu_Lab2_Master.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        [DataType( DataType.Date)]
        public DateTime BirthDate { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
