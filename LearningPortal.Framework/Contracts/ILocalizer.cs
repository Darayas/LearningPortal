namespace LearningPortal.Framework.Contracts
{
    public interface ILocalizer
    {
        public string this[string Name] { get; }
        public string this[string Name, params object[] args] { get; }

    }
}
