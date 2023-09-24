using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public struct InstanceBuffer
{
    public Matrix4x4 worldMatrix;
    public Matrix4x4 worldInverseMatrix;
    public InstanceBuffer(Matrix4x4 worldMatrix)
    {
        this.worldMatrix = worldMatrix;
        this.worldInverseMatrix = worldMatrix.inverse;
    }
}

[Serializable]
public struct InstanceKindData
{
    public int argsIndex;

    //�������͵�result��ʼλ��
    public int kindResultStart;

    //�ж���LOD
    public int lodNum;

    //�����͵�Cluster�ж��ٸ�
    public int elementNum;

    //ֻ����4��LOD
    public Vector4 lodRelative;

    //��Ӱ
    public int argsShadowIndex;

    //��Ӱ��LOD�ڼ���;
    public int shadowLODLevel;

    //��Ӱ��ResultBuffer��ʼλ��
    public int kindShadowResultStart;

    public InstanceKindData(int argsIndex, int kindResultStart, int elementNum, int lodNum, Vector4 lodRelative, int shadowLODLevel)
    {
        this.argsIndex = argsIndex;
        this.kindResultStart = kindResultStart;
        this.elementNum = elementNum;
        this.lodNum = lodNum;
        this.lodRelative = lodRelative;
        this.shadowLODLevel = shadowLODLevel;
        this.argsShadowIndex = 0;
        this.kindShadowResultStart = 0;
    }

    public void SetShadowData(int argsShadowIndex, int kindShadowResultStart)
    {
        this.argsShadowIndex = argsShadowIndex;
        this.kindShadowResultStart = kindShadowResultStart;
    }
}

[Serializable]
public struct InstanceData
{
    public Vector3 center;
    public Vector3 extends;
    //��ǰʵ���ı任�������������
    public int instanceIndex;

    public int instanceKindIndex;

    public int clusterIndex;

    public InstanceData(Bounds bound, int clusterIndex = 0)
    {
        center = bound.center;
        extends = bound.extents;
        instanceIndex = instanceKindIndex = -1;
        this.clusterIndex = clusterIndex;
    }

    public InstanceData(InstanceData other, int instanceIndex, int instanceKindIndex)
    {
        center = other.center;
        extends = other.extends;
        this.instanceIndex = instanceIndex;
        this.instanceKindIndex = instanceKindIndex;
        this.clusterIndex = other.clusterIndex;
    }
    public void SetClusterIndex(int clusterIndex)
    {
        this.clusterIndex = clusterIndex;
    }
}

[Serializable]
public struct ClusterData
{
    //lod0��ChunkLODData��ʼλ��
    public int chunkLODStartIndex;

    //ÿ��LOD��chunkData�ж��ٸ�
    public int chunkLODDataCount;

    //lod0��ClusterChunkData��ʼλ��
    public int chunkStartIndex;

    //��ǰ����instanceData�е�λ��
    public int instanceIndex;

    //��ǰ����ÿ��LOD����������������buffer����ʼλ��
    public Vector4 triangleLODResultData;

    public ClusterData(int chunkLODStartIndex, int chunkLODDataCount, int instanceIndex, int chunkStartIndex, Vector4 triangleLODResultData)
    {
        this.chunkLODStartIndex = chunkLODStartIndex;
        this.chunkLODDataCount = chunkLODDataCount;
        this.instanceIndex = instanceIndex;
        this.chunkStartIndex = chunkStartIndex;
        this.triangleLODResultData = triangleLODResultData;
    }
}

[Serializable]
public struct ClusterChunkLODData
{
    public Vector3 center;
    public Vector3 extends;

    //������������buffer��ʼλ��
    public int triangleStart;

    //����������
    public int triangleNum;

    public ClusterChunkLODData(Vector3 center, Vector3 extends, int triangleNum)
    {
        this.center = center;
        this.extends = extends;
        this.triangleNum = triangleNum;
        triangleStart = 0;
    }
    public void SetData(int triangleStart)
    {
        this.triangleStart = triangleStart;
    }

}

[Serializable]
public struct VertexBuffer
{
    public Vector3 vertex;

    public Vector3 normal;

    public Vector4 color;

    public Vector4 tangent;

    public Vector2 uv0;

    public Vector2 uv1;

    public VertexBuffer(Vector3 vertex, Vector3 normal, Vector4 color, Vector4 tangent, Vector2 uv0, Vector2 uv1)
    {
        this.vertex = vertex;
        this.normal = normal;
        this.color = color;
        this.tangent = tangent;
        this.uv0 = uv0;
        this.uv1 = uv1;
    }
}


