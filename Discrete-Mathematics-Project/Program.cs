using System;

namespace Discrete_Mathematics_Project
{
    static class Matrix
    {

        // Dimension of input square matrix 
        public static int N;

        // Function to get cofactor of  
        // mat[p][q] in temp[][]. n is  
        // current dimension of mat[][] 
        public static void Get_Cofactor(int[,] Matrix, int[,] Temp, int p, int q, int n)
        {
            int i = 0, j = 0;

            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {

                    // Copying into temporary matrix  
                    // only those element which are  
                    // not in given row and column 
                    if (row != p && col != q)
                    {
                        Temp[i, j++] = Matrix[row, col];

                        // Row is filled, so increase  
                        // Row index and reset col  
                        //index 
                        if (j == n - 1)
                        {
                            j = 0;
                            i++;
                        }
                    }
                }
            }
        }

        /* Recursive function for finding determinant of matrix.
           Using laplace expansion.
           n is current dimension of mat[][]. */
        public static int Determinant_Of_Matrix(int[,] Matrix, int n)
        {
            int Determinan = 0; // Initialize result 

            // Base case : if matrix  
            // contains single 
            // element 
            if (n == 1)
                return Matrix[0, 0];

            // To store cofactors 
            int[,] Temp = new int[N, N];

            // To store sign multiplier 
            int sign = 1;

            // Iterate for each element 
            // of first row 
            for (int f = 0; f < n; f++)
            {

                // Getting Cofactor of mat[0][f] 
                Get_Cofactor(Matrix, Temp, 0, f, n);
                Determinan += sign * Matrix[0, f] * Determinant_Of_Matrix(Temp, n - 1);

                // terms are to be added with  
                // alternate sign 
                sign = -sign;
            }

            return Determinan;
        }

        public static void Make_A_Laplace_Matrix(int[,] Matrix, int[,] LaplaceMatrix, int n)
        {
            int NodeDegree = 0;
            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    NodeDegree += Matrix[row, col];
                    if (Matrix[row, col] != 0)
                    {
                        LaplaceMatrix[row, col] = -1;
                    }
                }
                LaplaceMatrix[row, row] = NodeDegree;
                NodeDegree = 0;
            }

        }
        public static void First_Matrix(int[,] Matrix, int n)
        {
            for (int row = 0; row < n; row++)
            {
                for (int col = 0; col < n; col++)
                {
                    if (row == col)
                    {
                        Matrix[row, col] = 0;
                    }
                    else
                    {
                        Matrix[row, col] = 1;
                    }
                }
            }
        }

        public static void Read_Matrix_From_User(int[,] Matrix, int n)
        {
            for (; ; )
            {
                Console.Clear();
                Console.WriteLine("This is a Adjatency Matrix.\n");
                Console.WriteLine("If you want to change a cell enter your value like this :");
                Console.WriteLine("Row Column Value\n");
                Console.WriteLine("For exampel if you want change second row and first column and your value is 4 enter : ");
                Console.WriteLine("2 1 4 \n");
                Console.WriteLine("Note that you CAN'T change the original diameter of the matrix.");
                Console.WriteLine("Note that you CAN'T enter a negative number.");
                Console.WriteLine("When the matrix is correct, enter yes");
                Console.WriteLine();
                Display(Matrix, n, n);
                Console.WriteLine();

                string UserInput = Console.ReadLine();
                if (UserInput == "yes")
                {
                    return;
                }
                else
                {
                    try
                    {
                        string[] UserNumber = UserInput.Split(" ");
                        int Row = Convert.ToInt32(UserNumber[0]) - 1;
                        int Column = Convert.ToInt32(UserNumber[1]) - 1;
                        int Value = Convert.ToInt32(UserNumber[2]);
                        if (Row != Column && Value>=0)
                        {
                            Matrix[Row, Column] = Value;
                            Matrix[Column, Row] = Value;
                        }
                        else if (Row==Column)
                        {
                            Console.Clear();
                            Console.WriteLine("Note that you CAN'T change the original diameter of the matrix.");
                            Console.ReadKey();
                        }
                        else if(Value<0)
                        {
                            Console.Clear();
                            Console.WriteLine("Note that you CAN'T enter a negative number.");
                            Console.ReadKey();
                        }
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Please Check your input.");
                        Console.ReadKey();
                    }

                }

            }
        }

        public static void Display(int[,] Matrix, int row, int col)
        {
            Console.Write("   ");
            for (int i = 0; i < row; i++)
            {
                Console.Write(i + 1 + " ");
            }
            Console.WriteLine();
            for (int i = 0; i < row; i++)
            {
                Console.Write(i + 1 + "  ");
                for (int j = 0; j < col; j++)
                {
                    Console.Write(Matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }

    static class Kruskal
    {

        public static int Vertex;

        static int[] parent;
        static void initializeParent()
        {
            parent = new int[Vertex];
        }

        static int INF = int.MaxValue;



        // Find set of vertex i 
        static int find(int i)
        {
            while (parent[i] != i)
            {
                i = parent[i];
            }
            return i;
        }

        // Does union of i and j. It returns 
        // false if i and j are already in same 
        // set. 
        static void union1(int i, int j)
        {
            int a = find(i);
            int b = find(j);
            parent[a] = b;
        }

        // Finds MST using Kruskal's algorithm 
        public static int kruskalMST(int[,] Matrix, int[,] Temp)
        {
            for (int i = 0; i < Vertex; i++)
            {
                for (int j = 0; j < Vertex; j++)
                {
                    if (i == j || Matrix[i, j] == 0)
                    {
                        Matrix[i, j] = INF;
                    }
                }
            }

            int mincost = 0; // Cost of min MST. 
            initializeParent();
            // Initialize sets of disjoint sets. 
            for (int i = 0; i < Vertex; i++)
            {
                parent[i] = i;
            }

            // Include minimum weight edges one by one 
            int edge_count = 0;
            while (edge_count < Vertex - 1)
            {
                int min = INF, a = -1, b = -1;
                for (int i = 0; i < Vertex; i++)
                {
                    for (int j = 0; j < Vertex; j++)
                    {
                        if (find(i) != find(j) && Matrix[i, j] < min)
                        {
                            min = Matrix[i, j];
                            a = i;
                            b = j;
                        }
                    }
                }

                union1(a, b);
                Temp[a, b] = min;
                Temp[b, a] = min;
                edge_count++;
                mincost += min;

            }

            for (int i = 0; i < Vertex; i++)
            {
                for (int j = 0; j < Vertex; j++)
                {
                    if (i == j || Matrix[i, j]==INF)
                    {
                        Matrix[i, j] = 0;
                    }
                }
            }

            return mincost;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            for (; ; )
            {
                Console.Clear();
                Console.WriteLine("                         ____________ WELCOME ____________");
                Console.WriteLine("Please enter the number of graph vertices or Enter 0 for EXIT : ");
                int NumberOfVertices;
                try
                {
                    NumberOfVertices = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Please Check your input.");
                    Console.ReadKey();
                    continue;
                }
                if (NumberOfVertices == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Have a good time. :)");
                    Console.ReadKey();
                    return;
                }
                int[,] AdjatencyMatrix = new int[NumberOfVertices, NumberOfVertices];
                int[,] Temp = new int[NumberOfVertices, NumberOfVertices];
                int[,] LaplaceMatrix = new int[NumberOfVertices, NumberOfVertices];
                Matrix.N = NumberOfVertices;
                Kruskal.Vertex = NumberOfVertices;
                Matrix.First_Matrix(AdjatencyMatrix, NumberOfVertices);
                Matrix.Read_Matrix_From_User(AdjatencyMatrix, NumberOfVertices);
                for (; ; )
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a number and Enter 0 for EXIT. ");
                    Console.WriteLine("1) Calculate the number of Spanning trees ");
                    Console.WriteLine("2) Kruskal's algorithm ");
                    Console.WriteLine("3) Edit your Adjatency Matrix  ");
                    Console.WriteLine("4) Bcak to home page ");

                    int UserChoice;
                    try
                    {
                        UserChoice = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Please Check your input.");
                        Console.ReadKey();
                        continue;
                    }

                    if (UserChoice == 4)
                    {
                        break;
                    }
                    switch (UserChoice)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("After calculation we have a Laplace Matrix. ");
                            Console.WriteLine("If we delete a row and a column from the Laplace Matrix,");
                            Console.WriteLine("And calculate determinant ,  ");
                            Console.WriteLine("The result will be the number of Spanning trees");
                            Console.WriteLine("\nYour Adjatency Matrix : \n");
                            Matrix.Display(AdjatencyMatrix, NumberOfVertices, NumberOfVertices);
                            Console.WriteLine("\nLaplace Adjatency Matrix :\n");
                            Matrix.Make_A_Laplace_Matrix(AdjatencyMatrix, LaplaceMatrix, NumberOfVertices);
                            Matrix.Display(LaplaceMatrix, NumberOfVertices, NumberOfVertices);
                            Console.WriteLine();
                            Matrix.Get_Cofactor(LaplaceMatrix, Temp, 1, 1, NumberOfVertices);
                            Console.Write("The number of Spanning trees = ");
                            Console.Write(Matrix.Determinant_Of_Matrix(LaplaceMatrix, NumberOfVertices - 1));
                            Console.ReadKey();
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("After calculation we have Adjatency Matrix Minimum Spanning Tree");
                            Console.WriteLine("\nYour Adjatency Matrix : \n");
                            Matrix.Display(AdjatencyMatrix, NumberOfVertices, NumberOfVertices);
                            Console.WriteLine("\nMinimum cost = {0}\n", Kruskal.kruskalMST(AdjatencyMatrix, Temp));
                            Console.WriteLine("\nAdjatency Matrix Minimum Spanning Tree :\n");
                            Matrix.Display(Temp, NumberOfVertices, NumberOfVertices);
                            Console.ReadKey();
                            break;
                        case 3:
                            Matrix.Read_Matrix_From_User(AdjatencyMatrix, NumberOfVertices);
                            break;
                        case 0:
                            Console.Clear();
                            Console.WriteLine("Have a good time. :)");
                            Console.ReadKey();
                            return;
                        default:
                            Console.Clear();
                            Console.WriteLine("Please Check your input.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
        }
    }
}
