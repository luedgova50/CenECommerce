namespace CenECommerce.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]        
        public int WareHouseId { get; set; }

        [Required(ErrorMessage = "You must enter a {0}")]
        public int ProductID { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}",
            ApplyFormatInEditMode = false)]
        public double Stock { get; set; }

        public virtual WareHouse WareHouse { get; set; }

        public virtual Product Product { get; set; }

    }
}