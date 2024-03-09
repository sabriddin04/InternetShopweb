using System.Xml;
using Npgsql;

namespace Infrastructure.DataContext;

public class DapperContext
{

  private readonly string _connection="Host=localhost;Port=5432;Database=WebInternetShop;User Id=postgres;Password=sabriddin2004;";


 public NpgsqlConnection Connection()
 {
    return new NpgsqlConnection(_connection);
 }


}
