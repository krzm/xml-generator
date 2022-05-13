namespace Xml.Generator;

public class XmlCompositeObjectCollectionBuilder
{
    private IDictionary<XmlCollectionParts, string>? collectionParts;
    private IDictionary<XmlObjectParts, string>? objectParts;
    private IDictionary<XmlObjectParts, string>? innerObjectParts;

    public IText CreateXml(
        IDictionary<XmlCollectionParts, string> collectionParts,
        IDictionary<XmlObjectParts, string> objectParts,
        IDictionary<XmlObjectParts, string> innerObjectParts)
    {
        this.collectionParts = collectionParts;
        this.objectParts = objectParts;
        this.innerObjectParts = innerObjectParts;
        return CreateXmlCollection();
    }

    private XmlCollection CreateXmlCollection()
    {
        ArgumentNullException.ThrowIfNull(collectionParts);
        return new XmlCollection(
            new XmlCollectionParser(
                new XmlElementParser(
                    collectionParts[XmlCollectionParts.Prefix1],
                    collectionParts[XmlCollectionParts.Name],
                    collectionParts[XmlCollectionParts.Postfix1]),
                new XmlElementParser(
                    collectionParts[XmlCollectionParts.Prefix2],
                    collectionParts[XmlCollectionParts.Name],
                    collectionParts[XmlCollectionParts.Postfix2])),
                CreateXmlCompositeObject(),
                CreateXmlCompositeObject());
    }
        

    private XmlObject CreateXmlCompositeObject()
    {
        ArgumentNullException.ThrowIfNull(objectParts);
        ArgumentNullException.ThrowIfNull(innerObjectParts);
        return new XmlObject(
            CreateObject(objectParts),
            CreateObject(innerObjectParts));
    }

    private XmlObjectParser CreateObject(IDictionary<XmlObjectParts, string> objectParts) =>
        new XmlObjectParser(
            new string[][]
            {
                new string[]
                {
                    objectParts[XmlObjectParts.PropPrefix],
                    objectParts[XmlObjectParts.Property1],
                    objectParts[XmlObjectParts.Value1],
                    objectParts[XmlObjectParts.NewLine]
                },
                new string[]
                {
                    objectParts[XmlObjectParts.PropPrefix],
                    objectParts[XmlObjectParts.Property2],
                    objectParts[XmlObjectParts.Value2],
                    objectParts[XmlObjectParts.NewLine]
                }
            }
            , (property) => new XmlPropertyParser(property)
            , new XmlElementParser(
                objectParts[XmlObjectParts.ObjectPrefix],
                objectParts[XmlObjectParts.ObjectName],
                objectParts[XmlObjectParts.NewLine])
            , new XmlElementParser(
                objectParts[XmlObjectParts.ObjectPrefix],
                objectParts[XmlObjectParts.ObjectName],
                objectParts[XmlObjectParts.NewLine])
        );
}