namespace Vives.Services.Model
{
    public class ServiceResult
    {
        public IList<ServiceMessage> Messages { get; set; } = new List<ServiceMessage>();

        public bool IsSuccess => Messages.All(m => m.Type != ServiceMessageType.Error
                                                   && m.Type != ServiceMessageType.Critical);

        //public bool IsSuccess => !Messages.Any(m => m.Type == ServiceMessageType.Error
        //                                           || m.Type == ServiceMessageType.Critical);
    }
}
