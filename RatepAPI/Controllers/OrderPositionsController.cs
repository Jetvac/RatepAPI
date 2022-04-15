#nullable disable
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RatepAPI.Models;

namespace RatepAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderPositionsController : ControllerBase
    {

        [HttpPost ("OpenOrderPosition")]
        public ActionResult<string> OpenOrderPosition(int OrderPositionID, string Token)
        {
            User user;
            Employee employee;
            OrderPosition orderPosition;
            Order order;
            PartAssemblyUnit pau;

            try
            {
                user = UsersController.DBContext.Users.FirstOrDefault(c => c.Token == Token);
                employee = UsersController.DBContext.Employees.FirstOrDefault(c => c.AccountId == user.AccountId);
                orderPosition = UsersController.DBContext.OrderPositions.FirstOrDefault(c => c.PosId == OrderPositionID);
                order = UsersController.DBContext.Orders.FirstOrDefault(c => c.OrderPositions.Contains(orderPosition));
                pau = UsersController.DBContext.PartAssemblyUnits.FirstOrDefault(c => c.OrderPositions.Where(c => c.PosId == OrderPositionID).Any());
            }
            catch (Exception ex)
            {
                return NotFound(505);
            }

            if (orderPosition == null || user == null)
                return NotFound(505);
            if (employee.ManufactoryId != pau.ManufactoryId)
            { 
                return BadRequest(504);
            }

            try
            {
                orderPosition.CompletionDate = null;
                order.CompletionDate = null;
                UsersController.DBContext.SaveChanges();
            } catch
            {
                return NotFound(404);
            }
            return "1";
        }

        [HttpPost("CloseOrderPosition")]
        public ActionResult<string> CloseOrderPosition(int OrderPositionID, string Token)
        {
            User user;
            Employee employee;
            OrderPosition orderPosition;
            Order order;
            PartAssemblyUnit pau;

            try
            {
                user = UsersController.DBContext.Users.FirstOrDefault(c => c.Token == Token);
                employee = UsersController.DBContext.Employees.FirstOrDefault(c => c.AccountId == user.AccountId);
                orderPosition = UsersController.DBContext.OrderPositions.FirstOrDefault(c => c.PosId == OrderPositionID);
                order = UsersController.DBContext.Orders.FirstOrDefault(c => c.OrderPositions.Contains(orderPosition));
                pau = UsersController.DBContext.PartAssemblyUnits.FirstOrDefault(c => c.OrderPositions.Where(c => c.PosId == OrderPositionID).Any());
            }
            catch (Exception ex)
            {
                return NotFound(505);
            }


            if (orderPosition == null || user == null)
                return NotFound(505);
            if (employee.ManufactoryId != pau.ManufactoryId)
            {
                return BadRequest(504);
            }

            try
            {
                orderPosition.CompletionDate = DateTime.Now;
                if (UsersController.DBContext.OrderPositions.Where(c => c.OrderId == order.OrderId).Select(c => c.CompletionDate == null).Any())
                    order.CompletionDate = DateTime.Now;
                UsersController.DBContext.SaveChanges();
            }
            catch
            {
                return NotFound(404);
            }
            return Ok(1);
        }

        [HttpGet("GetClientsOrderList")]
        public ActionResult<IEnumerable<Client>> GetClientsOrderList(string Token)
        {
            try
            {
                List<Client> clientsOrder = UsersController.DBContext.Clients.ToList();

                foreach (Client client in clientsOrder)
                {
                    client.Account = UsersController.DBContext.Users.FirstOrDefault(c => c.AccountId == client.AccountId);
                    client.Account.Clients = null;
                    client.Contracts = UsersController.DBContext.Contracts.Where(c => c.ClientId == client.ClientId).ToList();
                    foreach (Contract contract in client.Contracts)
                    {
                        contract.Orders = UsersController.DBContext.Orders.Where(c => c.ContractId == contract.ContractId).ToList();
                        contract.Client = null;
                        contract.Orders.First().Contract = null;


                        contract.Orders.First().OrderPositions = UsersController.DBContext.OrderPositions.Where(c => c.OrderId == contract.Orders.First().OrderId).ToList();

                        foreach (OrderPosition position in contract.Orders.First().OrderPositions)
                        {
                            position.Pau = UsersController.DBContext.PartAssemblyUnits.FirstOrDefault(c => c.Pauid == position.Pauid);
                            position.Pau.Manufactory = UsersController.DBContext.Manufactories.FirstOrDefault(c => c.ManufactoryId == position.Pau.ManufactoryId);
                            position.Pau.Manufactory.PartAssemblyUnits = null;
                            position.Pau.Manufactory.Employees = null;
                            position.Order = null;
                            position.Pau.OrderPositions = null;
                        }
                    }
                }

                UsersController.DBContext = new VeloRaContext();
                return clientsOrder;
            }
            catch (Exception ex)
            {
                return NotFound(404);
            }
        }
    }
}
