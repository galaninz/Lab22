using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество элементов массива");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Action<Task<int[]>> action1 = new Action<Task<int[]>>(MaxArray);
            Task task2 = task1.ContinueWith(action1);

            Action<Task<int[]>> action2 = new Action<Task<int[]>>(SumArray);
            Task task3 = task1.ContinueWith(action2);

            //Почему если вместо task1 поставить предшественник task2 программа ругается как будто больше не видит сам массив?
            
            Action<Task<int[]>> action3 = new Action<Task<int[]>>(PrintArray);
            Task task4 = task1.ContinueWith(action3);

            task1.Start();


            Console.ReadKey();


        }

        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random r = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = r.Next(0, 100);
            }
            return array;
        }

        static void MaxArray(Task<int[]> task1)
        {
            int[] array = task1.Result;
            for (int i = 0; i < array.Count() - 1; i++)
            {
                for (int j = 0; j < array.Count(); j++)
                {
                    if (array[i] > array[j])
                    {
                        int t=array[i];
                        array[i] = array[j];
                        array[j] = t;
                    }
                }
            }
            Console.WriteLine($"Максимальное число в массиве = {array[0]}");
        }

        static void PrintArray(Task<int[]> task1)
        {
            int[] array = task1.Result;
            for (int i = 0; i < array.Count(); i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
        }

        static void SumArray(Task<int[]> task1)
        {
            int[] array = task1.Result;
            int sum = 0;
            for (int i = 0; i < array.Count() - 1; i++)
            {
                sum += array[i];
            }
            Console.WriteLine($"Сумма чисел массива = {sum}");
        }
    }
}
