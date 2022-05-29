#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RatepAPI.Class;
using RatepAPI.Models;
using System.Security.Claims;

namespace RatepAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderPositionsController : ControllerBase
    {

        [HttpPost("OpenOrderPosition")]
        [Authorize]
        public ActionResult<string> OpenOrderPosition(int _orderId, int _posId, string _pauId, string _pauidcontains)
        {
            VeloRaContext DBContext = new VeloRaContext();
            RoadMap roadMap = DBContext.RoadMaps.FirstOrDefault(c => c.OrderId == _orderId
                    && c.PosId == _posId
                    && c.Pauid == _pauId
                    && c.Pauidcontains == _pauidcontains);
            if (roadMap.RoadMapStatusId == 3)
            {
                roadMap.CompletionDate = null;
                roadMap.RoadMapStatusId = 2;
            }
            if (roadMap.RoadMapStatusId == 2)
                roadMap.RoadMapStatusId = 1;
            try
            {
                DBContext.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("ChangeOrderState")]
        [Authorize(Roles="Enginer")]
        public IActionResult ChangeOrderState(int _orderId)
        {
            VeloRaContext DBContext = new VeloRaContext();
            Order order = DBContext.Orders.FirstOrDefault(c => c.OrderId == _orderId);
            switch(order.OrderStatusId)
            {
                case 0:
                    order.OrderStatusId = 1001;
                    break;
                case 2:
                    order.OrderStatusId = 4;
                    order.CompletionDate = DateTime.Now;
                    break;
            }

            try
            {
                DBContext.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok(order.OrderStatusId);
        }

        [HttpPost("CloseOrderPosition")]
        [Authorize]
        public IActionResult CloseOrderPosition(int _orderId, int _posId, string _pauId, string _pauidcontains)
        {
            VeloRaContext DBContext = new VeloRaContext();
            RoadMap roadMap = DBContext.RoadMaps.FirstOrDefault(c => c.OrderId == _orderId
                    && c.PosId == _posId
                    && c.Pauid == _pauId
                    && c.Pauidcontains == _pauidcontains);
            Order order = DBContext.Orders.FirstOrDefault(c => c.OrderId == roadMap.OrderId);
            if (roadMap.RoadMapStatusId == 1)
                roadMap.RoadMapStatusId = 2;
            else if (roadMap.RoadMapStatusId == 2)
            {
                roadMap.CompletionDate = DateTime.Now;
                roadMap.RoadMapStatusId = 3;
                int OrderPositionAmmount = DBContext.RoadMaps.Where(c => c.RoadMapStatusId != 3 && c.OrderId == order.OrderId).Count() - 1;
                if (OrderPositionAmmount == 0)
                {
                    order.OrderStatusId = 2;
                }
            }
            try
            {
                DBContext.SaveChanges();
            } catch(Exception)
            {
                return BadRequest();
            }
            return Ok(order.OrderStatusId);
        }

        [HttpGet("GetClientsOrderList")]
        [Authorize]
        public IActionResult GetClientsOrderList()
        {
            VeloRaContext DBContext = new VeloRaContext();
            try
            {
                List<Client> clientsOrder = DBContext.Clients.ToList();
                Employee employee = GetCurrentUser();
                

                foreach (Client client in clientsOrder)
                {
                    client.Orders = DBContext.Orders.Where(c => c.ClientId == client.ClientId).ToList();
                    client.LegalPeople = DBContext.LegalPeople.Where(c => c.ClientId == client.ClientId).ToList();
                    client.LegalPeople.ToList().ForEach(c => { c.Client = null; });
                    client.NaturalPeople = DBContext.NaturalPeople.Where(c => c.ClientId == client.ClientId).ToList();
                    client.NaturalPeople.ToList().ForEach(c => {
                        c.Client = null;
                        c.Gender = DBContext.Genders.FirstOrDefault(d => d.GenderId == c.GenderId);
                        if (c.Gender != null)
                        {
                            c.Gender.NaturalPeople = null;
                            c.Gender.PassportData = null;
                        }
                    });

                    foreach (Order order in client.Orders)
                    {
                        order.Employee = GetEmployee(order.EmployeeId);
                        order.RoadMaps = null;
                        order.Client = null;
                        order.OrderStatus = DBContext.OrderStatuses.FirstOrDefault(c => c.OrderStatusId == order.OrderStatusId);
                        order.OrderStatus.Orders = null;
                        order.OrderPositions = DBContext.OrderPositions.Where(c => c.OrderId == order.OrderId).ToList();
                        order.RoadMaps = DBContext.RoadMaps.Where(c => c.OrderId == order.OrderId).ToList();
                        order.Vat = DBContext.Vats.FirstOrDefault(c => c.Vatid == order.Vatid);
                        order.Vat.Orders = null;


                        foreach (OrderPosition position in order.OrderPositions)
                        {
                            position.ArticulNavigation = DBContext.PartAssemblyUnits.FirstOrDefault(c => c.Articul == position.Articul);
                            position.ArticulNavigation.Manufactory = DBContext.Manufactories.FirstOrDefault(c => c.ManufactoryId == position.ArticulNavigation.ManufactoryId);
                            position.ArticulNavigation.Manufactory.PartAssemblyUnits = null;
                            position.Order = null;
                            position.ArticulNavigation.OrderPositions = null;
                            position.RoadMaps = null;
                        }

                        foreach (RoadMap roadMap in order.RoadMaps)
                        {

                            roadMap.Order = null;
                            roadMap.Employee = roadMap.EmployeeId.Equals(null)? null : GetEmployee(Convert.ToInt32(roadMap.EmployeeId));
                            roadMap.PauidcontainsNavigation = DBContext.PartAssemblyUnits.FirstOrDefault(c => c.Articul == roadMap.Pauidcontains);
                            roadMap.PauidcontainsNavigation.Cost = DBContext.CostJournals.FirstOrDefault(c => c.CostId == roadMap.PauidcontainsNavigation.CostId);
                            roadMap.PauidcontainsNavigation.Cost.PartAssemblyUnits = null;
                            roadMap.PauidcontainsNavigation.OrderPositions = null;
                            roadMap.PauidcontainsNavigation.RoadMapPauidcontainsNavigations = null;
                            roadMap.PauidcontainsNavigation.RoadMapPaus = null;
                            roadMap.PauidcontainsNavigation.StructurePauidcontainsNavigations = null;
                            roadMap.PauidcontainsNavigation.StructurePaus = null;
                            roadMap.PauidcontainsNavigation.Manufactory = DBContext.Manufactories.FirstOrDefault(c => c.ManufactoryId == roadMap.PauidcontainsNavigation.ManufactoryId);
                            roadMap.PauidcontainsNavigation.Manufactory.Employees = null;
                            roadMap.PauidcontainsNavigation.Manufactory.ManufactoryType = DBContext.ManufactoryTypes.FirstOrDefault(c => c.ManufactoryTypeId == roadMap.PauidcontainsNavigation.Manufactory.ManufactoryTypeId);
                            roadMap.PauidcontainsNavigation.Manufactory.ManufactoryType.Manufactories = null;
                            roadMap.PauidcontainsNavigation.Manufactory.PartAssemblyUnits = null;

                            roadMap.Pau = DBContext.PartAssemblyUnits.FirstOrDefault(c => c.Articul == roadMap.Pauid);
                            roadMap.Pau.Cost = DBContext.CostJournals.FirstOrDefault(c => c.CostId == roadMap.Pau.CostId);
                            roadMap.Pau.Cost.PartAssemblyUnits = null;
                            roadMap.Pau.OrderPositions = null;
                            roadMap.Pau.RoadMapPauidcontainsNavigations = null;
                            roadMap.Pau.RoadMapPaus = null;
                            roadMap.Pau.StructurePauidcontainsNavigations = null;
                            roadMap.Pau.StructurePaus = null;
                            roadMap.Pau.Manufactory = DBContext.Manufactories.FirstOrDefault(c => c.ManufactoryId == roadMap.Pau.ManufactoryId);
                            roadMap.Pau.Manufactory.Employees = null;
                            roadMap.Pau.Manufactory.ManufactoryType = DBContext.ManufactoryTypes.FirstOrDefault(c => c.ManufactoryTypeId == roadMap.Pau.Manufactory.ManufactoryTypeId);
                            roadMap.Pau.Manufactory.ManufactoryType.Manufactories = null;
                            roadMap.Pau.Manufactory.PartAssemblyUnits = null;


                            roadMap.Pos = DBContext.OrderPositions.FirstOrDefault(c => c.PosId == roadMap.PosId);
                            roadMap.Pos.ArticulNavigation = DBContext.PartAssemblyUnits.FirstOrDefault(c => c.Articul == roadMap.Pos.Articul);
                            roadMap.Pos.ArticulNavigation.Manufactory = DBContext.Manufactories.FirstOrDefault(c => c.ManufactoryId == roadMap.Pos.ArticulNavigation.ManufactoryId);
                            roadMap.Pos.ArticulNavigation.Manufactory.PartAssemblyUnits = null;
                            roadMap.Pos.Order = null;
                            roadMap.Pos.ArticulNavigation.OrderPositions = null;
                            roadMap.Pos.RoadMaps = null;

                            roadMap.RoadMapStatus = DBContext.RoadMapStatuses.FirstOrDefault(c => c.RoadMapStatusId == roadMap.RoadMapStatusId);
                            roadMap.RoadMapStatus.RoadMaps = null;
                        }
                    }
                }

                return Ok(clientsOrder);
            }
            catch (Exception ex)
            {
                return NotFound(404);
            }
        }

        [HttpGet("GetRoadMapStatuses")]
        public IActionResult GetRoadMapStatuses()
        {
            VeloRaContext DBContext = new VeloRaContext();
            List<RoadMapStatus> result = DBContext.RoadMapStatuses.ToList();
            result.ForEach(c => { c.RoadMaps = null; });
            return Ok(result);
        }

        [HttpGet("GetOrderStatuses")]
        public IActionResult GetOrderStatuses()
        {
            VeloRaContext DBContext = new VeloRaContext();
            List<OrderStatus> result = DBContext.OrderStatuses.ToList();
            result.ForEach(c => { c.Orders = null; });
            return Ok(result);
        }

        [HttpGet("GetEmployeeData")]
        [Authorize]
        public IActionResult GetEmployeeData()
        {
            Employee employee = GetEmployee(GetCurrentUser().EmployeeId);
            return Ok(employee);
        }

        #region Master methods
        [HttpGet("GetEmployeeListByManufactory")]
        [Authorize(Roles = "Master")]
        public IActionResult GetEmployeeListByManufactory()
        {
            VeloRaContext DBContext = new VeloRaContext();
            Employee currentUser = GetEmployee(GetCurrentUser().EmployeeId);
            List<Employee> employeeList = new List<Employee>();

            foreach (Employee employee in DBContext.Employees.Where(c => c.ManufactoryId == currentUser.ManufactoryId && c.PostId == 1001))
            {
                employeeList.Add(GetEmployee(employee.EmployeeId));
            }

            return Ok(employeeList);
        }

        [HttpPost("SetResponsibleEmployee")]
        [Authorize(Roles = "Master")]
        public IActionResult SetResponsibleEmployee(int _orderId, int _posId, string _pauId, string _pauidcontains, int _employeeId)
        {
            VeloRaContext DBContext = new VeloRaContext();
            try
            {
                Employee responsibleEmployee = DBContext.Employees.FirstOrDefault(c => c.EmployeeId == _employeeId);
                RoadMap roadMap = DBContext.RoadMaps.FirstOrDefault(c => c.OrderId == _orderId
                    && c.PosId == _posId
                    && c.Pauid == _pauId
                    && c.Pauidcontains == _pauidcontains);

                roadMap.EmployeeId = responsibleEmployee.EmployeeId;
                roadMap.RoadMapStatusId = 1;
                DBContext.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost("ClearResponsibleEmployee")]
        [Authorize(Roles = "Master")]
        public IActionResult ClearResponsibleEmployee(int _orderId, int _posId, string _pauId, string _pauidcontains)
        {
            VeloRaContext DBContext = new VeloRaContext();
            try
            {
                RoadMap roadMap = DBContext.RoadMaps.FirstOrDefault(c => c.OrderId == _orderId
                    && c.PosId == _posId
                    && c.Pauid == _pauId
                    && c.Pauidcontains == _pauidcontains);

                roadMap.EmployeeId = null;
                roadMap.RoadMapStatusId = 0;
                DBContext.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }
        #endregion

        private Employee GetCurrentUser()
        {
            VeloRaContext DBContext = new VeloRaContext();
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                string serialNumber = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.SerialNumber)?.Value;
                Employee result = DBContext.Employees.ToList().FirstOrDefault(c => c.SerialNumber == serialNumber);
                result = DataForming.GetEmployeeData(result);

                return result;
            }
            return null;
        }

        private Employee GetEmployee(int EmployeeId)
        {
            VeloRaContext DBContext = new VeloRaContext();
            Employee employee = DBContext.Employees.FirstOrDefault(c => c.EmployeeId == EmployeeId);
            employee.Account = null;
            employee.Manufactory = DBContext.Manufactories.FirstOrDefault(c => c.ManufactoryId == employee.ManufactoryId);
            employee.Manufactory.PartAssemblyUnits = null;
            employee.Manufactory.ManufactoryType = null;
            employee.Manufactory.Employees = null;
            employee.PassportDatum = DBContext.PassportData.FirstOrDefault(c => c.Seria == employee.Seria && c.Number == employee.Number);
            employee.PassportDatum.Employees = null;
            employee.Post = DBContext.Posts.FirstOrDefault(c => c.PostId == employee.PostId);
            employee.Post.Employees = null;
            employee.Post.Role = DBContext.Roles.FirstOrDefault(c => c.RoleId == employee.Post.RoleId);
            if (employee.Post.Role != null)
                employee.Post.Role.Posts = null;
            employee.Orders = null;
            employee.RoadMaps = null;
            return employee;
        }
    }
}
