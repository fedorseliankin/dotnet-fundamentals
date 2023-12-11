namespace Task1;

public class ExceptionTask1
{
    public static void ExeptionMessage(string msg)
    {

        if (msg == null || msg.Length == 0)
        {
            throw new Exception("no input here");
        }
        else
        {
            Console.WriteLine(msg[0]);
        }
    }
}

