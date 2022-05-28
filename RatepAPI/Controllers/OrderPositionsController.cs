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

        //[HttpPost("OpenOrderPosition")]
        //public ActionResult<string> OpenOrderPosition(int OrderPositionID, string Token)
        //{
        //    User user;
        //    Employee employee;
        //    OrderPosition orderPosition;
        //    Order order;
        //    PartAssemblyUnit pau;

        //    try
        //    {
        //        user = UsersController.DBContext.Users.FirstOrDefault(c => c.Token == Token);
        //        employee = UsersController.DBContext.Employees.FirstOrDefault(c => c.AccountId == user.AccountId);
        //        orderPosition = UsersController.DBContext.OrderPositions.FirstOrDefault(c => c.PosId == OrderPositionID);
        //        order = UsersController.DBContext.Orders.FirstOrDefault(c => c.OrderPositions.Contains(orderPosition));
        //        pau = UsersController.DBContext.PartAssemblyUnits.FirstOrDefault(c => c.OrderPositions.Where(c => c.PosId == OrderPositionID).Any());
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(505);
        //    }

        //    if (orderPosition == null || user == null)
        //        return NotFound(505);
        //    if (employee.ManufactoryId != pau.ManufactoryId)
        //    {
        //        return BadRequest(504);
        //    }

        //    try
        //    {
        //        orderPosition.CompletionDate = null;
        //        order.CompletionDate = null;
        //        UsersController.DBContext.SaveChanges();
        //    }
        //    catch
        //    {
        //        return NotFound(404);
        //    }
        //    return "1";
        //}

        //[HttpPost("CloseOrderPosition")]
        //public ActionResult<string> CloseOrderPosition(int OrderPositionID, string Token)
        //{
        //    User user;
        //    Employee employee;
        //    OrderPosition orderPosition;
        //    Order order;
        //    PartAssemblyUnit pau;

        //    try
        //    {
        //        user = UsersController.DBContext.Users.FirstOrDefault(c => c.Token == Token);
        //        employee = UsersController.DBContext.Employees.FirstOrDefault(c => c.AccountId == user.AccountId);
        //        orderPosition = UsersController.DBContext.OrderPositions.FirstOrDefault(c => c.PosId == OrderPositionID);
        //        order = UsersController.DBContext.Orders.FirstOrDefault(c => c.OrderPositions.Contains(orderPosition));
        //        pau = UsersController.DBContext.PartAssemblyUnits.FirstOrDefault(c => c.OrderPositions.Where(c => c.PosId == OrderPositionID).Any());
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(505);
        //    }


        //    if (orderPosition == null || user == null)
        //        return NotFound(505);
        //    if (employee.ManufactoryId != pau.ManufactoryId)
        //    {
        //        return BadRequest(504);
        //    }

        //    try
        //    {
        //        orderPosition.CompletionDate = DateTime.Now;
        //        if (UsersController.DBContext.OrderPositions.Where(c => c.OrderId == order.OrderId).Select(c => c.CompletionDate == null).Any())
        //            order.CompletionDate = DateTime.Now;
        //        UsersController.DBContext.SaveChanges();
        //    }
        //    catch
        //    {
        //        return NotFound(404);
        //    }
        //    return Ok(1);
        //}

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
                        order.Employee = DBContext.Employees.FirstOrDefault(c => c.EmployeeId == order.EmployeeId);
                        order.Employee.Manufactory = DBContext.Manufactories.FirstOrDefault(c => c.ManufactoryId == order.Employee.ManufactoryId);
                        order.Employee.Manufactory.Employees = null;
                        order.Employee.Manufactory.PartAssemblyUnits = null;
                        order.Employee.PassportDatum = null;
                        order.Employee.Post = null;
                        order.Employee.Orders = null;
                        order.Employee.Account = null;
                        order.RoadMaps = null;
                        order.Client = null;
                        order.OrderStatus = DBContext.OrderStatuses.FirstOrDefault(c => c.OrderStatusId == order.OrderStatusId);
                        order.OrderStatus.Orders = null;
                        order.OrderPositions = DBContext.OrderPositions.Where(c => c.OrderId == order.OrderId && c.ArticulNavigation.Manufactory.Name == employee.Manufactory.Name).ToList();
                        order.RoadMaps = DBContext.RoadMaps.Where(c => c.OrderId == order.OrderId && c.PauidcontainsNavigation.Manufactory.Name == employee.Manufactory.Name).ToList();


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

                roadMap.Employee = responsibleEmployee;
                roadMap.RoadMapStatus = DBContext.RoadMapStatuses.FirstOrDefault(c => c.RoadMapStatusId == 1);
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

                DBContext.RoadMaps.Update(roadMap.Employee);
                DBContext.Entry(roadMap).CurrentValues.SetValues(em);
                roadMap.RoadMapStatus = DBContext.RoadMapStatuses.FirstOrDefault(c => c.RoadMapStatusId == 0);
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
