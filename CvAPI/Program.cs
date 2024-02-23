using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Driver;
using ClassLibrary.Models;
using ClassLibrary.Data;
using System.Runtime.InteropServices;
using MongoDB.Bson;
using System.Diagnostics.Eventing.Reader;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(_ => new MongoCrud<About>("Cv"));
builder.Services.AddScoped(_ => new MongoCrud<Education>("Cv"));
builder.Services.AddScoped(_ => new MongoCrud<Skills>("Cv"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//MongoCrud<About> Aboutdb = new MongoCrud<About>("Cv");

//About Endpoints
app.MapGet("/about", async (MongoCrud<About> Aboutdb) =>
{
	var result = await Aboutdb.GetAll("About");
	if (result.Count > 0) return Results.Ok(result);
	else return Results.NoContent();
	
});

app.MapPost("/about", async ( MongoCrud <About> Aboutdb, About about) =>
{
	var Aboutok = await Aboutdb.Add<About>( about, "About");
	return Results.Ok(Aboutok);
});

app.MapDelete("/about/{id}", async (MongoCrud<About> Aboutdb, string id) =>
{
	try
	{
		var result = await Aboutdb.DeleteById("About", id);
		if (result) return Results.Ok();

		
		else return Results.Ok($"Document with ID {id} deleted successfully.");
	}
	catch (Exception ex)
	{
		return Results.Problem($"An error occurred while deleting the document: {ex.Message}");
	}
});

app.MapPut("/about/{id}", async (MongoCrud<About> Aboutdb, string id, About data) =>
{
	try
	{

		var result = await Aboutdb.UpdateById<About>("About", id, data);
		if (result) return Results.Ok();

		else return Results.NotFound();
	}
	catch (Exception ex)
	{
		return Results.Problem($"An error occurred while deleting the document: {ex.Message}");
	}
});

// Education Endpoints
app.MapGet("/education", async (MongoCrud<Education> Educationdb) =>
{
	var result = await Educationdb.GetAll("Education");
	if (result.Count > 0) return Results.Ok(result);
	else return Results.NoContent();

});

app.MapPost("/education", async (MongoCrud<Education> Educationdb, Education education) =>
{
	var Educationok = await Educationdb.Add<Education>(education, "Education");
	return Results.Ok(Educationok);
});

app.MapDelete("/education/{id}", async (MongoCrud<Education> Educationdb, string id) =>
{
	try
	{
		var result = await Educationdb.DeleteById("Education", id);
		if (result) return Results.Ok();


		else return Results.Ok($"Document with ID {id} deleted successfully.");
	}
	catch (Exception ex)
	{
		return Results.Problem($"An error occurred while deleting the document: {ex.Message}");
	}
});

app.MapPut("/education/{id}", async (MongoCrud<Education> Educationdb, string id, Education data) =>
{
	try
	{

		var result = await Educationdb.UpdateById<Education>("Education", id, data);
		if (result) return Results.Ok();

		else return Results.NotFound();
	}
	catch (Exception ex)
	{
		return Results.Problem($"An error occurred while deleting the document: {ex.Message}");
	}
});

//Skills Endpoints
app.MapGet("/skills", async (MongoCrud<Skills> Skillsdb) =>
{
	var result = await Skillsdb.GetAll("Skills");
	if (result.Count > 0) return Results.Ok(result);
	else return Results.NoContent();

});

app.MapPost("/skills", async (MongoCrud<Skills> Skillsdb, Skills skills) =>
{
	var Skillsok = await Skillsdb.Add<Skills>(skills, "Skills");
	return Results.Ok(Skillsok);
});


app.MapDelete("/skills/{id}", async (MongoCrud<Skills> Skillsdb, string id) =>
{
	try
	{
		var result = await Skillsdb.DeleteById("Skills", id);
		if (result) return Results.Ok();


		else return Results.Ok($"Document with ID {id} deleted successfully.");
	}
	catch (Exception ex)
	{
		return Results.Problem($"An error occurred while deleting the document: {ex.Message}");
	}
});


app.MapPut("/skills/{id}", async (MongoCrud<Skills> Skillsdb, string id, Skills data) =>
{
	try
	{

		var result = await Skillsdb.UpdateById<Skills>("Skills", id, data);
		if (result) return Results.Ok();

		else return Results.NotFound();
	}
	catch (Exception ex)
	{
		return Results.Problem($"An error occurred while deleting the document: {ex.Message}");
	}
});

app.Run();


