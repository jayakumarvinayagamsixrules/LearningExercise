public class EFCoreFileLog
{
    
    public void Log(string message)
    {
        // append to the existing stream
        StreamWriter sw = new StreamWriter("simple_logging_output.txt", true);

        sw.WriteLine(message);
        sw.WriteLine("-----------------------------------------------------------------");

        // Close the handle
        sw.Flush();
        sw.Close();
    }
}