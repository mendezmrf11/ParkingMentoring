using CupiParqueadero.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ParkingMentoring.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly DataContext _context;

        public VehiculoController(DataContext context) 
        {
            this._context = context;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> Get()
        {
            List<Vehicle> lista = new List<Vehicle>();

            try
            {
                lista = _context.Vehicles.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Route("List/{startTime:datetime}/{endTime:datetime}")]
        public async Task<IActionResult> Payment(DateTime startTime, DateTime endTime)
        {
            var calculator = new Vehicle();
            double payment = calculator.PaymentParking(startTime, endTime);
            return StatusCode(StatusCodes.Status200OK, new { valuePay = payment});
        }

        [HttpGet]
        [Route("Get/{idVehicle:int}")]
        public async Task<IActionResult> GetId(int idVehicle)
        {
            Vehicle oVehicle = new Vehicle();
            oVehicle = _context.Vehicles.Find(idVehicle);

            if(oVehicle == null)
            {
                return BadRequest("Vehicle not found");
            }

            try
            {
                oVehicle = _context.Vehicles.Where(p => p.Id == idVehicle).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = oVehicle });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = oVehicle });
            }
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> AddVehiculo(Vehicle vehicle)
        {
            try
            {
                _context.Vehicles.Add(vehicle);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Vehiculo agregado con exito" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = e.Message });
            }
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult EditVehiculo([FromBody] Vehicle objeto)
        {
            Vehicle oVehicle = _context.Vehicles.Find(objeto.Id);
            if (oVehicle == null)
            {
                return BadRequest("Vehicle not found");
            }

            try
            {
                oVehicle.Plate = objeto.Plate is null ? oVehicle.Plate : objeto.Plate;
                oVehicle.EntryDate = oVehicle.EntryDate;
                oVehicle.Color = objeto.Color is null ? oVehicle.Color : objeto.Color;
                oVehicle.Make = objeto.Make is null ? oVehicle.Make : objeto.Make;

                _context.Vehicles.Update(oVehicle);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
            }

            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = e.Message });
            }
        }

        [HttpDelete]
        [Route("Delete/{idVehicle:int}")]
        public async Task<IActionResult> Delete(int idVehicle)
        {
            Vehicle oVehicle = _context.Vehicles.Find(idVehicle);

            if (oVehicle == null)
            {
                return BadRequest("Vehicle not found");
            }

            try
            {
                _context.Vehicles.Remove(oVehicle);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = e.Message });
            }
        }
    }
}
