using gRPC.Server.Services;
using GrpcBrowser.Configuration;
using ProtoBuf.Grpc.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddGrpc();
builder.Services.AddCodeFirstGrpc();
builder.Services.AddGrpcBrowser();

var app = builder.Build();
app.UseGrpcBrowser();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseRouting();

app.MapGrpcService<FeedService>()
    .AddToGrpcBrowserWithClient<FeedMeMoreService.FeedMeMoreServiceClient>(); 

app.UseGrpcBrowser();

app.MapGrpcBrowser();

app.MapGet("/", context =>
{
    context.Response.StatusCode = 302;
    context.Response.Headers.Add("Location", "https://localhost:7046/grpc");
    return Task.CompletedTask;
});

app.Run();
