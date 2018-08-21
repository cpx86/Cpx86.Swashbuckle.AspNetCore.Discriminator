using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Cpx86.Swashbuckle.AspNetCore.Discriminator.Extensions
{
    public static class SwaggerGeneratorOptionsExtensions
    {
        public static void Polymorphic(this SwaggerGenOptions options, Action<SwaggerPolymorphicOptions> builder)
        {
            var polyOptions = new SwaggerPolymorphicOptions();
            builder(polyOptions);
        }
    }

    public class SwaggerPolymorphicOptions
    {
        public PolymorphicTypeBuilder<T> Add<T>()
        {
            var polymorphicTypeBuilder = new PolymorphicTypeBuilder<T>();
            TypeBuilders.Add(polymorphicTypeBuilder);
            return polymorphicTypeBuilder;
        }

        internal List<PolymorphicTypeBuilder> TypeBuilders { get; } = new List<PolymorphicTypeBuilder>();
    }

    public abstract class PolymorphicTypeBuilder
    {
        internal List<DiscriminatorBuilder> DiscriminatorBuilders { get; } = new List<DiscriminatorBuilder>();
        internal abstract Type Type { get; }
    }

    public class PolymorphicTypeBuilder<T> : PolymorphicTypeBuilder
    {
        public DiscriminatorBuilder<T, TDiscriminator> Discriminator<TDiscriminator>(Func<T, TDiscriminator> func)
        {
            var discriminatorBuilder = new DiscriminatorBuilder<T, TDiscriminator>();
            DiscriminatorBuilders.Add(discriminatorBuilder);
            return discriminatorBuilder;
        }

        internal override Type Type { get; } = typeof(T);
    }

    public class DiscriminatorBuilder
    {
        internal List<DiscriminatorMapping> Mappings { get; } = new List<DiscriminatorMapping>();
    }

    public class DiscriminatorBuilder<TBaseType, TDiscriminator> : DiscriminatorBuilder
    {
        public DiscriminatorBuilder<TBaseType, TDiscriminator> Mapping<TMappedType>(TDiscriminator discriminatorValue)
        {
            Mappings.Add(new DiscriminatorMapping(typeof(TBaseType), typeof(TMappedType), discriminatorValue));
            return this;
        }

    }

    internal class DiscriminatorMapping
    {
        public Type BaseType { get; }
        public Type Mappedtype { get; }
        public object DiscriminatorValue { get; }

        public DiscriminatorMapping(Type baseType, Type mappedtype, object discriminatorValue)
        {
            BaseType = baseType;
            Mappedtype = mappedtype;
            DiscriminatorValue = discriminatorValue;
        }
    }
}