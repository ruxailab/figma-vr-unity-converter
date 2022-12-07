using System;
using System.Collections.Generic;
using UnityEngine;

public class Create {
    private int xSize = 40;
    private int ySize = 40;
    private int zSize = 40;
    private Vector3[] vertices;
    private Mesh mesh;
    private Vector3[] normals;
    private int tB;
    private int border = 0;
    private float roundness = 0;

    public GameObject createCubo(float roundness, float border, float height) {
        this.roundness = roundness;
        if(border > height/2)
            this.border = xSize/2;
        else
            this.border = Convert.ToInt32(Math.Round(0+(border-0)*((xSize/2)-0)/((height/2)-0)));

        Material[] materials = new Material[2];
        GameObject gameObject = new GameObject("Cubo");
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        Renderer renderer = gameObject.GetComponent<Renderer>();
        mesh = new Mesh();
        CreateVertices();
        CreateTriangles();
        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
        renderer.materials = materials;
        return gameObject;
    }

    private void CreateVertices() {
        int cornerVertices = 8;
        int edgeVertices = (xSize + ySize + zSize - 3) * 4;
        int faceVertices = (
            (xSize - 1) * (ySize - 1) +
            (xSize - 1) * (zSize - 1) +
            (ySize - 1) * (zSize - 1)) * 2;
        vertices = new Vector3[cornerVertices + edgeVertices + faceVertices];
        normals = new Vector3[vertices.Length];
        
        int index = 0;
        for(int y = 0; y <= ySize; y++) {
            for(int x = 0; x <= xSize; x++)
                SetVertex(index++, x, y, 0);
            for(int z = 1; z <= zSize; z++)
                SetVertex(index++, xSize, y, z);
            for(int x = xSize - 1; x >= 0; x--)
                SetVertex(index++, x, y, zSize);
            for(int z = zSize - 1; z > 0; z--)
                SetVertex(index++, 0, y, z);
        }        
        for(int z = 1; z < zSize; z++) {
            for(int x = 1; x < xSize; x++)
                SetVertex(index++, x, ySize, z);
        }
        for(int z = 1; z< zSize; z++) {
            for(int x = 1; x < xSize; x++)
                SetVertex(index++, x, 0, z);
        }
        mesh.vertices = vertices;
    }

    private void SetVertex(int i, int x, int y, int z) {
        Vector3 inner = vertices[i] = new Vector3(x, y, z);

        if(x < roundness)
            inner.x = roundness;
        else if(x > xSize - roundness)
            inner.x = xSize - roundness;

        if(y < roundness)
            inner.y = roundness;
        else if(y > ySize - roundness)
            inner.y = ySize - roundness;

        if (z < roundness)
			inner.z = roundness;
		else if (z > zSize - roundness)
			inner.z = zSize - roundness;

        normals[i] = (vertices[i] - inner).normalized;
        vertices[i] = inner + normals[i] * roundness;
    }

    private void CreateTriangles() {
        int quads = (xSize * ySize + xSize * zSize + ySize * zSize) * 2;
        int[] triangles = new int [quads * 6];
        int[] trianglesBorder = new int[triangles.Length];
        int ring = (xSize + zSize) * 2;
        int t = 0, v = 0;
        tB = 0;

        for(int y = 0; y < ySize; y++, v++) {
            if(y < border || y >= ySize-border){
                for(int x = 0; x < xSize; x++, v++)
                    tB = SetQuad(trianglesBorder, tB, v, v + 1, v + ring, v + ring + 1);
                for (int z = 0; z < zSize; z++, v++)
	    			tB = SetQuad(trianglesBorder, tB, v, v + 1, v + ring, v + ring + 1);
                for (int x = 0; x < xSize; x++, v++)
                    tB = SetQuad(trianglesBorder, tB, v, v + 1, v + ring, v + ring + 1);
                for (int z = 0; z < zSize-1; z++, v++)
                    tB = SetQuad(trianglesBorder, tB, v, v + 1, v + ring, v + ring + 1);
                tB = SetQuad(trianglesBorder, tB, v, v - ring + 1, v + ring, v + 1);
            }
            else {
                for(int x = 0; x < xSize; x++, v++) {
                    if(x < border || x >= xSize-border)
                        tB = SetQuad(trianglesBorder, tB, v, v + 1, v + ring, v + ring + 1); 
                    else
                        t = SetQuad(triangles, t, v, v + 1, v + ring, v + ring + 1);
                }
                for (int z = 0; z < zSize; z++, v++) {
                    if(z < border || z >= zSize-border)
                        tB = SetQuad(trianglesBorder, tB, v, v + 1, v + ring, v + ring + 1); 
                    else
                        t = SetQuad(triangles, t, v, v + 1, v + ring, v + ring + 1);
                }
                for (int x = 0; x < xSize; x++, v++) {
                    if(x < border || x >= xSize-border)
                        tB = SetQuad(trianglesBorder, tB, v, v + 1, v + ring, v + ring + 1); 
                    else
                        t = SetQuad(triangles, t, v, v + 1, v + ring, v + ring + 1);
                }
                for (int z = 0; z < zSize - 1; z++, v++) {
                    if(z < border || z >= zSize-border)
                        tB = SetQuad(trianglesBorder, tB, v, v + 1, v + ring, v + ring + 1); 
                    else
                        t = SetQuad(triangles, t, v, v + 1, v + ring, v + ring + 1);
                }
                if(border != 0)
                    tB = SetQuad(trianglesBorder, tB, v, v - ring + 1, v + ring, v + 1);
                else
                    t = SetQuad(triangles, t, v, v - ring + 1, v + ring, v + 1);
            }
        }
        t = CreateTopFace(triangles, trianglesBorder, t, ring);
        t = CreateBottomFace(triangles, trianglesBorder, t, ring);
        mesh.subMeshCount = 2;
        mesh.SetTriangles(triangles, 0);
		mesh.SetTriangles(trianglesBorder, 1);
    }

    private int CreateTopFace (int[] triangles, int[] trianglesBorder,int t, int ring) {
		int v = ring * ySize;
        
        if(border != 0) {
            for (int x = 0; x < xSize - 1; x++, v++) {
                tB = SetQuad(trianglesBorder, tB, v, v + 1, v + ring - 1, v + ring);
            }
            tB = SetQuad(trianglesBorder, tB, v, v + 1, v + ring - 1, v + 2);
        }
        else {
            for (int x = 0; x < xSize - 1; x++, v++) {
                t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + ring);
            }
            t = SetQuad(triangles, t, v, v + 1, v + ring - 1, v + 2);
        }

		int vMin = ring * (ySize + 1) - 1;
        int vMid = vMin + 1;
        int vMax = v + 2;
        for (int z = 1; z < zSize - 1; z++, vMin--, vMid++, vMax++) {
			if(border != 0)
                tB = SetQuad(trianglesBorder, tB, vMin, vMid, vMin - 1, vMid + xSize - 1);
            else
                t = SetQuad(triangles, t, vMin, vMid, vMin - 1, vMid + xSize - 1);
			for (int x = 1; x < xSize - 1; x++, vMid++) {
                if(x < border || x >= xSize-border || z < border || z >= xSize-border)
                    tB = SetQuad(trianglesBorder, tB, vMid, vMid + 1, vMid + xSize - 1, vMid + xSize);
                else
				    t = SetQuad(triangles, t, vMid, vMid + 1, vMid + xSize - 1, vMid + xSize);
			}
            if(border != 0)
			    tB = SetQuad(trianglesBorder, tB, vMid, vMax, vMid + xSize - 1, vMax + 1);
            else
                t = SetQuad(triangles, t, vMid, vMax, vMid + xSize - 1, vMax + 1);
		}

        int vTop = vMin - 2;

        if(border !=0) {
            tB = SetQuad(trianglesBorder, tB, vMin, vMid, vTop + 1, vTop);
            for(int x=1; x < xSize - 1; x++, vTop--, vMid++) {
                tB = SetQuad(trianglesBorder, tB, vMid, vMid + 1, vTop, vTop - 1);
            }
            tB = SetQuad(trianglesBorder, tB, vMid, vTop - 2, vTop, vTop - 1);
        }
        else {
            t = SetQuad(triangles, t, vMin, vMid, vTop + 1, vTop);
            for(int x=1; x < xSize - 1; x++, vTop--, vMid++) {
                t = SetQuad(triangles, t, vMid, vMid + 1, vTop, vTop - 1);
            }
            t = SetQuad(triangles, t, vMid, vTop - 2, vTop, vTop - 1);
        }
		return t;
	}

    private int CreateBottomFace (int[] triangles, int[] trianglesBorder, int t, int ring) {
		int v = 1;
		int vMid = vertices.Length - (xSize - 1) * (zSize - 1);
		
        if(border != 0) {
            tB = SetQuad(trianglesBorder, tB, ring - 1, vMid, 0, 1);
            for (int x = 1; x < xSize - 1; x++, v++, vMid++) {
                tB = SetQuad(trianglesBorder, tB, vMid, vMid + 1, v, v + 1);
            }
		    tB = SetQuad(trianglesBorder, tB, vMid, v + 2, v, v + 1);
        }
        else {
            t = SetQuad(triangles, t, ring - 1, vMid, 0, 1);
            for (int x = 1; x < xSize - 1; x++, v++, vMid++) {
                t = SetQuad(triangles, t, vMid, vMid + 1, v, v + 1);
            }
		    t = SetQuad(triangles, t, vMid, v + 2, v, v + 1);
        }

		int vMin = ring - 2;
		vMid -= xSize - 2;
		int vMax = v + 2;

		for (int z = 1; z < zSize - 1; z++, vMin--, vMid++, vMax++) {
            if(border != 0)
			    tB = SetQuad(trianglesBorder, tB, vMin, vMid + xSize - 1, vMin + 1, vMid);
			else
                t = SetQuad(triangles, t, vMin, vMid + xSize - 1, vMin + 1, vMid);
            for (int x = 1; x < xSize - 1; x++, vMid++) {
                if(x < border || x >= xSize-border || z < border || z >= xSize-border)
                    tB = SetQuad(trianglesBorder, tB, vMid + xSize - 1, vMid + xSize, vMid, vMid + 1);
                else
				    t = SetQuad(triangles, t, vMid + xSize - 1, vMid + xSize, vMid, vMid + 1);
			}
            if(border != 0)
			    tB = SetQuad(trianglesBorder, tB, vMid + xSize - 1, vMax + 1, vMid, vMax);
            else
                t = SetQuad(triangles, t, vMid + xSize - 1, vMax + 1, vMid, vMax);
        }

		int vTop = vMin - 1;

        if(border != 0) {
            tB = SetQuad(trianglesBorder, tB, vTop + 1, vTop, vTop + 2, vMid);
            for (int x = 1; x < xSize - 1; x++, vTop--, vMid++) {
                tB = SetQuad(trianglesBorder, tB, vTop, vTop - 1, vMid, vMid + 1);
            }
            tB = SetQuad(trianglesBorder, tB, vTop, vTop - 1, vMid, vTop - 2);
        }
        else {
            t = SetQuad(triangles, t, vTop + 1, vTop, vTop + 2, vMid);
            for (int x = 1; x < xSize - 1; x++, vTop--, vMid++) {
                t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vMid + 1);
            }
            t = SetQuad(triangles, t, vTop, vTop - 1, vMid, vTop - 2);
        }
		return t;
	}

    private static int SetQuad (int[] triangles, int i, int v00, int v10, int v01, int v11) {
		triangles[i] = v00;
		triangles[i + 1] = triangles[i + 4] = v01;
		triangles[i + 2] = triangles[i + 3] = v10;
		triangles[i + 5] = v11;
		return i + 6;
	}
}