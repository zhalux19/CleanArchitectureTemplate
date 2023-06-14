using System.Reflection;

namespace KafkaTemplate.Data
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
