using BackendApi.Models;
using Microsoft.EntityFrameworkCore;
using BackendApi.Services.Termino;
using BackendApi.Services.Implementacion;

using AutoMapper;
using BackendApi.DTOs;
using BackendApi.Utilidades;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//CONFIGURACION SQL SERVER
builder.Services.AddDbContext<DbusuarioContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(options => {
    options.AddPolicy("NuevaPolitica",app => {
        app.AllowAnyOrigin()
       .AllowAnyHeader()
       .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

#region PETICIONES API REST
app.MapGet("/rol/lista", async (
    IRolService _rolService,
    IMapper _mapper
    ) =>
{
    var listaRol = await _rolService.GetList();
    var listaRolDTO = _mapper.Map<List<RolDTO>>(listaRol);

    if (listaRolDTO.Count > 0)
        return Results.Ok(listaRolDTO);
    else
        return Results.NotFound();

});

app.MapGet("/usuario/lista", async (
    IUsuarioService _usuarioService,
    IMapper _mapper
    ) =>
{
    var listaUsuario = await _usuarioService.GetList();
    var listaUsuarioDTO = _mapper.Map<List<UsuarioDTO>>(listaUsuario);

    if (listaUsuarioDTO.Count > 0)
        return Results.Ok(listaUsuarioDTO);
    else
        return Results.NotFound();

});

app.MapPost("/usuario/guardar", async (UsuarioDTO modelo, IUsuarioService _usuarioService,IMapper _mapper) => {
    var _usuario = _mapper.Map<Usuario>(modelo);
    var _usuarioCreado = await _usuarioService.Add(_usuario);

    if (_usuarioCreado.IdUsuario != 0)
        return Results.Ok(_mapper.Map<UsuarioDTO>(_usuarioCreado));
    else
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
});

app.MapPut("/usuario/actualizar/{idUsuario}", async (int idUsuario,UsuarioDTO modelo, IUsuarioService _usuarioService, IMapper _mapper) => {

    var _encontrado = await _usuarioService.Get(idUsuario);
    if (_encontrado is null) return Results.NotFound();

    var _usuario = _mapper.Map<Usuario>(modelo);
    _encontrado.Cedula = _usuario.Cedula;
    _encontrado.NombreCompleto = _usuario.NombreCompleto;
    _encontrado.IdRol= _usuario.IdRol;
    _encontrado.Correo = _usuario.Correo;

    var respuesta = await _usuarioService.Update(_encontrado);

    if (respuesta)
        return Results.Ok(_mapper.Map<UsuarioDTO>(_encontrado));
    else
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
});

app.MapDelete("/usuario/eliminar/{idUsuario}", async (int idUsuario, IUsuarioService _usuarioService) => {
    
    var _encontrado = await _usuarioService.Get(idUsuario);
    if (_encontrado is null) return Results.NotFound();


    var respuesta = await _usuarioService.Delete(_encontrado);

    if (respuesta)
        return Results.Ok();
    else
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
});



#endregion


app.UseCors("NuevaPolitica");
app.Run();

