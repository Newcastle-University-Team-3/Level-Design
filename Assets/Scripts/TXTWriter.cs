using System.IO;
using System.Threading.Tasks;
using UnityEngine;

class TXTWriter : MonoBehaviour
{
    private GameObject[] gOArray;

    [SerializeField] string fileName = "Assets/FileOutput/worldCoord.txt";
    void Start()
    {
        gOArray = GameObject.FindGameObjectsWithTag("WorldObject");

        StreamWriter file = new StreamWriter(fileName);

        foreach (GameObject i in gOArray)
        {
            string position = "new Vector3(" + i.transform.position.x + "," + i.transform.position.y + "," + i.transform.position.z + ");";
            Debug.Log(position);
            file.WriteLine(position);
        }
        file.Close();
    }
}
