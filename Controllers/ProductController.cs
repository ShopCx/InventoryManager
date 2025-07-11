using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace InventoryManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly string _connectionString = "Server=localhost;Database=ShopCx;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True";
    
    [HttpGet("search")]
    public IActionResult SearchProducts(string query)
    {
        try
        {
            var products = new List<Dictionary<string, object>>();
            
            string sqlQuery = $"SELECT * FROM Products WHERE Name LIKE '%{query}%' OR Description LIKE '%{query}%'";
            
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(sqlQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                product[reader.GetName(i)] = reader.GetValue(i);
                            }
                            products.Add(product);
                        }
                    }
                }
            }
            
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.ToString() });
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Dictionary<string, object> product)
    {
        try
        {
            string updateQuery = $"UPDATE Products SET ";
            foreach (var kvp in product)
            {
                updateQuery += $"{kvp.Key} = '{kvp.Value}', ";
            }
            updateQuery = updateQuery.TrimEnd(',', ' ') + $" WHERE Id = {id}";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(updateQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            return Ok(new { message = "Product updated successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.ToString() });
        }
    }

    [HttpPost("upload")]
    public IActionResult UploadProductImage(IFormFile file)
    {
        try
        {
            var fileName = file.FileName;
            var filePath = Path.Combine("uploads", fileName);
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Ok(new { message = "File uploaded successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.ToString() });
        }
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] string productJson)
    {
        try
        {
            var product = (Dictionary<string, object>)JsonConvert.DeserializeObject(productJson);
            
            string insertQuery = $"INSERT INTO Products ({string.Join(", ", product.Keys)}) VALUES ({string.Join(", ", product.Values.Select(v => $"'{v}'"))})";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(insertQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            return Ok(new { message = "Product created successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.ToString() });
        }
    }
} 