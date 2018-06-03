using System.IO;

namespace Fusion.Runner
{
    public class Arguments
    {
        public readonly DirectoryInfo Input;
        public readonly DirectoryInfo Output;

        public Arguments(DirectoryInfo input, DirectoryInfo output)
        {
            Input = input;
            Output = output;
        }
    }
}