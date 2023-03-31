using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
[ExecuteInEditMode]
public class MyLineChart : BaseMeshEffect
{
    public Image image;
    [Tooltip("�趨ֵ")]
    /// <summary>
    /// �趨ֵ
    /// </summary>
    public List<float> valueList = new List<float>();
    [Tooltip("��ֵ�ļ��")]
    /// <summary>
    /// ��ֵ�ļ��
    /// </summary>
    public float valueInternal = 1;
    /// <summary>
    /// ����
    /// </summary>
    [Tooltip("���")]
    public float amplitude = 1;
    [Tooltip("�����ܶ�")]
    /// <summary>
    /// ����������ܶ�
    /// </summary>
    [Range(5, 100)]
    public int density = 10;

    /// <summary>
    /// �߿�
    /// </summary>
    public float lineWidth = 1;
    /// <summary>
    /// ��ߵ�
    /// </summary>
    public float heightPos;


    [Tooltip("�ߵ���ɫ")]
    public Color lineColor = Color.white;
    [Tooltip("�����ɫ0")]
    public Color fillColor0 = Color.white;
    [Tooltip("�����ɫ1")]
    public Color fillColor1 = Color.white;
    [Tooltip("�Ƿ���������ɫ")]
    public bool fillAreaColor;






    private List<Vector2> points = new List<Vector2>();
    /// <summary>
    /// ����֮��Ķ�λ��
    /// </summary>
    private List<Vector2> controlPointList = new List<Vector2>();
    //������ĵ������
    private List<Vector3> resList = new List<Vector3>();
    /// <summary>
    /// ���ε��Ľǵĵ�
    /// </summary>
    private List<Vector3> quadAnchorList = new List<Vector3>();

    protected override void Awake()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }

    private void Update()
    {


        image.SetVerticesDirty();
    }
    public override void ModifyMesh(VertexHelper vh)
    {
        vh.Clear();
        points.Clear();
        quadAnchorList.Clear();
        heightPos = 0;


        for (int i = 0; i < valueList.Count; i++)
        {
            points.Add(new Vector2(i * valueInternal, valueList[i] * amplitude));
            //��ǰֵ�������ֵ���滻
            if (valueList[i] * amplitude > heightPos)
            {
                heightPos = valueList[i] * amplitude;
            }
        }



        resList.Clear();
        controlPointList.Clear();

        for (int i = 0; i < points.Count - 1; i++)
        {
            //��ȡ�����м��
            controlPointList.Add(new Vector2((points[i] + points[i + 1]).x / 2, points[i].y));
            controlPointList.Add(new Vector2((points[i] + points[i + 1]).x / 2, points[i + 1].y));

        }







        for (int i = 0; i < points.Count - 1; i++)
        {
            //todo:�����ױ���������,��Ȼ�������ֵĲ���
            //�趨���������߲����ܶ�
            for (int j = 0; j < density; j++)
            {
                resList.Add(BezierMath.Bezier_3(points[i], controlPointList[i * 2], controlPointList[i * 2 + 1], points[i + 1], j * (1 / (float)density)));
            }
        }
        resList.Add(points[points.Count - 1]);


        for (int i = 0; i < resList.Count - 1; i++)
        {
            DrawQuad(vh, resList[i], resList[i + 1], i);
        }
        DrawTriangle(vh);
        FillAreaColor(vh);




    }


    /// <summary>
    /// ��������
    /// </summary>
    public void SetData(List<float> val)
    {
        valueList = val;
    }

    /// <summary>
    /// ������,������λ��,ͬʱ��startindex�����
    /// </summary>
    public void DrawQuad(VertexHelper vh, Vector2 startPos, Vector2 endPos, int index)
    {

        var normal = endPos - startPos;//·������
        float angle = Vector2.SignedAngle(Vector2.right, normal);//���������x��������Ƕ�
        angle *= Mathf.Deg2Rad;//һ��Ҫת�ɻ���
        var _pList = new List<Vector3>();
        Vector3 p0, p1, p2, p3;

        //��������
        float length = Vector2.Distance(startPos, endPos);


        //�ȰѾ�����x�����
        p0 = new Vector3(startPos.x, startPos.y + lineWidth);
        p1 = new Vector3(startPos.x, startPos.y - lineWidth);
        p2 = new Vector3(startPos.x + length, startPos.y + lineWidth);
        p3 = new Vector3(startPos.x + length, startPos.y - lineWidth);
        //�ĸ��㰴��0   3
        //          1   2
        //��˳��
        _pList.Add(p0);
        _pList.Add(p1);
        _pList.Add(p2);
        _pList.Add(p3);

        //Ȼ���ĸ�������startpos��ת

        for (int i = 0; i < _pList.Count; i++)
        {
            //ƽ��
            var _p = new Vector3(_pList[i].x - startPos.x, _pList[i].y - startPos.y, 0);

            //��ת
            _p = new Vector3(Mathf.Cos(angle) * _p.x - Mathf.Sin(angle) * _p.y,
                Mathf.Sin(angle) * _p.x + Mathf.Cos(angle) * _p.y,
                0);

            //��ƽ��
            _p += new Vector3(startPos.x, startPos.y);
            _pList[i] = _p;
        }




        foreach (var i in _pList)
        {
            vh.AddVert(i, lineColor, Vector2.zero);
            //�ӵ������б���
            quadAnchorList.Add(i);
        }


        //���������
        vh.AddTriangle(index * 4, index * 4 + 1, index * 4 + 2);
        vh.AddTriangle(index * 4 + 1, index * 4 + 2, index * 4 + 3);

    }

    /// <summary>
    /// �����뷽��֮���п�϶,�������β���
    /// </summary>
    public void DrawTriangle(VertexHelper vh)
    {
        //0  3 | 0'  3'
        //1  2 | 1'  2'
        //�������������

        //0 | 3  0' | 3'
        //1 | 2  1' | 2'
        //�����м��ٲ�һ�����鲹��

        for (int i = 0; i < quadAnchorList.Count / 4 - 1; i++)
        {
            vh.AddVert(quadAnchorList[i * 4 + 3], lineColor, Vector2.zero);
            vh.AddVert(quadAnchorList[i * 4 + 2], lineColor, Vector2.zero);
            vh.AddVert(quadAnchorList[(i + 1) * 4 + 1], lineColor, Vector2.zero);
            vh.AddVert(quadAnchorList[(i + 1) * 4], lineColor, Vector2.zero);
        }

        for (int i = resList.Count - 1; i < resList.Count * 2 - 3; i++)
        {
            vh.AddTriangle(i * 4, i * 4 + 1, i * 4 + 2);
            vh.AddTriangle(i * 4 + 1, i * 4 + 2, i * 4 + 3);
        }


    }


    /// <summary>
    /// ��������ɫ
    /// </summary>
    public void FillAreaColor(VertexHelper vh)
    {
        if (!fillAreaColor)
        {
            return;
        }

        for (int i = 0; i < resList.Count; i++)
        {
            var p0 = resList[i];
            var p1 = resList[i];
            p1.y = 0;
            vh.AddVert(p0, ComputeColor(p0.y), Vector2.zero);
            vh.AddVert(p1, ComputeColor(p1.y), Vector2.zero);
        }
        //�Ӳ������һ�������ο�ʼ
        //0   2
        //1   3
        int startIndex = (resList.Count * 2 - 3) * 4;
        for (int i = startIndex; i < startIndex + resList.Count * 2 - 2; i += 2)
        {

            var p0 = i;
            var p1 = i + 1;
            var p2 = i + 2;
            var p3 = i + 3;

            vh.AddTriangle(p0, p1, p3);
            vh.AddTriangle(p0, p2, p3);
        }

    }

    /// <summary>
    /// ���㵱ǰ�߶ȵ����ɫ
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private Color ComputeColor(float pos)
    {
        float rate = pos / (heightPos - 0);

        return Color.Lerp(fillColor1, fillColor0, rate);
    }
}

