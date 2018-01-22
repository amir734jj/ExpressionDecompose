using System;
using Xunit;
using System;
using System.Linq.Expressions;
using ExpressionDecompose;

namespace ExpressionDecomposeTests
{
    public class ExpressionDecomposeTest : IDisposable
    {
        private readonly ExpressionDecompose<DummyClass> _decompose;
        
        public ExpressionDecomposeTest()
        {
            _decompose = new ExpressionDecompose<DummyClass>();
        }
        
        [Fact]
        public void Test_Count()
        {
            // Arrange
            Expression<Func<DummyClass, bool>> lambda = x => x.FirstName == "Random firstname" &&
                                                             x.LastName == "Random lastname" ||
                                                             x.LastName != "Testy"
                                                             && x.Age > 10;
            // Act
            _decompose.Decompose(lambda);
                
            // Assrt
            Assert.Equal(4, _decompose.AtomicExpressions.Count);
        }

        public void Dispose()
        {
            _decompose.Reset();
        }
    }
}