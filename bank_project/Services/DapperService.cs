namespace bank_project.Services
{
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;

    public class DapperService
    {
        private readonly string _connectionString;

        public DapperService(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task ExecuteStoredProcedure(string spName, object parameters)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<T>> QueryStoredProcedure<T>(string spName, object parameters)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                return await conn.QueryAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
