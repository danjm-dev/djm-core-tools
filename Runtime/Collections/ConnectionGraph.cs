using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace DJM.CoreTools.Collections
{
    public sealed class ConnectionGraph<T> where T : IEquatable<T>
    {
        private readonly Dictionary<T, NodeData> _graph = new();
        
        public IReadOnlyCollection<T> Nodes => _graph.Keys;

        private bool IsConnectionValid(T nodeA, T nodeB)
        {
            return !nodeA.Equals(nodeB);
        }
        
        private void GetOrCreateConnectionData(T node, out NodeData nodeData)
        {
            if (_graph.TryGetValue(node, out nodeData)) return;
            nodeData = NodeData.Get(node);
            _graph[node] = nodeData;
        }
        
        private void ReleaseConnectionDataIfEmpty(NodeData nodeData)
        {
            if(nodeData.Count > 0) return;
            _graph.Remove(nodeData.Node);
            nodeData.Release();
        }
        
        private void RemoveConnectedNode(T node, T connectedNode)
        {
            if (!_graph.TryGetValue(node, out var connectionData)) return;
            connectionData.RemoveConnection(connectedNode);
            ReleaseConnectionDataIfEmpty(connectionData);
        }

        
        // public API
        
        public void AddConnection(T nodeA, T nodeB)
        {
            if(!IsConnectionValid(nodeA, nodeB)) return;
            
            GetOrCreateConnectionData(nodeA, out var nodeAConnectionData);
            nodeAConnectionData.AddConnection(nodeB);
            
            GetOrCreateConnectionData(nodeB, out var nodeBConnectionData);
            nodeBConnectionData.AddConnection(nodeA);
        }

        public void AddConnections(T node, IEnumerable<T> nodes)
        {
            GetOrCreateConnectionData(node, out var nodeConnectionData);
            
            foreach (var otherNode in nodes)
            {
                if(!IsConnectionValid(node, otherNode)) continue;
                nodeConnectionData.AddConnection(otherNode);
                GetOrCreateConnectionData(otherNode, out var otherNodeConnectionData);
                otherNodeConnectionData.AddConnection(node);
            }
        }

        public void RemoveConnection(T nodeA, T nodeB)
        {
            RemoveConnectedNode(nodeA, nodeB);
            RemoveConnectedNode(nodeB, nodeA);
        }
        
        public void RemoveConnections(T node, IEnumerable<T> connectedNodes)
        {
            if (!_graph.TryGetValue(node, out var connectionData)) return;
            foreach (var connectedNode in connectedNodes)
            {
                connectionData.RemoveConnection(connectedNode);
                RemoveConnectedNode(connectedNode, node);
            }
            ReleaseConnectionDataIfEmpty(connectionData);
        }
        
        public void ClearConnections(T node)
        {
            if (!_graph.Remove(node, out var connectionData)) return;

            foreach (var connectedNode in connectionData)
            {
                RemoveConnectedNode(connectedNode, node);
            }
            
            connectionData.Release();
        }

        public void Collapse(T node)
        {
            if (!_graph.Remove(node, out var connectionData)) return;

            foreach (var connectedNode in connectionData)
            {
                RemoveConnectedNode(connectedNode, node);
                
                foreach (var otherConnectedNode in connectionData)
                {
                    if (connectedNode.Equals(otherConnectedNode)) continue;
                    AddConnection(connectedNode, otherConnectedNode);
                }
            }
            
            connectionData.Release();
        }
        
        public IReadOnlyCollection<T> GetConnectedNodes(T node)
        {
            if(_graph.TryGetValue(node, out var connectionData)) return connectionData;
            return Array.Empty<T>();
        }
        
        public int GetConnectionCount(T node)
        {
            return _graph.TryGetValue(node, out var connectionData) ? connectionData.Count : 0;
        }
        
        public bool ContainsConnection(T nodeA, T nodeB)
        {
            return _graph.TryGetValue(nodeA, out var connectionData) && connectionData.ContainsConnection(nodeB);
        }
        
        
        public void Clear()
        {
            var connectionData = _graph.Values;
            _graph.Clear();
            foreach (var data in connectionData) data.Release();
        }
        
        private sealed class NodeData : IReadOnlyCollection<T>
        {
            private readonly HashSet<T> _connectedNodes = new();
            
            public T Node { get; private set; }
            public int Count => _connectedNodes.Count;

            public bool AddConnection(T node) => _connectedNodes.Add(node);
            public bool RemoveConnection(T node) => _connectedNodes.Remove(node);
            public bool ContainsConnection(T node) => _connectedNodes.Contains(node);

            public IEnumerator<T> GetEnumerator() => _connectedNodes.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            internal static NodeData Get(T node)
            {
                var data = GenericPool<NodeData>.Get();
                data.Node = node;
                return data;
            }
        
            internal void Release()
            {
                _connectedNodes.Clear();
                GenericPool<NodeData>.Release(this);
            }
        }
    }
}