namespace LearningPortal.Framework.Application.Services.IPChecker
{
    public interface IIPAddressChecker
    {
        string CheckIp(string ip);
        string GetLangAbbr(string ip);
    }
}