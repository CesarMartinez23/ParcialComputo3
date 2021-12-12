using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//Importar el espacio de nombre de donde se encuentra el contexto de la Base de Datos.
using ParcialComputo3.Data;
using ParcialComputo3.Models;

namespace ParcialComputo3.Controllers
{
    ///<summary>
    ///Definiendo que sera un Controlador para API llamado Brand, igual que el controlador.
    ///</summary>
    [ApiController]
    //Definir la Ruta de la API, llamada del mismo nombre que el controller de la API.
    [Route("[controller]")]
    public class BrandController : Controller
    {
        // Creando la referencia al Contexto de la Base de Datos.
        private DatabaseContext _context;

        //Definir el constructor para inicializar el contexto de la Base de Datos.
        public BrandController(DatabaseContext context)
        {
            _context = context;
        }

        ///<summary>
        ///Definicion del Primer Verbo HTTP a utilizar (HttpGet) (Obteniendo las Marcas registradas en la API).
        ///</summary>
        ///<remarks>
        ///Con este verbo HTTP obtendremos el resultado del objeto, los datos de las Marcas registradas en nuestra API.
        ///</remarks>
        ///<response code="200">Registros de Marcas, encontrados correctamente.</response>
        /// <response code="404">NOT FOUND. No se han podido encontrar las Marcas registradas en esta API.</response>
        [HttpGet]
        public async Task<ActionResult<List<Brand>>> GetBrands()
        {
            var brands = await _context.Brands.ToListAsync();
            return brands;
        }

        /// <summary>
        /// Definicion del Verbo HTTP a utilizar (HttpGet) (Obteniendo los registros del ID de Marca solicitada).
        /// </summary>
        /// <remarks>
        /// Con este verbo HTTP obtendremos el resultado del objeto, los datos de la Marcas solicitada mediante el ID de Marca en nuestra API.
        /// </remarks>
        /// <param name="id">(ID) Identificador de la Marca.</param>
        /// <response code="200">Registro de la Marca, encontrado correctamente.</response>
        /// <response code="404">NOT FOUND. No se ha podido encontrar la Marca solicitada mediante el ID de la Marca en esta API.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrandsByID(int id)
        {
            var brands = await _context.Brands.FindAsync(id);
            if(brands==null)
            {
                return NotFound();
            }
            return brands;
        }
    }                           
}