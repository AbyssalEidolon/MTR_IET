namespace JsonUnityLoader
{
    internal class Program
    {
        //First run (No Args Needed.) to generate folders.

        static void Main(string[] args)
        {
            InitFolders();
            Loader loader = new();
        }
        static void InitFolders()
        {
            if (!Directory.Exists(".\\Input") && !Directory.Exists(".\\Output")){
                Directory.CreateDirectory(".\\Input");
                Directory.CreateDirectory(".\\Output");
                Environment.Exit(0);
            }
        }
    }
}