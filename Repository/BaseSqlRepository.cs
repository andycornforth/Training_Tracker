using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;

namespace Repository
{
    public class BaseSqlRepository
    {
        private string _connectionString = new ConnectionStringProvider().GetConnectionString();
        private const short MaxDeadlockRetryAttempts = 2;

        protected IDbCommand GetCommand(string commandText, CommandType commandType)
        {
            var connection = new SqlConnection(_connectionString);
            return new SqlCommand(commandText, connection) { CommandType = commandType };
        }

        protected void AddParameter(IDbCommand command, string name, object value)
        {
            command.Parameters.Add(new SqlParameter(name, value));
        }

        protected void AddOutputParameter(IDbCommand command, string name, DbType dbType, int paramSize)
        {
            var parameter = new SqlParameter();
            parameter.ParameterName = name;
            parameter.DbType = dbType;
            parameter.Direction = ParameterDirection.Output;
            parameter.Value = DBNull.Value;
            if (paramSize != 0)
            {
                parameter.Size = paramSize;
            }
            command.Parameters.Add(parameter);            
        }

        protected IList<T> GetEntitiesFromDatabase<T>(IDbCommand command)
        {
            return GetEntitiesFromDatabase<T>(command, 0);
        }

        protected IList<T> GetEntitiesFromDatabase<T>(IDbCommand command, int attempt)
        {
            attempt++;
            try
            {
                command.Connection.Open();

                return RunReadEntitiesCommand<T>(command);
            }
            catch (SqlException ex)
            {
                CloseConnection(command);
                if (attempt < MaxDeadlockRetryAttempts)
                {
                    Thread.Sleep(500);
                    return GetEntitiesFromDatabase<T>(command, attempt);
                }
                else
                {
                    throw new Exception("Repository query execution error.", ex);
                }
            }
            finally
            {
                CloseConnection(command);
            }
        }


        private IList<T> RunReadEntitiesCommand<T>(IDbCommand command)
        {
            IList<T> results = new List<T>();
            using (var reader = command.ExecuteReader(CommandBehavior.Default))
            {
                while (reader.Read())
                {
                    results.Add((T)MapRowToEntity(reader));
                }
            }
            return results;
        }

        protected void ExecuteNonQueryChecked(IDbCommand command)
        {
            int queryStatus = 0;
            try
            {
                command.Connection.Open();
                queryStatus = command.ExecuteNonQuery();
                if (queryStatus == 0)
                {
                    throw new Exception("The entity does not exist or it is obsolete.");
                }
            }
            finally
            {
                CloseConnection(command);
            }
        }

        protected void ExecuteNonQuery(IDbCommand command)
        {
            try
            {
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection(command);
            }
        }

        protected int ExecuteInt32Scalar(IDbCommand command)
        {
            try
            {
                command.Connection.Open();
                return (int) command.ExecuteScalar();
            }
            catch (SqlException e)
            {
                throw new Exception("Repository query execution error.", e);
            }
            finally
            {
                CloseConnection(command);
            }
        }

        protected long ExecuteInt64Scalar(IDbCommand command)
        {
            try
            {
                command.Connection.Open();
                return (long)command.ExecuteScalar();
            }
            finally
            {
                CloseConnection(command);
            }
        }

        [ExcludeFromCodeCoverage]
        protected virtual object MapRowToEntity(IDataReader reader)
        {
            throw new InvalidOperationException("MapRowToEntity must be overridden in the specialised repository.");
        }

        public static void CloseConnection(IDbCommand command)
        {
            if (command.Connection != null && command.Connection.State != ConnectionState.Closed)
            {
                command.Connection.Close();
            }
        }
    }
}
