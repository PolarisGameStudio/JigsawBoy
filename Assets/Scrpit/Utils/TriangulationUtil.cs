using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// 判断凹点，凸点，耳朵的比较轴
/// </summary>
public enum CompareAxle
{
    X,
    Y,
    Z
}
/// <summary>
/// 对多边形处理
/// </summary>
public class TriangulationUtil
{
    /// <summary>
    /// 判断凹凸的时候的比对轴
    /// </summary>
    private CompareAxle _compareAxle = CompareAxle.Y;
    /// <summary>
    /// 多边形顶点
    /// </summary>
    private List<Vector3> _polygonVertexs = new List<Vector3>();
    /// <summary>
    /// 顶点序列
    /// </summary>
    private List<int> _vertexsSequence = new List<int>();
    /// <summary>
    /// 节点管理器
    /// </summary>
    private NodeManager _nodeManager = new NodeManager();

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="polygonVertexs">多边形顶点</param>
    public TriangulationUtil(List<Vector3> polygonVertexs)
    {
        this._polygonVertexs = polygonVertexs;
        _nodeManager.Init(polygonVertexs);
    }

    /// <summary>
    /// 设置比较轴
    /// </summary>
    /// <param name="compareAxle"></param>
    public void SetCompareAxle(CompareAxle compareAxle)
    {
        this._compareAxle = compareAxle;
    }

    /// <summary>
    /// 获取三角形的顶点序列
    /// </summary>
    public int[] GetTriangles()
    {
        while (_nodeManager.LinkedListLength >= 3)
        {
            SplitResult sr = SplitPolygon();
            //
            if (sr == null)
            {
                return null;
            }
        }
        return _vertexsSequence.ToArray();
    }

    /// <summary>
    /// 计算凹顶点，凸顶点，耳朵
    /// </summary>
    private SplitResult SplitPolygon()
    {
        //凹点
        List<Node> _concaveVertexs = new List<Node>();
        //凸点
        List<Node> _raisedVertexs = new List<Node>();
        //耳朵
        List<Node> _polygonEars = new List<Node>();
        //起始节点
        Node currentNode = _nodeManager.FirstNode;

        #region 计算凹顶点，凸顶点
        for (int i = 0; i < _nodeManager.LinkedListLength; i++)
        {

            Vector3 one = currentNode.vertex - currentNode.lastNode.vertex;
            Vector3 two = currentNode.nextNode.vertex - currentNode.vertex;
            Vector3 crossRes = Vector3.Cross(one, two);

            if (_compareAxle == CompareAxle.Y)
            {
                if (crossRes.y < 0)
                {
                    _concaveVertexs.Add(currentNode);
                }
                else
                {
                    _raisedVertexs.Add(currentNode);
                }
            }
            if (_compareAxle == CompareAxle.X)
            {
                if (crossRes.x < 0)
                {
                    _concaveVertexs.Add(currentNode);
                }
                else
                {
                    _raisedVertexs.Add(currentNode);
                }
            }
            if (_compareAxle == CompareAxle.Z)
            {
                if (crossRes.z < 0)
                {
                    _concaveVertexs.Add(currentNode);
                }
                else
                {
                    _raisedVertexs.Add(currentNode);
                }
            }

            _polygonEars.Add(currentNode);
            currentNode = currentNode.nextNode;
        }

        for (int i = 0; i < _concaveVertexs.Count; i++)
        {
            _polygonEars.Remove(_concaveVertexs[i]);
        }
        #endregion

        #region 计算耳朵
        for (int i = 0; i < _polygonEars.Count; i++)
        {
            Node earNode = _polygonEars[i];
            Node compareNode = earNode.nextNode.nextNode;
            while (compareNode != earNode.lastNode)
            {
                bool isIn = VertexIsInTriangle(compareNode.vertex, earNode.lastNode.vertex, earNode.vertex, earNode.nextNode.vertex);
                if (isIn == true)
                {
                    if (_polygonEars.Contains(_polygonEars[i]))
                    {
                        _polygonEars.Remove(_polygonEars[i]);
                    }
                    break;
                }
                compareNode = compareNode.nextNode;
            }
        }
        #endregion

        #region 打印初始化结果
        //Debug.Log("凸点");
        //for (int i = 0; i < _raisedVertexs.Count; i++) {
        //    Debug.Log(_raisedVertexs[i].id);
        //}

        //Debug.Log("凹点");
        //for (int i = 0; i < _concaveVertexs.Count; i++) {
        //    Debug.Log(_concaveVertexs[i].id);
        //}

        //Debug.Log("耳朵");
        //for (int i = 0; i < _polygonEars.Count; i++) {
        //    Debug.Log(_polygonEars[i].id);
        //}
        #endregion

        //耳朵为空说明不是简单多边形 多边形三角化失败
        if (_polygonEars.Count == 0)
        {
            return null;
        }

        _vertexsSequence.Add(_polygonEars[0].lastNode.id);
        _vertexsSequence.Add(_polygonEars[0].id);
        _vertexsSequence.Add(_polygonEars[0].nextNode.id);
        _nodeManager.RemoveNode(_polygonEars[0]);
        return new SplitResult(_raisedVertexs, _concaveVertexs, _polygonEars);
    }

    /// <summary>
    /// 计算凹顶点，凸顶点，耳朵
    /// </summary>
    //private SplitResult SplitPolygon(List<Vector3> _polygonVertexs) {

    //    //凹点
    //    List<int> _concaveVertexs = new List<int>();
    //    //凸点
    //    List<int> _raisedVertexs = new List<int>();
    //    //耳朵
    //    List<int> _polygonEars = new List<int>();

    //    #region 计算凹顶点，凸顶点
    //    for (int i = 0; i < _polygonVertexs.Count; i++) {

    //        Vector3 one;
    //        Vector3 two;

    //        if (i == 0) {
    //            one = _polygonVertexs[0] - _polygonVertexs[_polygonVertexs.Count - 1];
    //            two = _polygonVertexs[1] - _polygonVertexs[0];
    //        }
    //        else if (i == _polygonVertexs.Count - 1) {
    //            one = _polygonVertexs[i] - _polygonVertexs[_polygonVertexs.Count - 2];
    //            two = _polygonVertexs[0] - _polygonVertexs[i];
    //        }
    //        else {
    //            one = _polygonVertexs[i] - _polygonVertexs[i - 1];
    //            two = _polygonVertexs[i + 1] - _polygonVertexs[i];
    //        }

    //        Vector3 crossRes = Vector3.Cross(one, two);

    //        if (_compareAxle == CompareAxle.Y) {
    //            if (crossRes.y < 0) {
    //                _concaveVertexs.Add(i);
    //            }
    //            else {
    //                _raisedVertexs.Add(i);
    //            }
    //        }
    //        if (_compareAxle == CompareAxle.X) {
    //            if (crossRes.x < 0) {
    //                _concaveVertexs.Add(i);
    //            }
    //            else {
    //                _raisedVertexs.Add(i);
    //            }
    //        }
    //        if (_compareAxle == CompareAxle.Z) {
    //            if (crossRes.z < 0) {
    //                _concaveVertexs.Add(i);
    //            }
    //            else {
    //                _raisedVertexs.Add(i);
    //            }
    //        }

    //        _polygonEars.Add(i);
    //    }

    //    for (int i = 0; i < _concaveVertexs.Count; i++) {
    //        _polygonEars.Remove(_concaveVertexs[i]);
    //    }
    //    #endregion

    //    #region 计算耳朵
    //    //计算耳朵
    //    for (int i = 0; i < _polygonEars.Count; i++) {
    //        if (_polygonEars[i] == 0) {
    //            for (int j = 0; j < _polygonVertexs.Count; j++) {
    //                if (j == _polygonVertexs.Count - 1 || j == 0 || j == 1) {
    //                    continue;
    //                }
    //                else {
    //                    bool isIn = VertexIsInTriangle(_polygonVertexs[j], _polygonVertexs[_polygonVertexs.Count - 1], _polygonVertexs[0], _polygonVertexs[1]);
    //                    if (isIn == true) {
    //                        if (_polygonEars.Contains(_polygonEars[i])) {
    //                            _polygonEars.Remove(_polygonEars[i]);
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        else if (_polygonEars[i] == _polygonVertexs.Count - 1) {
    //            for (int j = 0; j < _polygonVertexs.Count; j++) {
    //                if (j == _polygonVertexs.Count - 2 || j == _polygonVertexs.Count - 1 || j == 0) {
    //                    continue;
    //                }
    //                else {
    //                    bool isIn = VertexIsInTriangle(_polygonVertexs[j], _polygonVertexs[_polygonVertexs.Count - 2], _polygonVertexs[_polygonVertexs.Count - 1], _polygonVertexs[0]);
    //                    if (isIn == true) {
    //                        if (_polygonEars.Contains(_polygonEars[i])) {
    //                            _polygonEars.Remove(_polygonEars[i]);
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        else {
    //            for (int j = 0; j < _polygonVertexs.Count; j++) {
    //                if (j == _polygonEars[i] - 1 || j == _polygonEars[i] || j == _polygonEars[i] + 1) {
    //                    continue;
    //                }
    //                else {
    //                    bool isIn = VertexIsInTriangle(_polygonVertexs[j], _polygonVertexs[_polygonEars[i] - 1], _polygonVertexs[_polygonEars[i]], _polygonVertexs[_polygonEars[i] + 1]);
    //                    if (isIn == true) {
    //                        if (_polygonEars.Contains(_polygonEars[i])) {
    //                            _polygonEars.Remove(_polygonEars[i]);
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    #endregion

    //    #region 打印初始化结果
    //    //Debug.Log("凸点");
    //    //for (int i = 0; i < _raisedVertexs.Count; i++) {
    //    //    Debug.Log(_raisedVertexs[i]);
    //    //}

    //    //Debug.Log("凹点");
    //    //for (int i = 0; i < _concaveVertexs.Count; i++) {
    //    //    Debug.Log(_concaveVertexs[i]);
    //    //}

    //    //Debug.Log("耳朵");
    //    //for (int i = 0; i < _polygonEars.Count; i++) {
    //    //    Debug.Log(_polygonEars[i]);
    //    //}
    //    #endregion

    //    return new SplitResult(_raisedVertexs, _concaveVertexs, _polygonEars,_polygonVertexs);
    //}

    /// <summary>
    /// 判断一点是否在三角形内
    /// </summary>
    /// <param name="p">一点</param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    private bool VertexIsInTriangle(Vector3 p, Vector3 a, Vector3 b, Vector3 c)
    {

        Vector3 ab = Vector3.Cross((b - a), (p - a));
        Vector3 bc = Vector3.Cross((c - b), (p - b));
        Vector3 ca = Vector3.Cross((a - c), (p - c));

        if (_compareAxle == CompareAxle.Y)
        {
            if (ab.y > 0 && bc.y > 0 && ca.y > 0)
            {
                return true;
            }
        }
        if (_compareAxle == CompareAxle.X)
        {
            if (ab.x > 0 && bc.x > 0 && ca.x > 0)
            {
                return true;
            }
        }
        if (_compareAxle == CompareAxle.Z)
        {
            if (ab.z > 0 && bc.z > 0 && ca.z > 0)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 管理多边形 构成一个双向链表
    /// </summary>
    public class NodeManager
    {

        private List<Node> _nodeList = new List<Node>();

        public int LinkedListLength
        {
            get { return _nodeList.Count; }
        }

        public Node FirstNode
        {
            get { return _nodeList[0]; }
        }

        public void Init(List<Vector3> vertexs)
        {

            for (int i = 0; i < vertexs.Count; i++)
            {
                Node node = new Node(i, vertexs[i]);
                _nodeList.Add(node);
            }

            for (int i = 0; i < LinkedListLength; i++)
            {
                if (i == 0)
                {
                    _nodeList[i].lastNode = _nodeList[LinkedListLength - 1];
                    _nodeList[i].nextNode = _nodeList[1];
                }
                else if (i == LinkedListLength - 1)
                {
                    _nodeList[i].lastNode = _nodeList[LinkedListLength - 2];
                    _nodeList[i].nextNode = _nodeList[0];
                }
                else
                {
                    _nodeList[i].lastNode = _nodeList[i - 1];
                    _nodeList[i].nextNode = _nodeList[i + 1];
                }
            }
        }

        public void RemoveNode(Node node)
        {
            _nodeList.Remove(node);
            node.lastNode.nextNode = node.nextNode;
            node.nextNode.lastNode = node.lastNode;
        }
    }

    public class Node
    {

        public int id;
        public Vector3 vertex;
        public Node lastNode;
        public Node nextNode;

        public Node(int id, Vector3 vertex)
        {
            this.id = id;
            this.vertex = vertex;
        }

        public Node(int id, Vector3 vertex, Node lastNode, Node nextNode)
        {
            this.id = id;
            this.vertex = vertex;
            this.lastNode = lastNode;
            this.nextNode = nextNode;
        }
    }

    public class SplitResult
    {
        /// <summary>
        /// 凸顶点
        /// </summary>
        public List<Node> raisedVertexs;
        /// <summary>
        /// 凹顶点
        /// </summary>
        public List<Node> concaveVertexs;
        /// <summary>
        /// 耳朵
        /// </summary>
        public List<Node> polygonEars;

        public SplitResult(List<Node> raisedVertexs, List<Node> concaveVertexs, List<Node> polygonEars)
        {
            this.raisedVertexs = raisedVertexs;
            this.concaveVertexs = concaveVertexs;
            this.polygonEars = polygonEars;
        }

    }
}
