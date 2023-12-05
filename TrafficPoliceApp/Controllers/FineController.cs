namespace TrafficPolice.Controllers;

using System.Data.SqlClient;
using System.Net;
using System.Text.Json;
using TrafficPolice.Controllers.Base;
using TrafficPolice.Models;
using TrafficPolice.Extensions;
using TrafficPolice.Attributes;
using Dapper;

public class FineController : ControllerBase
{
    private const string ConnectionString = "Server=localhost;Database=TrafficPoliceDb;User Id=FarkhadAsus;";

    [HttpGet("GetAll")]
    public async Task GetFinesAsync()
    {
        using var writer = new StreamWriter(base.HttpContext.Response.OutputStream);

        using var connection = new SqlConnection(ConnectionString);
        var fines = await connection.QueryAsync<Fine>("select * from Fines");

        var finesHtml = fines.GetHtml();
        await writer.WriteLineAsync(finesHtml);
        base.HttpContext.Response.ContentType = "text/html";

        base.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
    }

    [HttpGet("GetById")]
    public async Task GetFineByIdAsync()
    {
        var fineIdToGetObj = base.HttpContext.Request.QueryString["id"];

        if (fineIdToGetObj == null || int.TryParse(fineIdToGetObj, out int fineIdToGet) == false)
        {
            base.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return;
        }

        using var connection = new SqlConnection(ConnectionString);
        var fine = await connection.QueryFirstOrDefaultAsync<Fine>(
            sql: "select top 1 * from Fines where Id = @Id",
            param: new { Id = fineIdToGet });

        if (fine is null)
        {
            base.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return;
        }

        using var writer = new StreamWriter(base.HttpContext.Response.OutputStream);
        await writer.WriteLineAsync(JsonSerializer.Serialize(fine));

        base.HttpContext.Response.ContentType = "application/json";
        base.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
    }



    [HttpPost("Create")]
    public async Task PostFineAsync()
    {
        using var reader = new StreamReader(base.HttpContext.Request.InputStream);
        var json = await reader.ReadToEndAsync();

        var newFine = JsonSerializer.Deserialize<Fine>(json);

        if (newFine == null || newFine.Price == null || string.IsNullOrWhiteSpace(newFine.Name))
        {
            base.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return;
        }

        using var connection = new SqlConnection(ConnectionString);
        var fines = await connection.ExecuteAsync(
            @"insert into Fines (Name, Price) 
        values(@Name, @Price)",
            param: new
            {
                newFine.Name,
                newFine.Price,
            });

        base.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
    }

    [HttpDelete]
    public async Task DeleteFineAsync()
    {
        var fineIdToDeleteObj = base.HttpContext.Request.QueryString["id"];

        if (fineIdToDeleteObj == null || int.TryParse(fineIdToDeleteObj, out int fineIdToDelete) == false)
        {
            base.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return;
        }

        using var connection = new SqlConnection(ConnectionString);
        var deletedRowsCount = await connection.ExecuteAsync(
            @"delete Fines
        where Id = @Id",
            param: new
            {
                Id = fineIdToDelete,
            });

        if (deletedRowsCount == 0)
        {
            base.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return;
        }

        base.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
    }

    [HttpPut]
    public async Task PutFineAsync()
    {
        var fineIdToUpdateObj = base.HttpContext.Request.QueryString["id"];

        if (fineIdToUpdateObj == null || int.TryParse(fineIdToUpdateObj, out int fineIdToUpdate) == false)
        {
            base.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return;
        }

        using var reader = new StreamReader(base.HttpContext.Request.InputStream);
        var json = await reader.ReadToEndAsync();

        var fineToUpdate = JsonSerializer.Deserialize<Fine>(json);

        if (fineToUpdate == null || fineToUpdate.Price == null || string.IsNullOrEmpty(fineToUpdate.Name))
        {
            base.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return;
        }

        using var connection = new SqlConnection(ConnectionString);
        var affectedRowsCount = await connection.ExecuteAsync(
            @"update Fines
        set Name = @Name, Price = @Price
        where Id = @Id",
            param: new
            {
                fineToUpdate.Name,
                fineToUpdate.Price,
                Id = fineIdToUpdate
            });

        if (affectedRowsCount == 0)
        {
            base.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return;
        }

        base.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
    }
}