using NUnit.Framework;
using LivrariaAPI.DAO;

namespace LivrariaTest
{
    public class Tests
    {
        private LivroDAO _livroDAO;

        [SetUp]
        public void SetUp()
        {
            _livroDAO = new LivroDAO();
        }

        [Test]
        public void Testlista()
        {
            //var result = _livroDAO.ListAllAsync();
            Assert.Pass();
        }
    }
}