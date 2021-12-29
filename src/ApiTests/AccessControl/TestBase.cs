using System;
using Api.Data.Interfaces.Queries;
using Api.Data.Interfaces.Repositories;
using NUnit.Framework;

namespace ApiTests.AccessControl
{
    public abstract class TestBase
    {
        public TestBase()
        { }

        [SetUp]
        public virtual void Setup()
        {
            Environment.SetEnvironmentVariable("DATABASE_CONN_STRING", "server=localhost;user=root;password=my-secret-pw;database=Budget");
        }
    }
}
