using Moq;
using Neptune.Domain;
using Neptune.Infra;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neptune.Application.Tests
{
    public class TransacaoServiceTests
    {
        private readonly Mock<ITransacaoRepository> _transacaoRepository = new();

        [Test]
        public void Test()
        {
            // arrange
            
            // act            

            // assert
            Assert.True(true);            
        }
    }
}