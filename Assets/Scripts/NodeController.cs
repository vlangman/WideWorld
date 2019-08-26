using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NodeController : MonoBehaviour{

    public int _shopCount;
    public int _eventCount;

    public int _battleCount;
    private List<Node> _nodes;

    public NodeController(){
        _nodes  = new List<Node>();
    }

    public void RandomizeNodes(){
        List<Node> _nodeList = CreateNodeListToGen();
        int count = _nodeList.Count;
        _nodes.Clear();
        for(int i = 0; i < count; i++){
            int index = Random.Range(0,_nodeList.Count);
            _nodes.Add(_nodeList[index]);
            _nodeList.RemoveAt(i);
        }
    }

    public List<Node> CreateNodeListToGen(){
        List<Node> nodes = new List<Node>();
        for(int i = _shopCount; i < _shopCount; i++){
            nodes.Add(new Node(NODE_TYPES.SHOP));
        }
         for(int i = _eventCount; i < _eventCount; i++){
            nodes.Add(new Node(NODE_TYPES.EVENT));
        }
         for(int i = _battleCount; i < _battleCount; i++){
            nodes.Add(new Node(NODE_TYPES.BATTLE));
        }
        return nodes;
    }

    public void GenerateNodes(GameObject _nodePrefab,List<Vector3> spawnPoints, Transform parent){
        RandomizeNodes();
        for(int i = 0; i < spawnPoints.Count; i++){
            Object.Instantiate(_nodePrefab, spawnPoints[i], new Quaternion(0, 0, 0, 0), parent);
        }
    }
}
