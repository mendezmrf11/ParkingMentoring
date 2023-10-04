using CupiParqueadero.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingMentoring.Models;

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
        [Authorize]
        [Route("Get"), Authorize(Roles = "Admin, Regular")]
        public async Task<IActionResult> Get()
        {
            List<Vehicle> lista = new List<Vehicle>();

            try
            {
                lista = _context.Vehicles.Where(vehicle => vehicle.IsPayable == false).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetActive"), Authorize(Roles = "Admin, Regular")]
        public async Task<IActionResult> GetActive()
        {
            List<Vehicle> lista = new List<Vehicle>();

            try
            {
                lista = _context.Vehicles.Where(vehicle => vehicle.IsPayable == true).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Authorize]
        [Route("Calculate/{startTime:datetime}/{endTime:datetime}/{id:int}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> Payment(DateTime startTime, DateTime endTime, int id)
        {
            var calculator = new Vehicle("", "", "");
            double payment = calculator.PaymentParking(startTime, endTime);
            HideVehiculo(id, payment);
            return StatusCode(StatusCodes.Status200OK, new { valuePay = payment});
        }

        [HttpGet]
        [Route("GetById/{idVehicle:int}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetId(int idVehicle)
        {
            Vehicle oVehicle = new Vehicle("", "", "");
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
        [Authorize]
        [Route("Save"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddVehiculo(VehicleDto vehicle)
        {
            try
            {
                Vehicle oVehicle = _context.Vehicles.FirstOrDefault(v => v.Plate == vehicle.Plate && v.IsPayable == false);
                if (oVehicle != null)
                    return BadRequest("Vehicle already exists");

                Vehicle vehicle1 = new Vehicle(vehicle.Plate, vehicle.Make, vehicle.Color);
                _context.Vehicles.Add(vehicle1);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Vehiculo agregado con exito" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = e.Message });
            }
        }

        [HttpPut]
        [Route("Edit"), Authorize(Roles = "Admin")]
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
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Vehiculo agregado con exito" });
            }

            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = e.Message });
            }
        }

        [HttpPut]
        [Route("Hide/{id:int}/{value:double}"), Authorize(Roles = "Admin")]
        public IActionResult HideVehiculo(int id, double value)
        {
            Vehicle oVehicle = _context.Vehicles.Find(id);
            if (oVehicle == null)
            {
                return BadRequest("Vehicle not found");
            }

            try
            {
                oVehicle.Pay = value;
                oVehicle.IsPayable = true;
                oVehicle.PayDate = DateTime.Now;

                _context.Vehicles.Update(oVehicle);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Vehiculo escondido con exito" });
            }

            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = e.Message });
            }
        }

        [HttpDelete]
        [Route("Delete/{idVehicle:int}"), Authorize(Roles = "Admin")]
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
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Vehiculo eliminado con exito" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = e.Message });
            }
        }
    }
}
