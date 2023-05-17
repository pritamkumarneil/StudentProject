using Microsoft.EntityFrameworkCore;
using StudentProject.Repository;
using StudentProject.Service;
using StudentProject.Service.ServiceImpl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
        // registering the SchoolDbContext in IOC container for dependency 
        builder.Services.AddDbContext<SchoolDbContext>(options =>
        options.UseMySQL(builder.Configuration.GetConnectionString("mySqlConnectionString")));

//builder.Services.Add(new ServiceDescriptor(typeof(IStudentService),new StudentService()));

/*    builder.Services.AddSingleton<IStudentService,StudentService>(provider =>
    {
        SchoolDbContext dbDependency = provider.GetRequiredService<SchoolDbContext>();
        return new StudentService(dbDependency);
    });*/

        // this is for getting the dependecy from the IOC container
        SchoolDbContext schoolDbContext = builder.Services.BuildServiceProvider().GetService<SchoolDbContext>();

        // registering the StudentService in ioc container
        builder.Services.Add(new ServiceDescriptor(typeof(IStudentService), new StudentService(schoolDbContext)));
        builder.Services.Add(new ServiceDescriptor(typeof(ITeacherService), new TeacherService(schoolDbContext)));
        builder.Services.Add(new ServiceDescriptor(typeof(ICourseService), new CourseService(schoolDbContext)));
        builder.Services.Add(new ServiceDescriptor(typeof(IStandardService), new StandardService(schoolDbContext)));
        builder.Services.Add(new ServiceDescriptor(typeof(IStudentAddressService), new StudentAddressService(schoolDbContext)));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
