using KodivaFooFuu.Core.Configuration;
using KodivaFooFuu.Core.Extensions;
using KodivaFooFuu.Core.Infrastructure;
using KodivaFooFuu.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// 1. Registrácia vašej BL
// Pre API vytvoríme statické "fake" options pre CLI, 
// pretože IOutputSink si v API budeme riešiť lokálne (Scoped)
builder.Services.AddFooFuuServices(builder.Configuration, new KodivaOptions());

// 2. Prekryjeme IOutputSink, aby bol Scoped (pre každú požiadavku nový zoznam)
builder.Services.AddScoped<MemoryOutputSink>();
builder.Services.AddScoped<IOutputSink>(sp => sp.GetRequiredService<MemoryOutputSink>());

var app = builder.Build();

// 3. GET Endpoint
app.MapGet("/foofuu", (
    [FromQuery] int start,
    [FromQuery] int end,
    [FromServices] INumberProcessor processor,
    [FromServices] MemoryOutputSink sink) =>
{
    // 4. Validácia
    if (start > end || start < 1)
    {
        return Results.BadRequest("Neplatný rozsah. Start musí byť >= 1 a <= End.");
    }

    // 5. Spustenie biznis logiky
    processor.Generate(start, end);

    // 6. Vrátenie výsledkov ako JSON
    return Results.Ok(sink.GetResults());
});

app.Run();