using ContosoPizza.Models;
using ContosoPizza.Service;
using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Data;
using Microsoft.EntityFrameworkCore;
namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{   
    private readonly ApplicationDbContext _dbContext;
    public PizzaController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // GET all action
    [HttpGet]
    public async Task<ActionResult<List<Pizza>>> GetAll()
    {
        return (await _dbContext.Pizzas.ToListAsync());
    }

    // GET by Id action
    [HttpGet("{id}")]
    public async Task<ActionResult<Pizza>> Get(int id)
    {
        var pizza = await _dbContext.Pizzas.FindAsync(id);

        if(pizza == null)
            return NotFound();

        return pizza;
    }

    // POST action
    [HttpPost]
public async Task<ActionResult<List<Pizza>>> Create(Pizza pizza)
{    
    
    //Console.WriteLine(temp_pizzas);

    _dbContext.Pizzas.Add(pizza);
    await _dbContext.SaveChangesAsync();
    var temp_pizzas = await _dbContext.Pizzas.ToListAsync();
    foreach (var temp_pizza in temp_pizzas){
        Console.WriteLine(temp_pizza.Name);
    }
    return await _dbContext.Pizzas.ToListAsync();
}

    // PUT action
    [HttpPut("{id}")]
    public async Task<ActionResult<List<Pizza>>> Update(Pizza request)
    {
        // if (id != pizza.Id)
        //     return BadRequest();
            
        var existingPizza = await _dbContext.Pizzas.FindAsync(request.Id);
        if(existingPizza is null)
            return NotFound();
    
        existingPizza.Id = request.Id;
        existingPizza.Name = request.Name;
        existingPizza.IsGlutenFree = request.IsGlutenFree;          
    
    await _dbContext.SaveChangesAsync();
        return await _dbContext.Pizzas.ToListAsync();
    }


    // DELETE action
    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Pizza>>> Delete(int id)
    {
        var existing_pizza = await _dbContext.Pizzas.FindAsync(id);
    
        if (existing_pizza is null)
            return NotFound();
        
        _dbContext.Pizzas.Remove(existing_pizza);
        await _dbContext.SaveChangesAsync();
        return await _dbContext.Pizzas.ToListAsync();
    }

    //GET Glutin Free Pizza
    [HttpGet("GlutinFree")]
    public async Task<ActionResult<List<Pizza>>>  GetGlutinFree()
    {
        var existing_pizza = await _dbContext.Pizzas.ToListAsync();
        List<Pizza> temp_glutin_free_list = new List<Pizza>();

        foreach(var temp_pizza in existing_pizza){
            if(temp_pizza.IsGlutenFree){
                temp_glutin_free_list.Add(temp_pizza);
            }
        }

        return temp_glutin_free_list;
    }

// dotnet6 ef core
// mysql
// EF CORE NAVIGATION -> 
// 
}