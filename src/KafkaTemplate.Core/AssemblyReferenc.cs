using System.Reflection;

namespace KafkaTemplate.Core
{
    public static class AssemblyReference
    {
        public static Assembly Assembly => typeof(AssemblyReference).Assembly;
    }
}
