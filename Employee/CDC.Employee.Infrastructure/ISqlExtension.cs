using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CDC.Employee.Infrastructure
{
    public interface ISqlExtension
    {
        public Task<T> Get<T>(string query, object parameters = null, string mirrorConnectionString = "ConnectionStrings:CloudDBMirror", string connectionString = "ConnectionStrings:CloudDBMain");

        public Task<IEnumerable<T>> GetList<T>(string sqlQuery, object parameters = null, string mirrorConnectionString = "ConnectionStrings:CloudDBMirror", string connectionString = "ConnectionStrings:CloudDBMain");

        public Task<DataSet> ExecuteCommand(string connectionString, string commandName, SqlParameter[] parameters);

        public Task<SqlParameter[]> ExecuteNonQuery(string connectionString, string commandName, SqlParameter[] parameters);
    }
}
