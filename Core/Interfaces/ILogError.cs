using System;

namespace Core.Interfaces
{
    public interface ILogError
    {
        void Log(Exception ex);
        void Log(string msgLog);         
    }
}