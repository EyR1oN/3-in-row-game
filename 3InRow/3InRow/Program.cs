using System;
using System.Collections.Generic;

namespace _3inRow
{
    class Game
    {
        public enum RowOrColumn
        {
            Row = 1,
            Column = 2
        }
        private int size;
        private int[,] field;
        private int score;

        public Game(int size)
        {
            score = 0;
            this.size = size;
            field = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    field[i, j] = -1;
                }
            }
        }

        private void GenerateField()
        {
            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field[i, j] == -1)
                    {
                        field[i, j] = random.Next(4);
                    }
                }
            }
        }

        private void PrintField()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(field[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

        private void DestroyElements(int ind1, int ind2, int counter, RowOrColumn rowOrColumn)
        {
            if (rowOrColumn == RowOrColumn.Row)
            {
                for (int j = ind2; counter + 1 != 0; j--, counter--)
                {
                    field[ind1, j] = -1;
                }

            }
            else
            {
                for (int j = ind1; counter + 1 != 0; j--, counter--)
                {
                    field[j, ind2] = -1;
                }
            }
        }
        private void DestroyForList(List<List<int>> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                DestroyElements(list[i][0], list[i][1], list[i][2], (RowOrColumn)list[i][3]);
            }
        }

        private List<List<int>> CheckRow()
        {
            var arrToDestroy = new List<List<int>>();
            int counter = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size - 1; j++)
                {
                    if (field[i, j] == field[i, j + 1])
                    {
                        counter++;
                        if (counter > 1 && (j + 2 >= size || field[i, j + 2] != field[i, j + 1]))
                        {
                            arrToDestroy.Add(new List<int> { i, j + 1, counter, (int)RowOrColumn.Row });
                            counter = 0;
                        }
                    }
                    else
                    {
                        counter = 0;
                    }
                }
                counter = 0;
            }
            return arrToDestroy;
        }
        private List<List<int>> CheckColumn()
        {
            var arrToDestroy = new List<List<int>>();
            int counter = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size - 1; j++)
                {
                    if (field[j, i] == field[j + 1, i])
                    {
                        counter++;
                        if (counter > 1 && (j + 2 >= size || field[j + 2, i] != field[j + 1, i]))
                        {
                            arrToDestroy.Add(new List<int> { j + 1, i, counter, (int)RowOrColumn.Column });
                            counter = 0;
                        }
                    }
                    else
                    {
                        counter = 0;
                    }
                }
                counter = 0;
            }
            return arrToDestroy;
        }

        private void MoveDown()
        {
            int k = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field[size - j - 1, i] != -1)
                    {
                        int temp = field[size - k - 1, i];
                        field[size - k - 1, i] = field[size - j - 1, i];
                        field[size - j - 1, i] = temp;
                        k++;
                    }
                }
                k = 0;
            }
        }

        public void Play()
        {
            bool continueGame = true;
            int i = 1;
            while (continueGame)
            {
                Console.WriteLine("===================================================================");
                Console.WriteLine("ROUND " + i);
                i++;
                GenerateField();
                Console.WriteLine();
                Console.WriteLine("Generating random numbers: ");
                Console.WriteLine();
                PrintField();
                var lRow = CheckRow();
                var lColumn = CheckColumn();
                score += (lRow.Count + lColumn.Count) * 100;
                if (lRow.Count + lColumn.Count == 0)
                {
                    continueGame = false;
                    break;
                }
                DestroyForList(lRow);
                DestroyForList(lColumn);
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("Disappearing numbers (-1 is for empty): ");
                Console.WriteLine();
                PrintField();
                MoveDown();
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("Falling numbers: ");
                Console.WriteLine();
                PrintField();
                Console.WriteLine("-------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("YOUR SCORE: " + score);
                Console.WriteLine();
                Console.WriteLine("===================================================================");
            }
            Console.WriteLine();
            Console.WriteLine("Game over(");
            Console.WriteLine();
            Console.WriteLine("===================================================================");
            Console.WriteLine();
            Console.WriteLine("TOTAL SCORE: " + score);
            Console.WriteLine();
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game(9);
            game.Play();
        }
    }
}
