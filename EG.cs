using System;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.IO;

namespace EG
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                help();
                return;
            }

            CSharpCodeProvider provider = new CSharpCodeProvider();

            ICodeCompiler compiler = provider.CreateCompiler();

            CompilerParameters parameters = new CompilerParameters();
            
            parameters.GenerateExecutable = true;

            parameters.OutputAssembly = "output.exe";

            parameters.IncludeDebugInformation = true;

            parameters.ReferencedAssemblies.Add("System.dll");

            parameters.GenerateInMemory = false;

            parameters.WarningLevel = 3;

            parameters.TreatWarningsAsErrors = false;

            parameters.CompilerOptions = "/optimize";

            parameters.TempFiles = new TempFileCollection(".", true);
          
            string inputFilePath = "";

            for (int i = 0; i < args.Length; i++)
            {
                

                if (args[i] == "-h")
                {
                    help();
                    return;
                }

                if (args[i] == "-i")
                {
                    if (args.Length > (i + 1))
                    {
                        inputFilePath = args[i + 1];
                        
                    }
                    else
                    {
                        ERR("ERROR : wrong parameters \"-i\" e.g. -i ./input.cs");
                        return;
                    }
                }

                if (args[i] == "-o")
                {
                    if (args.Length > (i + 1))
                    {
                        parameters.OutputAssembly = args[i + 1];
                    }
                    else
                    {
                        ERR("WARN : wrong parameters \"-o\" e.g. -o output.exe", true);
                    }
                }

                if (args[i] == "-r")
                {
                    if (args.Length > (i + 1))
                    {
                        string[] rset = args[i + 1].Split(',');
                        foreach (var item in rset)
                        {
                            parameters.ReferencedAssemblies.Add(item);
                        }
                    }
                    else
                    {
                        ERR("ERROR : wrong parameters \"-r\" e.g. -r System.dll");
                        return;
                    }
                }
            }
            
            string input = "";

            if (inputFilePath != "")
            {
                try
                {
                    FileStream f = File.Open(inputFilePath, FileMode.Open, FileAccess.Read);

                    StreamReader reader = new StreamReader(f);

                    string line = "";

                    while ((line = reader.ReadLine()) != null)
                    {
                        input += line;
                    }
                }
                catch (Exception e)
                {
                    ERR("IO ERROR: " + e.Message);
                    return;
                }
            }

            if (input != "")
            {
                CompilerResults cr = compiler.CompileAssemblyFromSource(parameters, input);

                if (cr.Errors.HasErrors)
                {
                    foreach (CompilerError err in cr.Errors)
                    {
                        ERR(err.ErrorText);
                    }
                }
            }
            else
            {
                ERR("ERROR : Input file is null");
                return;
            }
        }

        static void ERR(string msg , bool warn = false)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (warn)
                Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        static void help()
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("This program generate .exe file from single csharp (.NET) sources (.cs) file ");
            Console.WriteLine("");
            Console.WriteLine("Example: eg.exe -i source.cs -o output.exe -r System.dll,DaWebSocket.dll ");
            Console.WriteLine("");
            Console.WriteLine("Parameters: ");
            Console.WriteLine("        -i | input file (required) ");
            Console.WriteLine("        -o | output file (defalt : \"./output.exe\")");
            Console.WriteLine("        -r | reference libiary");
            Console.WriteLine("        -h | help");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Project:  Src.Pub");
            Console.WriteLine("Team:     DA.Studio");
            Console.WriteLine("Email:    i@da.studio");
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}
