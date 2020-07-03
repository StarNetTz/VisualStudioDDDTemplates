using NUnit.Framework;
using ServiceStack.FluentValidation;
using ServiceStack.FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public abstract class ValidatorTestBase<T>
    {
        protected IValidator<T> Validator;

        public ValidatorTestBase(IValidator<T> validator)
        {
            Validator = validator;
        }

        protected async Task AssertRuleBroken(T obj, string property, string errorCode)
        {
            var res = await Validator.ValidateAsync(obj);
            AssertPropertyInvalid(res.Errors, property, errorCode);
        }

        void AssertPropertyInvalid(IList<ValidationFailure> errors, string property, string errorCode)
        {
            var item = (from e in errors where e.PropertyName == property && e.ErrorCode == errorCode select e).FirstOrDefault();
            Assert.That(item, Is.Not.Null);
        }

        protected string CreateStringOfLength(int length)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
                sb.Append("X");
            return sb.ToString();
        }
    }
}
