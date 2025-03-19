using ApplicationCore.Application.Commons;
using ApplicationCore.Domain.Models;
using ApplicationCore.Domain.ValueObject;

namespace Infrastructure.Memory
{
    public class InitData
    {
        public static void Init(IGenericRepositoryAsync<Movie> moviesRepo, IGenericRepositoryAsync<User> usersRepo)
        {
            var u1 = new User("Adam", "adam@wsei.edu.pl")
            {
                Id = Guid.Parse("CECF0050-3559-4983-A53C-749A7E6704B1")
            };
            var u2 = new User("Ewa", "ewa@wsei.edu.pl")
            {
                Id = Guid.Parse("2E0CCC34-FB17-44C8-8E33-9608F959ABE0")
            };
            usersRepo.AddAsync(u1);
            usersRepo.AddAsync(u2);

            Movie m1 = new Movie(
                title: "Tenet",
                description: "Sci-fi movie",
                releaseDate: new DateTime(2022, 10, 10)
            );
            m1.Id = Guid.Parse("04A67145-7D87-4FC7-983F-E9B27BB6B6A0");
            m1.Reviews = new List<Review>()
            {
                new (m1.Id, u1.Id, "Test 1", "Content 1", ReviewRate.Of(5))
                {
                    Id = Guid.NewGuid()
                },
                new (m1.Id, u2.Id, "Test 2", "Content 2", ReviewRate.Of(8))
                {
                    Id = Guid.NewGuid()
                }
            };
            moviesRepo.AddAsync(m1);
        }
    }
}