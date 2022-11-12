using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LearningPortal.Infrastructure.EFCore.Common.ExMethods
{
    public static class ModelBuilderEx
    {
        public static void RegisterEntityTypeConfiguration(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            MethodInfo applyGenericMethod = typeof(ModelBuilder).GetMethods().First(a => a.Name == nameof(ModelBuilder.ApplyConfiguration));

            var types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(a => a.IsClass && !a.IsAbstract && a.IsPublic);

            foreach (var type in types)
            {
                foreach (var iFace in type.GetInterfaces())
                {
                    if (iFace.IsConstructedGenericType && iFace.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    {
                        MethodInfo applyConcreateMethod = applyGenericMethod.MakeGenericMethod(iFace.GenericTypeArguments[0]);

                        applyConcreateMethod.Invoke(modelBuilder, new[] { Activator.CreateInstance(type) });
                    }
                }
            }
        }
        public static void RegisterAllEntities<T>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes())
                .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(T).IsAssignableFrom(c));

            foreach (var type in types)
            {
                modelBuilder.Entity(type);
            }
        }
    }
}
