namespace Vives.Services.Model.Extensions
{
    public static class ServiceResultExtensions
    {
        public static T AlreadyRemoved<T>(this T serviceResult)
            where T : ServiceResult
        {
            serviceResult.Messages.Add(
                new ServiceMessage()
                {
                    Code = "AlreadyRemoved",
                    Description = "Entity was already removed.",
                    Type = ServiceMessageType.Warning
                });

            return serviceResult;
        }

        public static T NotFound<T>(this T serviceResult, string entity)
            where T : ServiceResult
        {
            serviceResult.Messages.Add(
                new ServiceMessage()
                {
                    Code = "NotFound",
                    Description = $"{entity} is not found.",
                    Type = ServiceMessageType.Error
                });

            return serviceResult;
        }

        public static T Required<T>(this T serviceResult, string propertyName)
            where T : ServiceResult
        {
            serviceResult.Messages.Add(
                new ServiceMessage()
                {
                    Code = "Required",
                    PropertyName = propertyName,
                    Description = $"{propertyName} is required.",
                    Type = ServiceMessageType.Error
                });

            return serviceResult;
        }

        public static T NoContent<T>(this T serviceResult)
            where T : ServiceResult
        {
            serviceResult.Messages.Add(
                new ServiceMessage()
                {
                    Code = "NoContent",
                    Description = "There is no content",
                    Type = ServiceMessageType.Info
                });

            return serviceResult;
        }

        public static T Unauthorized<T>(this T serviceResult)
            where T : ServiceResult
        {
            serviceResult.Messages.Add(
                new ServiceMessage()
                {
                    Code = "Unauthorized",
                    Description = "You are not allowed to perform this action.",
                    Type = ServiceMessageType.Error
                });

            return serviceResult;
        }
    }
}
