using NationalInstruments.TestStand.Interop.API;

namespace seq_runner
{
    class SequenceRunner
    {

        static void Main(string[] args)
        {

            String targetSeqFilePath = Path.Combine(Environment.CurrentDirectory, "Sequence File\\Test.seq");
            Console.WriteLine("Loading File ->" + targetSeqFilePath);
            //String processModelName = ""; // Remove the comment if you're going to use a Process Model.
            String entryPoint = "MainSequence";
            Engine engine = new Engine();
         
            try{
            // Find the sequence file absolute path based on the calling sequence file's directory.
            Console.WriteLine("Loading Sequence File...");
            // Locate and open the sequence file contianing the sequence to be executed.
            SequenceFile targetSeqFile = engine.GetSequenceFileEx(targetSeqFilePath);
           
            /* Remove the comment if you're going to use a Process Model.
            Console.WriteLine("Loading Process Model...");
            // Locate and open the process model to be used for this execution.
            SequenceFile processModel = engine.GetStationModelSequenceFile(out processModelName);
            */

            SequenceFile processModel = null; // Comment this line if you're using a Process Model.

            // Launch a new execution of the sequence file using the specified process model.
            // The SequenceNameParameter is the name of the process model entry point
            Console.WriteLine("\nExecution Started\n-----------------------\n");
            Execution currentExecution = engine.NewExecution(targetSeqFile, entryPoint, processModel, false, 0);

            // Wait for the execution to end.
            currentExecution.WaitForEndEx(-1);

            Console.WriteLine("\n-----------------------\nExecution Finished\n");

            // Release the process model opened previously.
            engine.ReleaseSequenceFileEx(processModel, 4);

            // Release the sequence file opened previously.
            engine.ReleaseSequenceFileEx(targetSeqFile, 4);

            System.Threading.Thread.Sleep(1000);
             }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }

            }
    }
}

