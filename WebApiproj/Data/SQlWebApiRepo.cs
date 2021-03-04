using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiproj.Models;

namespace WebApiproj.Data
{
    public class SQlWebApiRepo : IWebApiRepo
    {
        private readonly WebApiContext _context;
        public SQlWebApiRepo(WebApiContext context)  //Dependency injection used , using context class to get data from database
        {
            _context = context;
        }

        public void CreateCommand(WebApiModel cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.WebApiTable.Add(cmd);

        }

        public IEnumerable<WebApiModel> GetAllCommands()
        {
            return _context.WebApiTable.ToList();
        }

        public WebApiModel GetCommandById(int Id)
        {
            return _context.WebApiTable.FirstOrDefault(p => p.Id == Id);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
