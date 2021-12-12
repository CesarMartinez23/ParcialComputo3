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
    }
}