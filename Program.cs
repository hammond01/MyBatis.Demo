using MyBatis.NET.Core;
using MyBatis.NET.Mapper;

MapperAutoLoader.AutoLoad();
var connStr = "Server=localhost;Database=DemoDb;User Id=sa;Password=123456;TrustServerCertificate=True;";

using var session = new SqlSession(connStr);

var mapper = session.GetMapper<IUserMapper>();

Console.WriteLine("\n== Test InsertUser ==");
// var newUser = ;
var data = mapper.InsertUser(new User { UserName = "Jane", Email = "jane@example.com" });
session.Commit();

Console.WriteLine("\n== Test GetAll ==");
var users = mapper.GetAll();
foreach (var u in users)
    Console.WriteLine($"{u.Id} - {u.UserName} - {u.Email}");

Console.WriteLine("\n== Test GetById (Id = 1) ==");
var user = mapper.GetById(1);

if (user != null)
    Console.WriteLine($"Found: {user.Id} - {user.UserName} - {user.Email}");
else
    Console.WriteLine("User not found!");

