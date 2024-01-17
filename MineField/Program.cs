namespace MineField
{
    public class MineField
    {
        public static void Main()
        {
            Console.Write('\0');
            char[,] input = readField();
            printField(processField(input));
        }

        public static char[,] readField()
        {

            string[] Sizes = Console.ReadLine()!.Split(" ");
            int N = int.Parse(Sizes[0]);
            int M = int.Parse(Sizes[1]);
            char[,] field = new char[M, N];
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    field[i, j] = Console.ReadKey().KeyChar;
                }
                Console.ReadKey();
            }
            return field;
        }

        public static int[,] processField(char[,] input)
        {
            int M = input.GetLength(0);
            int N = input.GetLength(1);
            int[,] field = new int[M, N];
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    int count = 0;
                    if (input[i, j] != '*' && input[i, j] != '.')
                    {
                        throw new Exception("incorrect input");
                    }

                    if (i + 1 < M && input[i + 1, j] == '*')
                    {
                        count++;
                    }
                    if (i - 1 >= 0 && input[i - 1, j] == '*')
                    {
                        count++;
                    }
                    if (i + 1 < M && j + 1 < N && input[i + 1, j + 1] == '*')
                    {
                        count++;
                    }
                    if (i + 1 < M && j - 1 >= 0 && input[i + 1, j - 1] == '*')
                    {
                        count++;
                    }
                    if (i - 1 >= 0 && j + 1 < M && input[i - 1, j + 1] == '*')
                    {
                        count++;
                    }
                    if (i - 1 >= 0 && j - 1 >= 0 && input[i - 1, j - 1] == '*')
                    {
                        count++;
                    }
                    if (j + 1 < N && input[i, j + 1] == '*')
                    {
                        count++;
                    }
                    if (j - 1 >= 0 && input[i, j - 1] == '*')
                    {
                        count++;
                    }
                    field[i, j] = count;
                }
            }
            return field;
        }

        public static void printField(int[,] field)
        {
            Console.Clear();
            int M = field.GetLength(0);
            int N = field.GetLength(1);
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (field[i, j] == 0)
                    {
                        Console.Write('*');
                    }
                    else
                    {
                        Console.Write(field[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}