using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Runtime.InteropServices;
using System.Web;
namespace LSCommonHelper
{
   public class XMLHelper
{


    public static string XmlReadValue(XmlDocument doc, string Section, string Key)
    {
        XmlNode result = doc.SelectSingleNode("//" + Section);
        string ss = "";
        if (null != result)
        {
            ss = result.SelectSingleNode(Key).InnerText;
        }
        return ss;
    }

    public static string XmlReadValue(XmlDocument doc, string Section, string Section1, string Key)
    {
        try
        {
            XmlNode result = doc.SelectSingleNode("//" + Section);
            if (null != result)
            {
                XmlNodeList childnodes = result.ChildNodes;
                for (int i = 0; i < childnodes.Count; i++)
                {
                    XmlNode node =childnodes[i];
                    if (node.LocalName.Equals(Section1))
                    {
                        return node.SelectSingleNode(Key).InnerText;
                    }
                }
            }
            return "";
        }
        catch
        {
            return "";
        }
    }

    public static string XmlReadValue(XmlDocument doc, string Section, string Section1, string Section2, string Key)
    {
        try
        {
            XmlNode result = doc.SelectSingleNode("//" + Section);
            if (null != result)
            {
                XmlNodeList parentNodes = result.ChildNodes;
                for (int i = 0; i < parentNodes.Count; i++)
                {
                    XmlNode node = parentNodes[i];
                    if (node.LocalName.Equals(Section1))
                    {
                        XmlNodeList childNodes = node.ChildNodes;
                        for (int j = 0; j < childNodes.Count; j++)
                        {
                            XmlNode node2 = childNodes[j];
                            if (node2.LocalName.Equals(Section2))
                            {
                                return node2.SelectSingleNode(Key).InnerText;
                            }
                        }
                    }
                }
            }
            return "";
        }
        catch (Exception)
        {
            return "";
        }
    }

    public static string XmlReadValue(XmlDocument doc, string Section, string Section1, string Section2, string Section3, string Key)
    {
        try
        {
            XmlNode result = doc.SelectSingleNode("//" + Section);
            if (null != result)
            {
                XmlNodeList childnodes = result.ChildNodes;
                for (int i = 0; i < childnodes.Count; i++)
                {
                    XmlNode node = childnodes[i];
                    if (node.LocalName.Equals(Section1))
                    {
                        XmlNodeList childNodes = node.ChildNodes;
                        for (int j = 0; j < childNodes.Count; j++)
                        {
                            XmlNode node2 = childNodes[j];
                            if (node2.LocalName.Equals(Section2))
                            {
                                XmlNodeList list2 = node2.ChildNodes;
                                for (int k = 0; k < list2.Count; k++)
                                {
                                    XmlNode node3 = list2[k];
                                    if (node3.LocalName.Equals(Section3))
                                    {
                                        return node3.SelectSingleNode(Key).InnerText;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return "";
        }
        catch
        {
            return "";
        }
    }

    public static void XmlWriteValue(XmlDocument doc, string sXMLPath, string Section, string Key, string Value)
    {
        XmlNode result = doc.SelectSingleNode("//" + Section);
        if (null != result)
        {
            if (Value == "")
            {
                result.SelectSingleNode(Key).InnerText = "无";
            }
            else
            {
                result.SelectSingleNode(Key).InnerText = Value;
            }
        }
        doc.Save(sXMLPath);
    }

    public static void XmlWriteValue(XmlDocument doc, string sXMLPath, string Section, string sValue1, string Key, string Value)
    {
        XmlNode result = doc.SelectSingleNode("//" + Section);
        if (null != result)
        {
            XmlNodeList childnodes = result.ChildNodes;
            for (int i = 0; i < childnodes.Count; i++)
            {
                XmlNode node = childnodes[i];
                if (node.LocalName.Equals(sValue1))
                {
                    if (Value == "")
                    {
                        node.SelectSingleNode(Key).InnerText = "无";
                    }
                    else
                    {
                        node.SelectSingleNode(Key).InnerText = Value;
                    }
                }
            }
        }
        doc.Save(sXMLPath);
    }

    public static void XmlWriteValue(XmlDocument doc, string sXMLPath, 
        string Section, string sValue1, 
        string sValue2, string Key, string Value)
    {
        XmlNode result = doc.SelectSingleNode("//" + Section);
        if (null != result)
        {
            XmlNodeList parentNodes = result.ChildNodes;
            for (int ii = 0; ii < parentNodes.Count; ii++)
            {
                XmlNode node = parentNodes[ii];
                if (node.LocalName.Equals(sValue1))
                {
                    XmlNodeList childNodes = node.ChildNodes;
                    for (int i = 0; i < childNodes.Count; i++)
                    {
                        XmlNode node2 = childNodes[i];
                        if (node2.LocalName.Equals(sValue2))
                        {
                            if (Value == "")
                            {
                                node2.SelectSingleNode(Key).InnerText = "无";
                            }
                            else
                            {
                                node2.SelectSingleNode(Key).InnerText = Value;
                            }
                        }
                    }
                }
            }
        }
        doc.Save(sXMLPath);
    }

    public static void XmlWriteValue(XmlDocument doc, string sXMLPath,
        string Section, string sValue1, string sValue2, 
        string sValue3, string Key, string Value)
    {
        XmlNode node = doc.SelectSingleNode("//" + Section);
        if (null != node)
        {
            XmlNodeList childNodes = node.ChildNodes;
            for (int i = 0; i < childNodes.Count; i++)
            {
                XmlNode node2 = childNodes[i];
                if (node2.LocalName.Equals(sValue1))
                {
                    XmlNodeList list2 = node2.ChildNodes;
                    for (int j = 0; j < list2.Count; j++)
                    {
                        XmlNode node3 = list2[j];
                        if (node3.LocalName.Equals(sValue2))
                        {
                            XmlNodeList list3 = node3.ChildNodes;
                            for (int k = 0; k < list3.Count; k++)
                            {
                                XmlNode node4 = list3[k];
                                if (node4.LocalName.Equals(sValue3))
                                {
                                    if (Value == "")
                                    {
                                        node4.SelectSingleNode(Key).InnerText = "无";
                                    }
                                    else
                                    {
                                        node4.SelectSingleNode(Key).InnerText = Value;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        doc.Save(sXMLPath);
    }
}


}
