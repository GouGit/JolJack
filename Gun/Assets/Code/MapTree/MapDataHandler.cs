using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;

public static class MapDataHandler
{
    public static void SaveMap(Spot spot, string filePath)
    {
        XmlDocument document = new XmlDocument();
        document.AppendChild(document.CreateXmlDeclaration("1.0","utf-8","yes"));



        XmlNode root = document.CreateNode(XmlNodeType.Element, "Stage_Test", string.Empty);
        document.AppendChild(root);

        XmlNode child = document.CreateNode(XmlNodeType.Element, "Spots", string.Empty);
        root.AppendChild(child);

    
        SaveMap(spot,document, child);
        

        document.Save(filePath);
    }

    private static void SaveMap(Spot spot, XmlDocument document, XmlNode root)
    {
        if(spot.isTraversal)
            return;

        Debug.Log(spot.sceneOption.testString);

        XmlElement wow = document.CreateElement("spot");
        wow.SetAttribute("string",spot.sceneOption.testString);
        wow.SetAttribute("position", spot.transform.position.ToString());

        spot.isTraversal = true;
        
        int count = spot.nextRoutes.Count;
        for(int i = 0; i < count; i++)
        {
            SaveMap(spot.nextRoutes[i],document, wow);
        }
        root.AppendChild(wow);
    }

    public static void LoadMap(Spot spot, string filePath)
    {
        TextAsset textAsset = Resources.Load("./Assets/Resources/Test.xml"); 
        XmlDocument document = new XmlDocument();

        document.Load(filePath);
        Debug.Log(document.Value);
        XmlElement stage = document["Stage_Test"];
        XmlElement spots = stage["Spots"];
    }

    // private static void LoadMap(Spot spot, XmlDocument document, XmlNode root)
    // {
    //     if(spot.isTraversal)
    //         return;

    //     XmlNode wow = root.FirstChild;

    // }
}
