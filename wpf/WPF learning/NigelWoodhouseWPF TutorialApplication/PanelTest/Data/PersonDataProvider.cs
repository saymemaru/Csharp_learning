using PanelTest.Models;

namespace PanelTest.Data
{
    public interface IPersonDataProvider
    {
        Task<IEnumerable<Person>?> GetAllAsync();
    }

    public class PersonDataProvider : IPersonDataProvider
    {
        async Task<IEnumerable<Person>?> IPersonDataProvider.GetAllAsync()
        {
            await Task.Delay(100);
            return new List<Person>
            {
                new Person() { Id = 1, FirstName = "Lee",LastName = "Shinhyun", IsDeveloper = false },
                new Person() { Id = 2, FirstName = "Quan",LastName = "Taiyung", IsDeveloper = false },
                new Person() { Id = 7, FirstName = "Kim",LastName = "Zoun", IsDeveloper = true },
            };
        }
    }
}
