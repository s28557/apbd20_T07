namespace T07.Models.DTOs;

public class WarehouseDTO
{
    public int IdProduct { get; set; }
    public int IdWarehouse { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public int IdProductWarehouse { get; set; }
}