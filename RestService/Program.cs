using Newtonsoft.Json;
using RestServis.Models;
using Microsoft.Extensions.Options;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Carriers carriers;
string rawData;

//Retrieve data from API
// var client = new HttpClient();
// var request = new HttpRequestMessage
// {
//     Method = HttpMethod.Get,
//     RequestUri = new Uri("https://skyscanner-api.p.rapidapi.com/v3/flights/carriers"),
//     Headers =
//     {
//         { "X-RapidAPI-Key", "3d572af981msh76feb8b882f1476p14410fjsn6e986425424a" },
//         { "X-RapidAPI-Host", "skyscanner-api.p.rapidapi.com" },
//     },
// };
// using (var response = await client.SendAsync(request))
// {
//     response.EnsureSuccessStatusCode();
//     var body = await response.Content.ReadAsStringAsync();
//     carriers = JsonConvert.DeserializeObject<Carriers>(body);
// }


builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Carriers>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();