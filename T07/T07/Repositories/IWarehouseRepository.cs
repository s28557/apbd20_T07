using T07.Models.DTOs;
namespace T07.Repositories;

public interface IWarehouseRepository
{
    Task<bool> DoesProductExist(int id);
    Task<bool> DoesWarehouseExist(int id);
    Task<bool> DoesOrderExist(int idProduct, int amount);
    Task<WarehouseDTO> GetProductByID(int id);
}