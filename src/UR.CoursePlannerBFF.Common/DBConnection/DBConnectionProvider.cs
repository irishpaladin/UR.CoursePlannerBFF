using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace UR.CoursePlannerBFF.Common.DBConnection
{
    public interface IDBConnectionProvider
    {
        public IDbConnection GetConnection();
    }
    public class DBConnectionProvider: IDBConnectionProvider
    {
        private readonly IOptions<DBConnectionOptions> _configuration;
        private IDbConnection _connection;


        public DBConnectionProvider(IOptions<DBConnectionOptions> configuration)
        {
            _configuration= configuration;
        }
        public IDbConnection GetConnection()
        {
            return _connection ?? (_connection = CreateConnection());
        }
        private IDbConnection CreateConnection()
        {
            var dbConnection = new SqlConnection() as IDbConnection;
            dbConnection.ConnectionString = _configuration.Value.ConnectionStrings;
            return dbConnection;
        }
    }
}