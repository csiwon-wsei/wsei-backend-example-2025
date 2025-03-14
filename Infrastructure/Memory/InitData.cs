using ApplicationCore.Application.Commons;
using ApplicationCore.Domain.Models;
using ApplicationCore.Domain.ValueObject;

namespace Infrastructer.Memory
{
    public class InitData
    {
        public static void Init(IGenericRepositoryAsync<Movie> moviesRepo, IGenericRepositoryAsync<User> usersRepo)
        {
            var u1 = new User("Adam", "adam@wsei.edu.pl") { Id = Guid.Parse("6049fb69-f573-4088-a1c5-4e6189f2f135") };
            var u2 = new User("Ewa", "ewa@wsei.edu.pl") { Id = Guid.Parse("D63213CF-9E7B-470F-A7F8-CE996DB31D06") };
            usersRepo.AddAsync(u1);
            usersRepo.AddAsync(u2);

            Movie m1 = new Movie(
                title: "Tenet",
                description: "Sci-fi movie",
                releaseDate: new DateTime(2022, 10, 10)
            );
            m1.Id = Guid.Parse("4A30CD68-5AC9-4782-B537-D8A0DF77E809");
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