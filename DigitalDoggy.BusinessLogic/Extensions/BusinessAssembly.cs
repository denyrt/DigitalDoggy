using System.Reflection;

namespace DigitalDoggy.BusinessLogic.Extensions
{
    public static class BusinessAssembly
    {
        public static Assembly GetAssembly() => typeof(BusinessAssembly).Assembly;
    }
}
