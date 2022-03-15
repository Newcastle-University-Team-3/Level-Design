using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Text;

public class WorldObject_Manager : MonoBehaviour
{
    // The weapon the player has.
    public GameObject Model;

    void Start()
    {
        Save();
    }

    public void Save()
    {
        Debug.Log(Application.persistentDataPath);

        // Stream the file with a File Stream. (Note that File.Create() 'Creates' or 'Overwrites' a file.)
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.dat");
        // Create a new Player_Data.
        WorldObject_Data data = new WorldObject_Data();
        //Save the data.
        data.model = Model;
        data.position = new Vector3(0, 0, 0);

        //Serialize to xml
        DataContractSerializer bf = new DataContractSerializer(data.GetType());
        MemoryStream streamer = new MemoryStream();

        //Serialize the file
        bf.WriteObject(streamer, data);
        streamer.Seek(0, SeekOrigin.Begin);

        //Save to disk
        file.Write(streamer.GetBuffer(), 0, streamer.GetBuffer().Length);

        // Close the file to prevent any corruptions
        file.Close();

        string result = XElement.Parse(Encoding.ASCII.GetString(streamer.GetBuffer()).Replace("\0", "")).ToString();
        Debug.Log("Serialized Result: " + result);

    }
}


[DataContract]
class WorldObject_Data
{
    [DataMember]
    private GameObject _model;

    public GameObject model
    {
        get { return _model; }
        set { _model = value; }
    }

    [DataMember]
    public Vector3 position;
    [DataMember]
    public Quaternion rotation;
    [DataMember]
    public Material material;
}