using System;

namespace LearningPortal.Framework.Domain.Contracts
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }

    public interface IEntity : IEntity<Guid>
    {

    }

    //public class BaseEntity : IEntity
    //{
    //    public Guid Id { get; set; }
    //}
}
