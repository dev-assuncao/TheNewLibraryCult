using LibraryCult.Core.DomainObjects;

namespace LibraryCult.Catalogo.API.Models
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Image { get; set; }
        public int WareHouseQuantity { get; set; }
    }
}
