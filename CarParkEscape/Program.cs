using System;
using System.Linq;


namespace CarParkEscape
{ 
    public class Program
    {

        static void Main(string[] args)
        {
            int[,] carpark = new int[,] { { 1, 0, 0, 0, 2 },
                                          { 0, 0, 0, 0, 0 } };

            escape(carpark);
            Console.WriteLine("Hello World!");
        }

        public static string[] escape(int[,] carpark)
        {
           
            Coordinate? myPosition = null;
            List<Coordinate> stairs = new();
            Coordinate exit = new(carpark.GetLength(0) - 1, carpark.GetLength(1) - 1);
            List<string> Directions = new();

            for (int row = 0; row < carpark.GetLength(0); row++)
            {
                for (int col = 0; col < carpark.GetLength(1); col++)
                {
                    if (carpark[row, col] == 2)
                    {
                        
                        myPosition = new(row, col);
                    }
                    else if (carpark[row, col] == 1)
                    {
                        stairs.Add(new(row, col));
                    }
                }
            }
            
            while(myPosition.Row < carpark.GetLength(0) -1)
            {
                List<Coordinate> stairsOnMyFloor = stairs.FindAll(x => x.Row == myPosition.Row);

                int minDistance = carpark.GetLength(1);

                foreach(Coordinate stair in stairsOnMyFloor)
                {
                    int distance = myPosition.Col - stair.Col;

                    if (Math.Abs(distance) < minDistance) minDistance = distance;
                }

                if(minDistance > 0)
                {
                    Directions.Add(new($"L{minDistance}"));
                    myPosition.Col -= minDistance;
                }
                else if(minDistance < 0)
                {
                    Directions.Add(new($"R{Math.Abs(minDistance)}"));
                    myPosition.Col += Math.Abs(minDistance);
                }
                else
                {
                    int downMoves = 1;
                    myPosition.Row += 1;
                    while(carpark[myPosition.Row, myPosition.Col] == 1)
                    {
                        myPosition.Row += 1;
                        downMoves++;
                    }
                    Directions.Add(new($"D{downMoves}"));
                }

            }

            if(myPosition.Col != (carpark.GetLength(1) -1)) Directions.Add(new($"R{(carpark.GetLength(1)-1) - myPosition.Col}"));

            if(Directions.Count > 0)
                return Directions.ToArray();
            return new string[] { };
        }
    }
}