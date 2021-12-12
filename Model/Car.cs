using System.ComponentModel.DataAnnotations;

namespace ParcialComputo3.Models
{
    public class Car
    {
        [Key]
        public int idCar { get; set; }
        public string modelCar { get; set; }
        public string colorCar { get; set; }
        public string yearCar { get; set; }
        public string typeCar { get; set; }
        
        // Instanciar la Key de la clase Brand.
        public int idBrand { get; set; }

    }
}