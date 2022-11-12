namespace LearningPortal.Framework.Contracts
{
    public interface ISmsSender
    {
        public bool Send(string PhoneNumber, string Message);
        public bool SendLoginCode(string PhoneNumber, string Code);
    }
}
