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
        XmlElement wow = document.CreateElement("spot");

        Debug.Log(spot.sceneOption.testString);

        XmlElement str = document.CreateElement("string");
        str.InnerText = spot.sceneOption.testString;
        wow.AppendChild(str);

        XmlElement position = document.CreateElement("position");
        position.InnerText = spot.transform.position.ToString();
        wow.AppendChild(position);

        if(!spot.isTraversal)
        {
            spot.isTraversal = true;
            
            int count = spot.nextRoutes.Count;
            for(int i = 0; i < count; i++)
            {
                SaveMap(spot.nextRoutes[i],document, wow);
            }
        }

        root.AppendChild(wow);
    }

    public static void LoadMap(Spot spot, string filePath)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(filePath); 
        Debug.Log(textAsset);
        XmlDocument document = new XmlDocument();

        document.LoadXml(textAsset.text);
        Debug.Log(document.InnerText);
        
        XmlNode root = document.SelectSingleNode("Stage_Test/Spots/spot");

        LoadMap(spot, document, root);
    }

    private static void LoadMap(Spot spot, XmlDocument document, XmlNode root)
    {
        Debug.Log(spot);
        spot.sceneOption.testString = root.SelectSingleNode("string").InnerText;
        // spot.transform.position = root.SelectSingleNode("position").InnerText.;

        if(!spot.isTraversal)
        {
            spot.isTraversal = true;
            XmlNodeList list = root.SelectNodes("spot");
            int count = spot.nextRoutes.Count;
            for(int i = 0; i < count; i++)
            {
                LoadMap(spot.nextRoutes[i], document, list[i]);
            }
        }
    }
}
