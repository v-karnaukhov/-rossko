using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PermutationsService.Tests.Mocks;
using PermutationsService.Web.DataAccess;

namespace PermutationsService.Tests
{
    [TestClass]
    public class PermutationsServiceTests
    {
        private Web.Services.Concrete.PermutationsService _permutationsService;
        private DataContext _context;

        [TestInitialize]
        public void Bootstrap()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "DatabaseForTests")
                .Options;
            _context = new DataContext(options);

            _permutationsService = new MockPermutationsService();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _context.Dispose();
        }

        [TestMethod]
        public void GenerateUniqueKey_MustReturnValidSHA()
        {
            var data = new Dictionary<string, string>
            {
                {"asdfg456", "CE4920552CB46EA325BCC1F5DEAA2CCE2C3516F49B5BF0372089D014984BE079"},
                {"qwerty123", "DAAAD6E5604E8E17BD9F108D91E26AFE6281DAC8FDA0091040A7A6D7BD9B43B5"},
                {"zxcvbnm1", "9257970BC4C4163C673336F01CB5CEB1180214012DDB1B438EC5696948FD9121"},
                {"12345678", "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F"}
            };

            foreach (var testData in data)
            {
                Assert.AreEqual(testData.Value, _permutationsService.GetUniqueKeyByValue(testData.Key));
            }

        }

        [TestMethod]
        public void GenerateUniqueKey_MustReturnIdenticalResult()
        {
            var testElement = "asdfg456";
            var calculatedHash = "CE4920552CB46EA325BCC1F5DEAA2CCE2C3516F49B5BF0372089D014984BE079";
            var testTasks = new List<Task>();
            var resultHashes = new List<string>();

            for (var i = 0; i < 10; i++)
            {
                testTasks.Add(new Task(() => resultHashes.Add(_permutationsService.GetUniqueKeyByValue(testElement))));
            }

            Parallel.ForEach(testTasks, t => t.Start());
            Task.WaitAll(testTasks.ToArray());

            Assert.AreEqual(resultHashes.Count, testTasks.Count);
            foreach (var resultHash in resultHashes)
            {
                Assert.AreEqual(resultHash, calculatedHash);
            }
        }

        [TestMethod]
        public void GetPermutations_SecondaryGenerationOfPermutationsMustReturnVakueFromDB()
        {
            var calculatedHash = "CE4920552CB46EA325BCC1F5DEAA2CCE2C3516F49B5BF0372089D014984BE079";
            var testString = "asdfg456";


            var result1 = _permutationsService.GetPermutations(new[] { testString }).Result.First();
            var result2 = _permutationsService.GetPermutations(new[] { testString }).Result.First();
            var result3 = _permutationsService.GetPermutations(new[] { testString }).Result.First();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "DatabaseForTests")
                .Options;
            var context = new DataContext(options);

            Assert.AreEqual(context.PermutationEntries.Count(), 1);
            Assert.AreEqual(result1.Id, 1);
            Assert.AreEqual(result1.UniqueKey, calculatedHash);
            Assert.AreEqual(result2.Id, 1);
            Assert.AreEqual(result2.UniqueKey, calculatedHash);
            Assert.AreEqual(result3.Id, 1);
            Assert.AreEqual(result3.UniqueKey, calculatedHash);
        }

        [TestMethod]
        public void GetPermutations_MustCalculateRightCountOfPermutations()
        {
            var calculatedHash = "CE4920552CB46EA325BCC1F5DEAA2CCE2C3516F49B5BF0372089D014984BE079";
            var testString = "asdfg456";

            var result1 = _permutationsService.GetPermutations(new[] { testString }).Result.First();

            // общее кол-во перестановок - это n!
            Assert.AreEqual((long)GetFactorial(result1.Item.Length), result1.ResultCount);
        }

        private double GetFactorial(int number)
        {
            if (number == 1)
            {
                return 1;
            }

            return number * GetFactorial(number - 1);
        }
    }
}
