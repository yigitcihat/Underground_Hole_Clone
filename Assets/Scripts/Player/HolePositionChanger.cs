using UnityEngine;

public class HolePositionChanger : MonoBehaviour
{
    [SerializeField] PolygonCollider2D holeCollider2D;
    [SerializeField] PolygonCollider2D groundCollider2D;

    Mesh generatedMesh;
    [SerializeField] MeshCollider generatedMeshCollider;
    [SerializeField] float initialScale = 0.5f;
    private void FixedUpdate()
    {
        if (transform.hasChanged == true)
        {
            transform.hasChanged = false;
            holeCollider2D.transform.position = new Vector2(transform.position.x, transform.position.z);
            holeCollider2D.transform.localScale = transform.localScale * initialScale;
            MakeHole2D();
            Make3DMeshCollider();
        }
    }

    private void MakeHole2D()
    {
        Vector2[] pointPositions = holeCollider2D.GetPath(0);
        for (int i = 0; i < pointPositions.Length; i++)
        {
            pointPositions[i] = holeCollider2D.transform.TransformPoint(pointPositions[i]);
        }
        groundCollider2D.pathCount = 2;
        groundCollider2D.SetPath(1, pointPositions);
    }

    private void Make3DMeshCollider()
    {
        if (generatedMesh != null) Destroy(generatedMesh);
        generatedMesh = groundCollider2D.CreateMesh(true, true);
        generatedMeshCollider.sharedMesh = generatedMesh;
    }


}
