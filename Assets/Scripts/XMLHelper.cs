using UnityEngine;
using System.Xml;
using System.IO;

public class Record
{
    public string name;
    public int score;

    public Record(string name = "empty", int score = 0)
    {
        this.name = name;
        this.score = score;
    }

    public override string ToString()
    {
        return name + " : " + score.ToString() + "\n";
    }

}

public class XMLHelper
{
    private static string path = Application.persistentDataPath + "/scores.xml";
    private static string root_tag = "Scoreboard";
    private static string child_tag = "Record";
    private static string name_attr = "name";

    private XMLHelper() {}

    public static void Write(Record[] records)
    {
        XmlDocument xml = new XmlDocument();
        XmlElement root = xml.CreateElement(root_tag);
        foreach (Record record in records)
        {
            XmlElement child = xml.CreateElement(child_tag);
            child.SetAttribute(name_attr, record.name);
            child.InnerText = record.score.ToString();
            root.AppendChild(child);
        }
        xml.AppendChild(root);
        xml.Save(path);
    }

    public static Record[] Read(uint size)
    {
        Record[] records = new Record[size];
        int index = 0;

        if (!File.Exists(path))
        {
            while (index < size)
            {
                records[index++] = new Record();
            }
        }
        else
        {
            XmlDocument xml = new XmlDocument(); xml.Load(path);
            XmlNodeList nodes = xml.SelectSingleNode(root_tag).ChildNodes;
            foreach(XmlElement node in nodes)
            {
                records[index] = new Record(node.GetAttribute(name_attr));
                records[index].score = int.Parse(node.InnerText);
                if (++index == size) break;
            }
            while (index < size)
            {
                records[index++] = new Record();
            }
        }
        return records;
    }

}
