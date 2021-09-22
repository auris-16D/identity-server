using System;
using Api.Data.Interfaces.Queries;
using Api.Data.Interfaces.Repositories;
using NUnit.Framework;

namespace ApiTests.AccessControl
{
    public abstract class TestBase
    {
        private IBudgetsReadRepository budgetsReadRepository;

        public TestBase(IBudgetsReadRepository budgetsReadRepository)
        {
            this.budgetsReadRepository = budgetsReadRepository;
        }

        public TestBase()
        { }

        [SetUp]
        public virtual void Setup()
        {
            Environment.SetEnvironmentVariable("DATABASE_CONNECTION_STRING", "server=localhost;user=root;password=my-secret-pw;database=Budget");
        }
    }
}
