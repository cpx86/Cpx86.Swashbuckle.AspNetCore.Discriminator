using System;
using Cpx86.Swashbuckle.AspNetCore.Discriminator.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UnitTests
{
    public class Class1
    {
        public void Foo()
        {
            var swaggerOptions = new SwaggerGenOptions();
            swaggerOptions.Polymorphic(p =>
            {
                p.Add<BaseType>()
                    .Discriminator(x => x.Type)
                    .Mapping<SubTypeA>(Discriminator.A)
                    .Mapping<SubTypeB>(Discriminator.B);
            });
        }
    }

    internal class BaseType
    {
        public Discriminator Type { get; set; }
    }

    internal enum Discriminator
    {
        A,
        B,
    }

    internal class SubTypeA : BaseType { }
    internal class SubTypeB : BaseType { }
}
