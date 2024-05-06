using System.Data;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using T07.Models.DTOs;

namespace T07.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly IConfiguration _configuration;

    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> DoesProductExist(int id)
    {
        var query = "SELECT 1 FROM Product WHERE ID = @ID";

        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }
    
    public async Task<bool> DoesWarehouseExist(int id)
    {
        var query = "SELECT 1 FROM Warehouse WHERE ID = @ID";

        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public async Task<bool> DoesOrderExist(int idProduct, int amount)
    {
        var query = "SELECT 1 FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount";

        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@IdProduct", idProduct);
        command.Parameters.AddWithValue("@Amount", amount);

        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }

    public async Task<WarehouseDTO> GetProductByID(int id)
    {
        var query = @"SELECT IdProductWarehouse, IdProduct, IdWarehouse, Amount, CreatedAt 
                          FROM Product_Warehouse 
                          WHERE IdProductWarehouse = @ID;";

        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();

        command.Connection = connection;
        command.CommandText = query;
        command.Parameters.AddWithValue("@ID", id);

        await connection.OpenAsync();

        using SqlDataReader reader = await command.ExecuteReaderAsync();

        await reader.ReadAsync();

        if (!reader.HasRows) throw new Exception();

        var idProductWarehouseOrdinal = reader.GetOrdinal("IdProductWarehouse");
        var idProductOrdinal = reader.GetOrdinal("IdProduct");
        var idWarehouseOrdinal = reader.GetOrdinal("IdWarehouse");
        var amountOrdinal = reader.GetOrdinal("Amount");
        var createdAtOrdinal = reader.GetOrdinal("CreatedAt");

        var warehouseDTO = new WarehouseDTO()
        {
            IdProductWarehouse = reader.GetInt32(idProductWarehouseOrdinal),
            IdProduct = reader.GetInt32(idProductOrdinal),
            IdWarehouse = reader.GetInt32(idWarehouseOrdinal),
            Amount = reader.GetInt32(amountOrdinal),
            CreatedAt = reader.GetDateTime(createdAtOrdinal)
        };

        return warehouseDTO;
    }
}