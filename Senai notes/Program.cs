using Senai_notes.Context;
using Senai_notes.Interfaces;
using Senai_notes.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SenaiNotesContext, SenaiNotesContext>();

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

// Adicionando cors para permitir que o back converse com o front
builder.Services.AddCors(

    // criando uma logica lambda para verificar se o link do site é o do nosso front
    option => 
    {
        // usando o metodo addpolicy para adicionar a politica do cors
        option.AddPolicy(

            name: "MinhasOrigens",
            policy => 
            {
                // usando metodo withOrigins para definir o link do site do front
                policy.WithOrigins("http://localhost:5500");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            }
            
        );

    });

var app = builder.Build();

app.UseCors("MinhasOrigens");

app.MapControllers();


app.UseSwagger();
app.UseSwaggerUI();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.Run();
