using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace shoe_store.Data
{
        public static class Database
        {
            private const string ConnectionString =
            "Host=localhost;Port=5432;Database=shoe_store;Username=postgres;Password=postgres";
            public static NpgsqlConnection CreateConnection()
            {
                return new NpgsqlConnection(ConnectionString);
            }
        }
    }
