using CityInfo.API.Services;

namespace CityApi.Services
{
    public class CloudMailService : IMailService
    {
        string _mailTo = "Iman@Madaeny.com";
        string _mailFrom = "log@Toplearn.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail  From {_mailFrom}  To {_mailTo}  , "
                + $"with {nameof(CloudMailService)}  ,  ");
            Console.WriteLine($"Subject {subject}");
            Console.WriteLine($"Message {message}");
        }
    }
}
