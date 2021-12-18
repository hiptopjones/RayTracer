using NLog;
using System;
using System.Diagnostics;
using System.IO;

namespace RayTracer
{
    class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private const int SuccessExitCode = 0;
        private const int FailureExitCode = 1;

        static int Main(string[] args)
        {
            int exitCode = FailureExitCode;
            CommandLineOptions options = new CommandLineOptions();

            try
            {
                if (!options.ParseArguments(args))
                {
                    throw new Exception($"Unable to parse command line arguments: '{Environment.CommandLine}'");
                }

                double aspectRatio = (double)16 / 9;
                int imageWidth = 400;
                int imageHeight = (int)(imageWidth / aspectRatio);

                ImageRendererChapter4 generator = new ImageRendererChapter4();
                Color[] pixels = generator.GenerateImage(imageWidth, imageHeight);

                string imageText = PpmImage.CreatePpmImage(pixels, imageWidth, imageHeight);
                File.WriteAllText(@"C:\Users\hipto\Desktop\image.ppm", imageText);

                exitCode = SuccessExitCode;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Processing failed: {ex.Message}");
                exitCode = FailureExitCode;
            }
            finally
            {
                if (options.PromptUser || (exitCode != SuccessExitCode && Debugger.IsAttached))
                {
                    PromptToContinue();
                }
            }

            return exitCode;
        }

        private static void PromptToContinue()
        {
            LogManager.Flush();

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}