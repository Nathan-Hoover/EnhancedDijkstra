using System;
using System.Collections.Generic;
using System.Linq;

namespace DijkstraExtraCredit
{
    /// <summary>
    /// A simple Graph structure that contains an optimized version of
    /// Dijkstra’s Shortest Path Algorithm using a priority queue (MinHeap)
    /// </summary>
    public class ExtraCreditGraph
    {
        // Constructor that initializes AdjacencyList based off the numberOfVertices
        public ExtraCreditGraph(int numberOfVertices)
        {
            NumberOfVertices = numberOfVertices;
            AdjacencyList = new List<Tuple<int, int>>[numberOfVertices];

            // Initialize lists
            for (var index = 0; index < AdjacencyList.Length; index++)
            {
                AdjacencyList[index] = new List<Tuple<int, int>>();
            }
        }

        private int NumberOfVertices { get; }

        // Stores vertex and weight pair for every edge
        private List<Tuple<int, int>>[] AdjacencyList { get; }

        // Adds an adjacency grid to the Graph
        public void AddAdjacencyGrid(int[,] grid, int numberOfRows, int numberOfColumns)
        {
            for (var i = 0; i < numberOfRows; i++)
            {
                for (var j = 0; j < numberOfColumns; j++)
                {
                    if (i != j && grid[i, j] != 999)
                    {
                        AddDirectedEdge(i, j, grid[i, j]);
                    }
                }
            }
        }

        // Adds an undirected edge going one way to the AdjacencyList
        public void AddUndirectedEdge(int vertexU, int vertexV, int weight)
        {
            AddDirectedEdge(vertexV, vertexU, weight);
            AddDirectedEdge(vertexU, vertexV, weight);
        }

        // Adds an edge going one way to the AdjacencyList
        public void AddDirectedEdge(int vertexU, int vertexV, int weight)
        {
            AdjacencyList[vertexU].Add(new Tuple<int, int>(vertexV, weight));
        }

        /// <summary>
        /// Dijkstra’s Shortest Path Algorithm using a priority queue (MinHeap)
        /// </summary>
        /// <param name="sourceVertex">The vertex to determine shortest path from</param>
        public void ShortestPath(int sourceVertex)
        {
            // Stores the shortest distance to every vertex
            var shortestDistances = new int[NumberOfVertices];

            // Stores what vertexes has been extracted from the priority queue
            var vertexExtracted = new bool[NumberOfVertices];
            for (var i = 0; i < NumberOfVertices; i++)
            {
                // Initialize to a high number to make proposed distances
                // always lower than the initialized distance
                shortestDistances[i] = 999;

                // Initialize to false so the vertex is calculated
                vertexExtracted[i] = false;
            }

            // Distance to source from source is always zero
            shortestDistances[sourceVertex] = 0;

            //// Source Vertex distance is already zero
            //vertexExtracted[sourceVertex] = true;

            // This priority queue will store the vertex with
            // its priority representing its weight
            var priorityQueue = new PriorityQueue<int>(true);

            // Insert sourceVertex into priority queue with distance of 0
            priorityQueue.Enqueue(0, sourceVertex);

            // Loop until the queue is empty
            while (priorityQueue.Count > 0)
            {
                // Dequeue the shortest distance vertex from the priority queue
                var vertexU = priorityQueue.Dequeue();
                // Mark the vertex that was dequeued as extracted
                vertexExtracted[vertexU] = true;

                // Loop through all adjacent edges to vertexU
                for (var i = 0; i < AdjacencyList[vertexU].Count; ++i)
                {
                    // The adjacent vertex and weight of vertexU as a tuple
                    var tuple = AdjacencyList[vertexU].ElementAt(i);

                    // Vertex V of the adjacent edge
                    var vertexV = tuple.Item1;
                    // Weight of the adjacent edge
                    var weight = tuple.Item2;

                    // Current shortest distance that we are trying to improve
                    var currentDistance = shortestDistances[vertexV];

                    // Proposed shortest distance made of distance to adjacent vertex
                    // plus its weight value
                    var proposedDistance = shortestDistances[vertexU] + weight;

                    // If the current distance is smaller or
                    // vertexV has already been extracted from the Priority Queue
                    // then skip to the next iteration of the for loop
                    if (vertexExtracted[vertexV] || currentDistance <= proposedDistance)
                    {
                        continue;
                    }

                    // Store new shortest distance for vertexV
                    shortestDistances[vertexV] = proposedDistance;
                    // Enqueue distance to travel to vertexV from sourceVertex
                    priorityQueue.Enqueue(shortestDistances[vertexV], vertexV);
                }
            }

            PrintShortestDistances(shortestDistances, sourceVertex);
        }

        private static void PrintShortestDistances(IReadOnlyList<int> shortestDistances, in int sourceVertex)
        {
            Console.WriteLine($"Shortest Distances from vertex {sourceVertex}");
            for (var i = 0; i < shortestDistances.Count; i++)
            {
                Console.WriteLine($"{sourceVertex} => {i}: " + shortestDistances[i]);
            }
        }
    }
}