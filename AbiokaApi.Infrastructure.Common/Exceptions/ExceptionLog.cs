using AbiokaApi.Infrastructure.Common.Domain;
using System;

namespace AbiokaApi.Infrastructure.Common.Exceptions
{
    public class ExceptionLog : IdEntity<Guid>
    {
        public ExceptionLog() {

        }

        public ExceptionLog(string source, string request, string typeName, string errorCode, string message, Guid userId, string ip)
            : this() {
            Source = source;
            Request = request;
            TypeName = typeName;
            ErrorCode = errorCode;
            Message = message;
            UserId = userId;
            IP = ip;
        }

        public virtual string Source { get; protected set; }

        public virtual string Request { get; protected set; }

        public virtual string TypeName { get; protected set; }

        public virtual string ErrorCode { get; protected set; }

        public virtual string Message { get; protected set; }

        public virtual Guid UserId { get; protected set; }

        public virtual string IP { get; protected set; }
    }
}
