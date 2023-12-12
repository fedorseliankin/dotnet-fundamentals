//using static Task1.ExceptionTask1;

//while (true)
//{
//    try
//    {
//        string userInput = Console.ReadLine();
//        ExeptionMessage(userInput);
//    } catch(Exception e)
//    {
//        Console.WriteLine(e);
//    }

//}

using Task2;
var parser = new ExceptionNumberParser();
while (true)
{
    try
    {
        string userInput = Console.ReadLine();
        Console.WriteLine("digit parsed: " + parser.Parse(userInput));
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }

}