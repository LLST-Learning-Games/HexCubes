using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
    public int width = 6;
    public int height = 6;

    public HexCell cellPrefab;
    public HexCell[] cells;

    public Text cellLabelPrefab;
    private Canvas gridCanvas;
    private HexMesh hexMesh;

    public Color defaultColor = Color.white;

    private void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[height * width];
        for(int z = 0, i = 0; z < height; z++)
            for(int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
    }

    private void Start()
    {
        hexMesh.Triangulate(cells);
    }



    public void TouchCell(Vector3 position, Color color)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        //Debug.Log("touched at " + coordinates.ToString());

        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        cell.color = color;
        hexMesh.Triangulate(cells);
    }

    private void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * .5f - z / 2) * (HexMetrics.innerRadius * 2f)  ;
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.color = defaultColor;

        if (x > 0)
        {
            cell.SetNeighbour(HexDirection.W, cells[i - 1]);
        }

        if(z > 0)
        {
            if ((z & 1) == 0)
            {
                /*
                What does z & 1 do?

                While && is the boolean AND operator, & is the bitwise AND operator. 
                It performs the same logic, but on each individual pair of bits of its 
                operands. So both bits of a pair need to be 1 for the result to be 1.
                For example, 10101010 & 00001111 yields 00001010.

                Internally, numbers are binary. They only use 0s and 1s. In binary, 
                the sequence 1, 2, 3, 4 is written as 1, 10, 11, 100. As you can see, 
                even number always have 0 as the least significant digit.
        
                We use the binary AND as a mask, ignoring everything except the first bit. 
                If the result is 0, then we have an even number.
                */

                cell.SetNeighbour(HexDirection.SE, cells[i - width]);
                if (x > 0)
                {
                    cell.SetNeighbour(HexDirection.SW, cells[i - width - 1]);
                }
            }

            else
            {
                cell.SetNeighbour(HexDirection.SW, cells[i - width]);
                if (x < width - 1)
                {
                    cell.SetNeighbour(HexDirection.SE, cells[i - width + 1]);
                }
            }

        }

        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
        label.text = cell.coordinates.ToStringOnSeparateLines();
    }
}
