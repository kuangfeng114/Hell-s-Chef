using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class SkinnedMeshToCollider : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMesh;
    private MeshCollider meshCollider;
    private Mesh bakedMesh;

    [Header("碰撞体大小调整")]
    [Tooltip("碰撞体缩放倍数，大于1放大，小于1缩小")]
    public float colliderScale = 1.0f;

    void Start()
    {
        skinnedMesh = GetComponent<SkinnedMeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

        if (meshCollider == null)
            meshCollider = gameObject.AddComponent<MeshCollider>();

        bakedMesh = new Mesh();
    }

    void Update()
    {
        // 将当前帧的蒙皮网格"烘焙"到静态网格
        skinnedMesh.BakeMesh(bakedMesh);

        // 应用缩放
        if (Mathf.Abs(colliderScale - 1.0f) > 0.001f)
        {
            Vector3[] vertices = bakedMesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] *= colliderScale;
            }
            bakedMesh.vertices = vertices;
            bakedMesh.RecalculateBounds();
        }

        // 更新碰撞体
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = bakedMesh;
    }

    void OnDestroy()
    {
        if (bakedMesh != null)
            Destroy(bakedMesh);
    }
}