using LivrariaAPI.Controllers;
using LivrariaAPI.DAO;
using LivrariaAPI.Data;
using NUnit.Framework;

namespace NUnitTestProject
{
    public class Tests
    {

        ILivroDAO _livroDAO;
        private readonly ApplicationDbContext _context;

        public Tests(ApplicationDbContext context, ILivroDAO livroDAO) 
        {
            _context = context;
            _livroDAO = livroDAO;
        }

        [SetUp]
        public void Setup()
        {
          
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}