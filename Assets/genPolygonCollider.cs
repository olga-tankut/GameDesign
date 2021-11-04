using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genPolygonCollider : MonoBehaviour
{
    PolygonCollider2D polygonCollider2D;
    Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        polygonCollider2D = this.GetComponent<PolygonCollider2D>();
        sprite = this.GetComponent<SpriteRenderer>().sprite;
        UpdatePolygonCollider2D();
    }

    // Store these outside the method so it can reuse the Lists (free performance)
    private List<Vector2> points = new List<Vector2>();
    private List<Vector2> simplifiedPoints = new List<Vector2>();
    public void UpdatePolygonCollider2D(float tolerance = 0.05f)
    {
        Debug.Log("Update Polygon Collider");
        polygonCollider2D.pathCount = sprite.GetPhysicsShapeCount();
        for (int i = 0; i < polygonCollider2D.pathCount; i++)
        {
            sprite.GetPhysicsShape(i, points);
            LineUtility.Simplify(points, tolerance, simplifiedPoints);
            polygonCollider2D.SetPath(i, simplifiedPoints);
        }
    }
}
