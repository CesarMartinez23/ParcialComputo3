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
    public class CarController : Controller
    {
         //Creando la referencia al Contexto de la Base de Datos.
        private DatabaseContext _context;


        //Definir el constructor para inicializar el contexto de la Base de Datos.
        public CarController(DatabaseContext context)
        {
            _context = context;
        }

        ///<summary>
        ///Definicion del Primer Verbo HTTP a utilizar (HttpGet) (Obteniendo los Vehiculos registrados en la API).
        ///</summary>
        ///<remarks>
        ///Con este verbo HTTP obtendremos el resultado de todos los datos de los Vehiculos registrados en nuestra API.
        ///</remarks>
        ///<response code="200">Registros de Vehiculos, encontrados correctamente.</response>
        /// <response code="404">NOT FOUND. No se han podido encontrar los Vehiculos registrados en esta API.</response>
        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            var cars = await _context.Cars.ToListAsync();
            return cars;
        }

        /// <summary>
        /// Definicion del Verbo HTTP a utilizar (HttpGet) (Obteniendo los registros del ID de Vehiculo solicitado).
        /// </summary>
        /// <remarks>
        /// Con este verbo HTTP obtendremos el resultado del ID del Vehiculo que ingresemos en la peticion.
        /// </remarks>
        /// <param name="id">(ID) Identificador del Vehiculo.</param>
        /// <response code="200">Registro del Vehiculo, encontrado correctamente.</response>
        /// <response code="404">NOT FOUND. No se ha podido encontrar el Vehiculo registrado en esta API.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCarsByID(int id)
        {
            var cars = await _context.Cars.FindAsync(id);
            if(cars==null)
            {
                return NotFound();
            }
            return cars;
        }
        
        /// <summary>
        /// Definicion del Segundo Verbo HTTP a utilizar (HttpPost) (Creando un nuevo objeto, un nuevo Vehiculo en la base de datos de la API).
        /// </summary>
        /// <remarks>
        /// Con este verbo HTTP crearemos un nuevo objeto osea un nuevo registro de un Vehiculo en nuestra base de Datos de la API.
        /// </remarks>
        /// <param name="car">Objeto a crear en los registros de la base de datos de la API.</param>             
        /// <response code="201">Created. Nuevo Vehiculo registrado correctamente en la Base de Datos de la API.</response>        
        /// <response code="400">BadRequest. No se ha podido registrar el vehiculo en la Base de Datos de la API. Formato del registro incorrecto.</response>
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCarsByID", new{id=car.idCar},car);
        }

        /// <summary>
        /// Definicion del Tercer Verbo HTTP a utilizar (HttpPut) (Actualizando un objeto o registro ya existente en la base de datos de la API, mediante el ID del Vehiculo).
        /// </summary>
        /// <remarks>
        /// Con este verbo HTTP actualizaremos un registro osea un objeto, que en este caso es un vehiculo ya existente en nuestra base de Datos de la API.
        /// El funcionamiento de este verbo es solicitar mediante un ID del vehiculo que querramos modificar, por lo cual debemos ingresar el identidicador de este, para que el verbo devuelva el objeto con los datos que se le habian asignado a este cuando se registro en la BD por primera vez.
        /// </remarks>
        /// <param name="id">(ID) Identificador del Vehiculo.</param>
        /// <param name="car">Objeto o Registro a Actualizar.</param>
        /// <response code="201">Update. Vehiculo actualizado correctamente en la Base de Datos de la API.</response>        
        /// <response code="400">BadRequest. No se ha podido actualizar el vehiculo en la Base de Datos de la API. Formato del registro incorrecto.</response>
        [HttpPut("{id}")]
        public async Task<ActionResult<Car>> PutCar(int id, Car car)
        {
            if(id!= car.idCar)
            {
                return BadRequest();
            }
            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetCarsByID", new{id=car.idCar},car);
        }
        /// <summary>
        /// Definicion del Cuarto Verbo HTTP a utilizar (HttpDelete) (Eliminando un objeto o registro ya existente en la base de datos de la API, mediante el ID del Vehiculo).
        /// </summary>
        /// <remarks>
        /// Con este verbo HTTP eliminaremos un registro osea un objeto, que en este caso es un vehiculo ya existente en nuestra base de Datos de la API.
        /// El funcionamiento de este verbo es solicitar mediante un ID del vehiculo que querramos eliminar, por lo cual debemos ingresar el identidicador de este, para que el verbo busque el objeto con los datos que se le habian asignado a este cuando se registro en la BD y los elimine de manera satisfactoria.
        /// </remarks>
        /// <param name="id">(ID) Identificador del Vehiculo.</param>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Car>> DeleteCar(int id)
        {
            var cars = await _context.Cars.FindAsync(id);
            if(cars==null)
            {
                return NotFound();
            }
            _context.Cars.Remove(cars);
            await _context.SaveChangesAsync();

            return cars;
            
        }

        /// <summary>
        /// Metodo Booleano para saber si existe o no un Vehiculo en los registros de la Base de Datos de la API, si existe devuelve una respuesta Verdadero o Falso (True or False).
        /// </summary>
        /// <param name="id">(ID) Identificador del Vehiculo.</param>
        /// <returns></returns>
        private bool CarExists(int id)
        {
            return _context.Cars.Any(c=>c.idCar==id);
        }
    }
}