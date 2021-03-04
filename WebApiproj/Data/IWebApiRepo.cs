using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiproj.Models;

namespace WebApiproj.Data
{
    public interface  IWebApiRepo
    {
        bool SaveChanges();
        WebApiModel GetCommandById(int Id);

        IEnumerable<WebApiModel> GetAllCommands();

        void CreateCommand(WebApiModel cmd);
    }
}
