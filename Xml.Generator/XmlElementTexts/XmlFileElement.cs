﻿namespace Xml.Generator;

public abstract class XmlFileElement : XmlElementsText
{
    protected IText XmlHeader;
    protected IText[] XmlElements;

    protected XmlFileElement(IXmlParser[] xmlParsers, params IText[] xmlElements) : base(xmlParsers)
    {
        if (XmlParsers.Length <= 1)
        {
            CreateHeader();
            XmlElements = xmlElements;
        }
        else if (XmlParsers.Length > 1)
        {
            CreateHeader();
            var list = new List<IText>();

            foreach (var xmlParser in XmlParsers.Skip(1))
            {
                xmlParser.CreateXmlTextObjects();
                list.AddRange(xmlParser.TextObjects);
            }

            list.AddRange(xmlElements);

            XmlElements = list.ToArray();
        }
    }

    private void CreateHeader()
    {
        XmlParsers[0].CreateXmlTextObjects();
        foreach (var textObj in XmlParsers[0].TextObjects)
        {
            if (textObj is XmlHeader)
                XmlHeader = textObj;
        }
    }
}