using System.Linq;
using GraphEditor;
using NUnit.Framework;

public class Tests
{
    [Test]
    [TestCase(100, 10, 2,4)]
    public void TestRandomGraphSimplePasses(int count, int nodesCount, int minEdgesCount, int maxEdgesCount)
    {
        for (int i = 0; i < count; i++)
        {
            var randomGraph = UndirectedVertexGraph.GenerateRandomGraph(nodesCount, (minEdgesCount, maxEdgesCount));
            
            Assert.IsTrue(randomGraph.AsReadOnlyNodesDictionary.Values.Min(x => x.NeighboursVertex.Count) >= minEdgesCount);
            Assert.IsTrue(randomGraph.AsReadOnlyNodesDictionary.Values.Max(x => x.NeighboursVertex.Count) <= maxEdgesCount);

            foreach (var node in randomGraph.AsReadOnlyNodesDictionary.Values)
            {
                foreach (var vert in node.NeighboursVertex)
                {
                    Assert.IsTrue(randomGraph.AsReadOnlyNodesDictionary[vert].NeighboursVertex.Contains(node.Vertex));
                }
            }
        }
    }
    
    
    [Test]
    public void TestConnectivityGraph()
    {
        var graph = new UndirectedVertexGraph();
        graph.AddNode(0);
        graph.AddNode(1);
        graph.AddNode(2);
        graph.AddNode(3);
        
        graph.ConnectNodes(0,1);
        graph.ConnectNodes(0,2);
        graph.ConnectNodes(0,3);
        
        Assert.IsTrue(UndirectedVertexGraph.CheckGraphForConnectivity(graph));
    }

    [Test]
    public void TestNotConnectivityGraph()
    {
        var graph = new UndirectedVertexGraph();
        graph.AddNode(0);
        graph.AddNode(1);
        graph.AddNode(2);
        graph.AddNode(3);
        
        Assert.IsFalse(UndirectedVertexGraph.CheckGraphForConnectivity(graph));
    }
}
