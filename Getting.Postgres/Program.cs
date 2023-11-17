using Getting.Postgres;
using Getting.Postgres.Data;
using Getting.Postgres.Repositories;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));

builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<INotesRepository, NotesRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/Notes", async (IUnitOfWork unitOfWork) =>
{
	return await unitOfWork.NotesRepository.GetAllAsync();
});

app.MapGet("/Note/{id}", async (Guid id, IUnitOfWork unitOfWork) =>
{
	return await unitOfWork.NotesRepository.FindAsync(id);
});

app.MapPost("/Note", async ([FromBody]NoteDto noteDto, IUnitOfWork unitOfWork) =>
{
	var note = NoteMapper.MapNoteDtoToNote(noteDto);

	await unitOfWork.NotesRepository.AddAsync(note);
	await unitOfWork.CommitAsync();

	return note;
});



app.Run();
