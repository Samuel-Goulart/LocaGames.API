using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocaGames_API.Infra.DatabaseConfiguration
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
