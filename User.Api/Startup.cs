using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Users.Api.Contexts;

namespace Users.Api
{
    public class Startup
    {
        const string _corsPolicy = "cors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
                //.AddJsonOptions( options =>
                //{
                //    options.JsonSerializerOptions.Converters.Add(new UserJsonFormatter());
                //});
            services.AddTransient<UserContext>(_ => new UserContext(Configuration.GetConnectionString("UserMysqlConnection")));
            services.AddCors(options => options.AddPolicy(_corsPolicy,
            builder =>
            {
                builder.
                AllowAnyOrigin().
                AllowAnyMethod().
                AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_corsPolicy);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }


    //class UserConverter : Newtonsoft.Json.JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        return (objectType == typeof(User));
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        // Load the JSON for the Result into a JObject
    //        JObject jo = JObject.Load(reader);

    //        // Read the properties which will be used as constructor parameters
    //        string firstName = (string)jo["firstName"];
    //        string lastName = (string)jo["lastname"];
    //        string email = (string)jo["email"];
    //        // Construct the Result object using the non-default constructor
    //        User result = new User(0, firstName, lastName, email);

    //        // (If anything else needs to be populated on the result object, do that here)

    //        // Return the result
    //        return result;
    //    }

    //    public override bool CanWrite => false;

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
