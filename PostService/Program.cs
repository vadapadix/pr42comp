using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PostService.BusinessLogic;
using PostService.DataAccess;
using PostService.Dtos;
using PostService.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddDbContext<PostingDbContext>();
builder.Services.AddScoped<IPostingRepository, PostingRepository>();
builder.Services.AddScoped<IPostingService, PostingService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.UseAllOfToExtendReferenceSchemas();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PostService API",
        Version = "v1",
        Description = "REST API обліку поштових відправлень: перегляд, створення, редагування та видалення."
    });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "PostService.xml"));
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "PostService.Dtos.xml"));
});

var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI(options => options.SwaggerEndpoint("v1/swagger.json", "PostService API v1"));

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PostingDbContext>();
    dbContext.Database.EnsureCreated();
}

app.MapGet("/", PostingEndpoints.GetInfo)
    .WithName("GetApiInfo")
    .WithTags("Про API")
    .Produces<string>(StatusCodes.Status200OK, "text/plain");
app.MapGet("/postings", PostingEndpoints.GetList)
    .WithName("GetPostings")
    .WithTags("Відправлення")
    .Produces<List<PostingGetDto>>(StatusCodes.Status200OK);
app.MapGet("/postings/{id}", PostingEndpoints.GetById)
    .WithName("GetPostingById")
    .WithTags("Відправлення")
    .Produces<PostingGetDto>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound);
app.MapPost("/postings", PostingEndpoints.Create)
    .WithName("CreatePosting")
    .WithTags("Відправлення")
    .Accepts<PostingPostDto>("application/json")
    .Produces<PostingGetDto>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest);
app.MapPut("/postings/{id}", PostingEndpoints.Update)
    .WithName("UpdatePosting")
    .WithTags("Відправлення")
    .Accepts<PostingPutDto>("application/json")
    .Produces<PostingGetDto>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound);
app.MapDelete("/postings/{id}", PostingEndpoints.Delete)
    .WithName("DeletePosting")
    .WithTags("Відправлення")
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound);

app.Run();