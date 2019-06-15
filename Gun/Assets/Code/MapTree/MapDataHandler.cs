using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;

public static class MapDataHandler
{
    private static void ResetSpot(Spot spot)
    {//  모든 Spot에 지나갔던 흔적을 지웁니다.
        if (!spot.isTraversal)
            return;

        spot.isTraversal = false;

        int count = spot.nextRoutes.Count;
        for (int i = 0; i < count; i++)
        {
            ResetSpot(spot.nextRoutes[i]);
        }
    }

    private static int SetID(Spot spot)
    {// 모든 Spot에 ID를 부여합니다. 그리고 모든 spot의 갯수를 리턴합니다.
        Debug.Log("각각 spot에 ID를 부여합니다.");
        int IDcount = 0;
        SetID(spot, ref IDcount);
        ResetSpot(spot);

        return IDcount;
    }

    private static void SetID(Spot spot, ref int IDcount)
    {
        if (spot.isTraversal)
            return;

        spot.isTraversal = true;

        spot.ID = IDcount++;

        int count = spot.nextRoutes.Count;
        for (int i = 0; i < count; i++)
        {
            SetID(spot.nextRoutes[i], ref IDcount);
        }
    }

    public static void SaveMap(Spot spot, string filePath)
    {
        Debug.Log("맵을 저장합니다.");

        int spotCount = SetID(spot);

        XmlDocument document = new XmlDocument();
        document.AppendChild(document.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        XmlElement root = document.CreateElement("Stage_Test");
        document.AppendChild(root);

        XmlElement child = document.CreateElement("Spots");
        child.SetAttribute("SpotCount", spotCount.ToString());
        root.AppendChild(child);

        SaveMap(spot, document, child);

        ResetSpot(spot);

        document.Save(filePath);
    }

    private static void SaveMap(Spot spot, XmlDocument document, XmlNode root)
    {
        if (spot.isTraversal)
        {// 이미 지나간 Spot 입니다. (트리의 노드가 이어지는 부분 입니다.)
            XmlElement nextSpot = document.CreateElement("nextSpot");
            nextSpot.InnerText = spot.ID.ToString();
            root.AppendChild(nextSpot);

            return;
        }

        XmlElement wow = document.CreateElement("spot");
        Debug.Log(spot.sceneOption.testString);

        XmlElement IDcount = document.CreateElement("ID");
        IDcount.InnerText = spot.ID.ToString();
        wow.AppendChild(IDcount);


        XmlElement str = document.CreateElement("string");
        str.InnerText = spot.sceneOption.testString;
        wow.AppendChild(str);

        XmlElement position = document.CreateElement("position");
        position.InnerText = spot.transform.position.ToString();
        wow.AppendChild(position);

        spot.isTraversal = true;

        int count = spot.nextRoutes.Count;
        for (int i = 0; i < count; i++)
        {
            SaveMap(spot.nextRoutes[i], document, wow);
        }
        root.AppendChild(wow);
    }

    public static void LoadMap(Spot spot, string filePath)
    {
        Debug.Log("맵을 불러옵니다.");
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

        if (!spot.isTraversal)
        {
            spot.isTraversal = true;
            XmlNodeList list = root.SelectNodes("spot");
            int count = spot.nextRoutes.Count;
            for (int i = 0; i < count; i++)
            {
                LoadMap(spot.nextRoutes[i], document, list[i]);
            }
        }
    }

    public static void CreateMap(string filePath)
    {
        TextAsset textAsset = (TextAsset)Resources.Load(filePath);
        Debug.Log(textAsset);
        XmlDocument document = new XmlDocument();

        document.LoadXml(textAsset.text);
        // Debug.Log(document.InnerText);

        
        XmlNode spots = document.SelectSingleNode("Stage_Test/Spots");//document.SelectSingleNode("Stage_Test/Spots");
        int count = (spots as XmlElement).GetAttribute("SpotCount").ToInt();
        //spots.

        // Debug.Log(count);

        List<Spot> spotList = new List<Spot>();

        XmlNode root = spots.SelectSingleNode("spot");
        Debug.Log(root.InnerText);
        CreateMap(document, root, spotList);
    }

    private static Spot CreateMap(XmlDocument document, XmlNode root, List<Spot> spotList)
    {
        int ID = root.SelectSingleNode("ID").InnerText.ToInt();
        Vector3 position = root.SelectSingleNode("position").InnerText.ToVector3();
        position.x += 10;
        XmlNodeList nextSpotList = root.SelectNodes("nextSpot");

        Spot spot = (GameObject.Instantiate(Resources.Load("Spot"), position, Quaternion.identity) as GameObject).GetComponent<Spot>();
        spot.ID = ID;
        spotList.Add(spot);
        
        for(int i = 0; i < nextSpotList.Count;i++)
        {
            spot.nextRoutes.Add(spotList[nextSpotList[i].InnerText.ToInt()]);
        }

        XmlNodeList list = root.SelectNodes("spot");
        int count = list.Count;
        for(int i = 0; i < count; i++)
        {
            spot.nextRoutes.Add(CreateMap(document, list[i], spotList));
        }

        return spot;
    }
}
