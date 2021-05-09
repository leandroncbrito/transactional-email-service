using System.Collections.Generic;
using System.Linq;
using TransactionalEmail.Core.DTO;
using TransactionalEmail.Core.ValueObjects;

namespace TransactionalEmail.Tests
{
    public abstract class TestCase
    {
        protected string ToName = "name";
        protected string ToEmail = "test@test.com";

        protected IEnumerable<To> Recipients => new List<To>
        {
            new To(ToName, ToEmail)
        };
    }
}
