namespace Task1;

public class Class1
{
    public static void ExeptionMessage()
    {
        while (true)
        {
            string userInput = Console.ReadLine();

            if (userInput == null || userInput.Length == 0)
            {
                throw new Exception("no input here");
            }
            else
            {
                Console.WriteLine(userInput[0]);
            }
        }
    }
}

