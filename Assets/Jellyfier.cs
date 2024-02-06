using UnityEngine;

public class Jellyfier : MonoBehaviour
{
    //A value that describes how fast our jelly object will be bouncing
    public float bounceSpeed;
    public float fallForce;

    //We need this value to eventually stop bouncing back and forth.
    public float stiffness;

    //We need our Meshfilter to get a hold of the mesh;
    private MeshFilter meshFilter;
    private Mesh mesh;


    JellyVertex[] jellyVertices;
    Vector3[] currentMeshVertices;

    private void Start(){
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;

        GetVertices();
    }

    private void Update(){
        UpdateVertices();
    }

    private void UpdateVertices()
    {
        for(int i = 0; i < jellyVertices.Length; i++){
            jellyVertices[i].UpdateVelocity(bounceSpeed);
            jellyVertices[i].Settle(stiffness);
            
            jellyVertices[i].currentVertexPosition += jellyVertices[i].currentVelocity * Time.deltaTime;
            currentMeshVertices[i] = jellyVertices[i].currentVertexPosition;
        }

        mesh.vertices = currentMeshVertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
    }

    private void GetVertices()
    {
        jellyVertices = new JellyVertex[mesh.vertices.Length];
        currentMeshVertices = new Vector3[mesh.vertices.Length];
        for(int i = 0; i < mesh.vertices.Length; i++){
            jellyVertices[i] = new JellyVertex(i, mesh.vertices[i], mesh.vertices[i], Vector3.zero);
            currentMeshVertices[i] = mesh.vertices[i];
        }
    }

    public void OnCollisionEnter(Collision _collision){
        ContactPoint[] collisionPoints = _collision.contacts;
        for(int i = 0; i < collisionPoints.Length; i++){
            Vector3 inputPoint = collisionPoints[i].point + (collisionPoints[i].point * .1f);
            ApplyPressureToPoint(inputPoint, fallForce);
        }
    }

    private void ApplyPressureToPoint(Vector3 _point, float _pressure)
    {
        for(int i = 0; i < jellyVertices.Length; i++){
            jellyVertices[i].ApplyPressureToVertex(transform, _point, _pressure);
        }
    }
}
