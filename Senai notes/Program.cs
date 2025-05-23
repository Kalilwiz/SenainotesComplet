using System.Text;
using Microsoft.IdentityModel.Tokens;
using Senai_notes.Context;
using Senai_notes.Interfaces;
using Senai_notes.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson(options => 
{ 
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddSwaggerGen(options => 
{
    options.EnableAnnotations();

});

builder.Services.AddDbContext<SenaiNotesContext, SenaiNotesContext>();

builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddTransient<ITagRepository, TagRepository>();

builder.Services.AddTransient<ITagNotaRepository, TagNotaRepository>();

builder.Services.AddTransient<INotaRepository, NotaRepository>();

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
                policy.WithOrigins("http://localhost:5173", "http://127.0.0.1:5173");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            }
            
        );

    });

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "senaiNotes",
            ValidAudience = "senaiNotes",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("minha-chave-ultra-mega-secreta-senai"))
        };
    });

builder.Services.AddAuthentication();

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

app.UseAuthentication();

app.UseAuthorization();

app.Run();
