using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
class ObjectToJSON : MonoBehaviour
{
    public int objShape;
    public Vector3 position;
    public Quaternion quaternion;
    public Vector3 scale;
    public Vector3 dimensions;
    public string materialPath;
    public string meshPath;

    private bool colonCheck;

    void Start()
    {
        GameObject[] gOArray = GameObject.FindGameObjectsWithTag("WorldObject");

        StreamWriter file = new StreamWriter("Assets/FileOutput/world.json");

        string objArr = "{\"Objects\":[";
        file.WriteLine(objArr);

        foreach (GameObject i in gOArray)
        {
            ObjectToJSON obj = new ObjectToJSON();
            if (i.GetComponent<ObjectType>())
            {
                obj.objShape = (int)i.GetComponent<ObjectType>().objectShape;
                switch (obj.objShape)
                {
                    case 0:
                        obj.dimensions = new Vector3(1, 0, 0);
                        break;
                    case 1:
                        obj.dimensions = new Vector3(1, 1, 1);
                        break;
                }
            }
            obj.position = i.transform.position;
            obj.quaternion = i.transform.rotation;
            obj.scale = i.transform.localScale;

            if (i.GetComponent<Renderer>().material)
            {
                string temp = "";
                foreach (char c in i.GetComponent<Renderer>().material.name)
                {
                    if (c == ' ')
                        break;
                    temp += c;
                }
                obj.materialPath = temp + ".mat";
            }

            if (i.GetComponent<MeshFilter>().sharedMesh)
            {
                string temp = "";
                foreach (char c in i.GetComponent<MeshFilter>().sharedMesh.name)
                {
                    if (c == ':')
                    {
                        colonCheck = true;
                        continue;
                    }

                    if (!colonCheck)
                        continue;

                    if (c == ' ')
                        break;
                    temp += c;
                }
                obj.meshPath = temp + ".msh";
                colonCheck = false;
            }

            string json = JsonUtility.ToJson(obj);

            Debug.Log(json);

            json += ",";

            file.WriteLine(json);


        }
        objArr = "]}";
        file.WriteLine(objArr);
        file.Close();
    }

}
