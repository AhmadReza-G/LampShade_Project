using System;

Console.Write("Enter a number: ");
int num = int.Parse(Console.ReadLine());

for (int i = 1; i <= num; i++)
{
    for (int j = 1; j <= i - 1; j++)
        Console.Write(' ');
    for (int j = i; j <= num; j++)
        Console.Write('*');
    Console.WriteLine();
}
Console.ReadKey();