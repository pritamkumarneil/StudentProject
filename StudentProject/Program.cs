using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StudentProject.Repository;
using StudentProject.Service;
using StudentProject.Service.ServiceImpl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
        // registering the SchoolDbContext in IOC container for dependency 
        builder.Services.AddDbContext<SchoolDbContext>(options =>
        options.UseMySQL(builder.Configuration.GetConnectionString("mySqlConnectionString")!));

//builder.Services.Add(new ServiceDescriptor(typeof(IStudentService),new StudentService()));

/*    builder.Services.AddSingleton<IStudentService,StudentService>(provider =>
    {
        SchoolDbContext dbDependency = provider.GetRequiredService<SchoolDbContext>();
        return new StudentService(dbDependency);
    });*/

        // this is for getting the dependecy from the IOC container
        SchoolDbContext? schoolDbContext = builder.Services.BuildServiceProvider().GetService<SchoolDbContext>()!;
        /*SchoolDbContext? schoolDbContext1=builder.Services.Configure()*/

        // registering the StudentService in ioc container
        // here schoolDbContext variable is not advisable because using ServiceDescriptor it creates another 
        // singleton object .. which is not optimized so we should fetch the dependency from factory container itself
        //builder.Services.Add(new ServiceDescriptor(typeof(IStudentService), new StudentService(schoolDbContext)));
        builder.Services.Add(new ServiceDescriptor(typeof(ITeacherService), new TeacherService(schoolDbContext)));
        builder.Services.Add(new ServiceDescriptor(typeof(ICourseService), new CourseService(schoolDbContext)));
        builder.Services.Add(new ServiceDescriptor(typeof(IStandardService), new StandardService(schoolDbContext)));
        builder.Services.Add(new ServiceDescriptor(typeof(IStudentAddressService), new StudentAddressService(schoolDbContext)));
        // here by using IOC container injecting the dependency of schoolDbContext into the studentService 
        builder.Services.AddScoped<IStudentService,StudentService>(serviceProvider => new StudentService(serviceProvider.GetRequiredService<SchoolDbContext>()));
        //we can use Add()/AddTransient()/AddScopped() to register dependency in IOc according to our need-like below
        //builder.Services.AddTransient<IStudentService, StudentService>(serviceProvider => new StudentService(serviceProvider.GetRequiredService<SchoolDbContext>()));  


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
