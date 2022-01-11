using System;

namespace DigitalDoggy.Domain.Constants
{
    public static class EnvironmentConstants
    {
        public static uint MaxConcurrentRequests => uint.Parse(Environment.GetEnvironmentVariable("DOGGY_MAX_REQUESTS", EnvironmentVariableTarget.Machine));
        public static string DbConnectionString => Environment.GetEnvironmentVariable("DOGGY_DB_STRING", EnvironmentVariableTarget.Machine);
    }
}