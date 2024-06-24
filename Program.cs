using SpaceCheckSimple;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<DataService>();
builder.Services.AddSingleton<AlertService>();
builder.Services.AddSingleton<EmailService>();
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/", () => "Valid endpoints: /last (dashbloard lol), /testmail (sends test email)");

app.MapGet("/post/machine/{machine}/percent_full/{percent_use}",
    (string machine, int percent_use, DataService ds) => 
        ds.Add(machine, percent_use, DateTime.Now));

app.MapGet("/last", (DataService ds) => ds.GetLast());

app.MapGet("/testmail", (EmailService em) => em.TestEmail());


app.Run();

