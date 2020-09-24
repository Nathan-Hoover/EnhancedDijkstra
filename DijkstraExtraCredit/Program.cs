using System;

namespace DijkstraExtraCredit
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Shortest Distance for Sample Graph");
            var graph = new ExtraCreditGraph(9);

            graph.AddDirectedEdge(0, 1, 2);
            graph.AddDirectedEdge(0, 3, 4);
            graph.AddDirectedEdge(0, 7, 8);
            graph.AddDirectedEdge(1, 3, 1);
            graph.AddDirectedEdge(1, 5, 3);
            graph.AddDirectedEdge(2, 3, 6);
            graph.AddDirectedEdge(2, 4, 8);
            graph.AddDirectedEdge(2, 5, 4);
            graph.AddDirectedEdge(3, 2, 7);
            graph.AddDirectedEdge(3, 1, 10);
            graph.AddDirectedEdge(4, 5, 4);
            graph.AddDirectedEdge(5, 6, 2);
            graph.AddDirectedEdge(6, 7, 5);
            graph.AddDirectedEdge(7, 8, 9);

            graph.ShortestPath(0);

            Console.WriteLine("Shortest Distance for Graph on Homework 5 question 5");
            var homeworkGraph = new ExtraCreditGraph(10);

            var dijstraGridHW5 = new[,]
            {  //0   1   2   3   4   5   6   7   8   9
                {0  ,4  ,999,3  ,2  ,3  ,3  ,1  ,5  ,2  },//0
                {5  ,0  ,4  ,5  ,4  ,999,999,5  ,5  ,5  },//1
                {5  ,999,0  ,2  ,999,4  ,4  ,4  ,5  ,3  },//2
                {1  ,5  ,4  ,0  ,5  ,999,999,4  ,5  ,4  },//3
                {5  ,999,5  ,999,0  ,1  ,4  ,1  ,3  ,5  },//4
                {4  ,3  ,999,5  ,999,0  ,999,3  ,4  ,1  },//5
                {1  ,1  ,5  ,1  ,2  ,4  ,0  ,5  ,5  ,5  },//6
                {2  ,4  ,1  ,1  ,1  ,999,999,0  ,3  ,4  },//7
                {999,1  ,4  ,3  ,3  ,4  ,5  ,999,0  ,4  },//8
                {4  ,4  ,2  ,3  ,4  ,4  ,999,4  ,5  ,0  } //9
            };

            homeworkGraph.AddAdjacencyGrid(dijstraGridHW5, 10,10);

            homeworkGraph.ShortestPath(4);
        }
    }
}