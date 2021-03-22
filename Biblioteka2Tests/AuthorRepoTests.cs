using Domain.Models;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Biblioteka2Tests
{
    public class AuthorRepoTests : TestBase
    {
        [Fact]
        public void GetAll_ShouldReturnAllObjects()
        {
            var expected = _context.Authors.Count();
            var actual = uow.Authors.GetAll().Count();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetAsync_ShouldReturnSingleObjectAsync(int id)
        {
            var expected = await _context.Authors.FindAsync(id);
            var actual = await uow.Authors.GetAsync(id);

            Assert.Equal(expected.FirstName, actual.FirstName);
            Assert.Equal(expected.LastName, actual.LastName);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNullObjectAsync()
        {
            var id = 8291;
            var actual = await uow.Authors.GetAsync(id);

            Assert.Null(actual);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddNewRecord()
        {
            var expected = new Author { AuthorId = 101, FirstName = "authornew", LastName = "lastnamenew" };

            await uow.Authors.CreateAsync(expected);
            var actual = _context.Authors.Find(101);

            Assert.Same(expected, actual);
        }

        [Fact]
        public void Update_ShouldUpdateRecord()
        {
            var authorId = 1;
            var author = _context.Authors.Find(authorId);
            var NewName = "New value";

            author.FirstName = NewName;
            uow.Authors.Update(author);
            var updated = _context.Authors.Find(authorId);

            Assert.Equal(NewName, updated.FirstName);
        }

        [Fact]
        public async Task Delete_ShouldRemoveRecord()
        {
            var id = 2;
            var toDelete = _context.Authors.Find(id);

            uow.Authors.Delete(toDelete);
            var shouldBeNull = _context.Authors.Find(id);

            Assert.Null(shouldBeNull);
        }
    }
}
