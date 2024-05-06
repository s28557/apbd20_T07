using T07.Repositories;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;


namespace T07.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseRepository _warehouseRepository;
    public WarehouseController(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    [HttpGet]
    [Route("{productID}")]
    public async Task<IActionResult> GetProduct(int productID)
    {
        if (!await _warehouseRepository.DoesProductExist(productID))
            return NotFound();
        var product = await _warehouseRepository.DoesProductExist(productID);
        return Ok(product);
    }
}

