using System.ComponentModel.DataAnnotations;
using PostService.Dtos;
using PostService.Mappings;
using PostService.BusinessLogic;
using PostService.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IPostingRepository, PostingRepository>();
builder.Services.AddScoped<IPostingService, PostingService>();

var app = builder.Build();

app.UseCors();

using (var scope = app.Services.CreateScope())
{
    var repository = scope.ServiceProvider.GetRequiredService<IPostingRepository>();
    repository.CreateDb();
}

app.MapGet("/", () => "Це API поштового клієнта");

app.MapGet("/postings", (IPostingService postingService) =>
{
    var postings = postingService.GetAll();
    var resultDtos = new List<PostingGetDto>();
    foreach (var posting in postings)
    {
        resultDtos.Add(PostingMapper.ToPostingGetDto(posting));
    }
    return Results.Ok(resultDtos);
});

app.MapGet("/postings/{id}", (int id, IPostingService postingService) =>
{
    var posting = postingService.Find(id);
    if (posting is null)
    {
        return Results.NotFound();
    }

    var resultDto = PostingMapper.ToPostingGetDto(posting);
    return Results.Ok(resultDto);
});

app.MapPost("/postings", (PostingPostDto postDto, IPostingService postingService) =>
{
    var errors = ValidatePosting(postDto);
    if (errors.Count > 0)
    {
        return Results.ValidationProblem(errors);
    }

    var newPosting = PostingMapper.ToPosting(postDto);
    var savedObject = postingService.Create(newPosting);
    var resultDto = PostingMapper.ToPostingGetDto(savedObject);
    return Results.Created($"/postings/{resultDto.Id}", resultDto);
});

app.MapPut("/postings/{id}", (int id, PostingPutDto putDto, IPostingService postingService) =>
{
    if (id != putDto.Id)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            ["Id"] = ["Id в маршруті та в тілі запиту не збігаються"]
        });
    }

    var errors = ValidatePosting(putDto);
    if (errors.Count > 0)
    {
        return Results.ValidationProblem(errors);
    }

    var postingToUpdate = PostingMapper.ToPosting(putDto);
    var updated = postingService.Update(postingToUpdate);
    if (updated is null)
    {
        return Results.NotFound();
    }

    var resultDto = PostingMapper.ToPostingGetDto(updated);
    return Results.Ok(resultDto);
});

app.MapDelete("/postings/{id}", (int id, IPostingService postingService) =>
{
    var deletedCount = postingService.Delete(id);
    if (deletedCount == 0)
    {
        return Results.NotFound();
    }

    return Results.NoContent();
});

app.Run();
static Dictionary<string, string[]> ValidatePosting(object posting)
{
    var results = new List<ValidationResult>();
    Validator.TryValidateObject(posting, new ValidationContext(posting), results, true);
    var errors = new Dictionary<string, string[]>();
    foreach (var result in results)
    {
        foreach (var field in result.MemberNames)
        {
            errors[field] = [result.ErrorMessage!];
        }
    }
    return errors;
}
