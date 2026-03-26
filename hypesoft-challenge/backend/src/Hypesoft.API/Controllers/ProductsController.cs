using Hypesoft.Application.Commands;
using Hypesoft.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Hypesoft.API.Controllers;

/// Controller para gerenciar produtos no sistema 

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Inicializa uma nova instância do controller de produtos.
    /// </summary>
    /// <param name="mediator">Mediator para despacho de comandos e queries.</param>
    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }


    /// Retorna todos os produtos cadastrados

    [HttpGet]
    [SwaggerOperation(Summary = "Listar produtos", Description = "Retorna todos os produtos cadastrados")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return Ok(products);
    }


    /// Retorna um produto pelo ID


    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Buscar produto por ID", Description = "Retorna um produto específico pelo ID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
       var products = await _mediator.Send(new GetProductsQuery());
        var product = products.Items.FirstOrDefault(p => p.Id == id);
        return product != null ? Ok(product) : NotFound();
    }


    /// Cria um novo produto no sistema

    [HttpPost]
    [SwaggerOperation(Summary = "Criar produto", Description = "Cria um novo produto")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }


    /// Atualiza um produto existente

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualizar produto", Description = "Atualiza os dados de um produto existente")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID da rota diferente do ID do corpo da requisição.");

        var success = await _mediator.Send(command);

        if (!success)
            return NotFound();

        return NoContent();
    }


    /// Remove um produto pelo ID

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Excluir produto", Description = "Remove um produto pelo ID")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _mediator.Send(new DeleteProductCommand(id));

        if (!success)
            return NotFound();

        return NoContent();
    }


    /// Busca produtos por nome.

    [HttpGet("search")]
    [SwaggerOperation(Summary = "Busca produtos pelo nome", Description = "Retorna uma lista de produtos que contenham o termo pesquisado.")]
    public async Task<IActionResult> Search([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("O termo de busca não pode estar vazio.");

        var result = await _mediator.Send(new SearchProductsByNameQuery(name));
        return Ok(result);
    }


    /// Lista produtos por categoria.

    [HttpGet("category/{categoryId:guid}")]
    [SwaggerOperation(Summary = "Filtra produtos por categoria", Description = "Retorna todos os produtos associados a uma categoria específica.")]
    public async Task<IActionResult> GetByCategory(Guid categoryId)
    {
        var result = await _mediator.Send(new GetProductsByCategoryQuery(categoryId));
        return Ok(result);
    }


    /// Atualiza manualmente a quantidade em estoque de um produto.

    [HttpPatch("{id:guid}/stock")]
    [SwaggerOperation(Summary = "Atualiza o estoque manualmente")]
    public async Task<IActionResult> UpdateStock(Guid id, [FromBody] int newQuantity)
    {
        var success = await _mediator.Send(new UpdateStockCommand(id, newQuantity));
        return success ? Ok() : NotFound();
    }


    /// Lista produtos com estoque baixo.

    [HttpGet("low-stock")]
    [SwaggerOperation(Summary = "Lista produtos com estoque baixo", Description = "Retorna produtos com menos de 10 unidades.")]
    public async Task<IActionResult> GetLowStock()
    {
        var result = await _mediator.Send(new GetLowStockProductsQuery());
        return Ok(result);
    }
}