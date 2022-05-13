namespace Xml.Generator;

public class XmlCompositeObjectCollectionBuilder
{
    private IDictionary<XmlCollectionParts, string> _collectionParts;
    private IDictionary<XmlObjectParts, string> _objectParts;
    private IDictionary<XmlObjectParts, string> _innerObjectParts;

    public IText CreateXml(
        IDictionary<XmlCollectionParts, string> collectionParts,
        IDictionary<XmlObjectParts, string> objectParts,
        IDictionary<XmlObjectParts, string> innerObjectParts)
    {
        _collectionParts = collectionParts;
        _objectParts = objectParts;
        _innerObjectParts = innerObjectParts;
        return CreateXmlCollection();
    }

    private XmlCollection CreateXmlCollection() =>
        new XmlCollection(
            new XmlCollectionParser(
                new XmlElementParser(
                    _collectionParts[XmlCollectionParts.Prefix1],
                    _collectionParts[XmlCollectionParts.Name],
                    _collectionParts[XmlCollectionParts.Postfix1]),
                new XmlElementParser(
                    _collectionParts[XmlCollectionParts.Prefix2],
                    _collectionParts[XmlCollectionParts.Name],
                    _collectionParts[XmlCollectionParts.Postfix2])),
                CreateXmlCompositeObject(),
                CreateXmlCompositeObject());

    private XmlObject CreateXmlCompositeObject() =>
        new XmlObject(
            CreateObject(_objectParts),
            CreateObject(_innerObjectParts));

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