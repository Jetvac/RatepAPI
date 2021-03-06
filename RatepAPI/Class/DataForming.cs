using RatepAPI.Models;
using System.Security.Cryptography;
using System.Text;

namespace RatepAPI.Class
{
    class DataForming
    {
        public static Employee GetEmployeeData(Employee employee)
        {
            VeloRaContext DBContext = new VeloRaContext();
            employee.PassportDatum = DBContext.PassportData.FirstOrDefault(c => c.Number == employee.Number && c.Seria == employee.Seria);
            employee.Post = DBContext.Posts.FirstOrDefault(c => c.PostId == employee.PostId);
            employee.Post.Role = DBContext.Roles.FirstOrDefault(c => c.RoleId == employee.Post.RoleId);
            employee.Manufactory = DBContext.Manufactories.FirstOrDefault(c => c.ManufactoryId == employee.ManufactoryId);
            employee.Manufactory.PartAssemblyUnits = null;
            employee.Manufactory.ManufactoryType = null;
            employee.Manufactory.Employees = null;

            return employee;
        }
    }
}
