using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public bool creation;
    public GameObject mousePosition;
    public GameObject[] objects;
    public GameObject wall;
    private GameObject pendingObject;
    private GameObject startObject;
    private Vector3 pos;
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {

        if (pendingObject != null)
        {

            pos.y = 0;
            pendingObject.transform.position = pos;
            mousePosition.transform.position = pos;
            if (Input.GetMouseButtonDown(0))
            {
                if (creation)
                {
                    startObject = pendingObject;
                    pendingObject = null;
                    pendingObject = Instantiate(startObject, pos, Quaternion.identity);
                    creation = false;
                }
                else
                {
                    startObject.transform.LookAt(pendingObject.transform);
                    print(startObject.transform.position.x + " " + startObject.transform.position.y + " " + startObject.transform.position.z);
                    print(pendingObject.transform.position.x + " " + pendingObject.transform.position.y + " " + pendingObject.transform.position.z);
                    Vector3 vec = new Vector3((pendingObject.transform.position.x + startObject.transform.position.x)/2,0, (pendingObject.transform.position.z + startObject.transform.position.z)/2);
                    wall = Instantiate(pendingObject, vec, Quaternion.identity);
                    wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, Vector3.Distance(pendingObject.transform.position, startObject.transform.position));
                    wall.transform.LookAt(startObject.transform);
                    pendingObject = null;
                    pendingObject = Instantiate(objects[0], pos, Quaternion.identity);
                    creation = true;
                    startObject = null;
                    wall = null;
                }
        
            }
        }
    }


    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layerMask)) {
            pos = hit.point;
        }
    }

    public void SelectObject(int index) {
        pendingObject = Instantiate(objects[index], pos, Quaternion.identity);
        mousePosition = Instantiate(mousePosition, pos, Quaternion.identity);
        startObject = pendingObject;
        creation = true;
    }
}
