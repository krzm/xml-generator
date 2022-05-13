namespace Xml.Generator;

public class XmlObjectCollectionBuilder
{
    private IDictionary<XmlCollectionParts, string> _collectionParts;
    private IDictionary<XmlObjectParts, string> _objectParts;

    public IText CreateXml(
        IDictionary<XmlCollectionParts, string> collectionParts,
        IDictionary<XmlObjectParts, string> objectParts)
    {
        _collectionParts = collectionParts;
        _objectParts = objectParts;
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
                CreateXmlObject(),
                CreateXmlObject());

    private XmlObject CreateXmlObject() =>
        new XmlObject(
            CreateObject());

    private XmlObjectParser CreateObject() =>
        new XmlObjectParser(
            new string[][]
            {
                    new string[]
                    {
                        _objectParts[XmlObjectParts.PropPrefix],
                        _objectParts[XmlObjectParts.Property1],
                        _objectParts[XmlObjectParts.Value1],
                        _objectParts[XmlObjectParts.NewLine]
                    },
                    new string[]
                    {
                        _objectParts[XmlObjectParts.PropPrefix],
                        _objectParts[XmlObjectParts.Property2],
                        _objectParts[XmlObjectParts.Value2],
                        _objectParts[XmlObjectParts.NewLine]
                    }
            }
            , (property) => new XmlPropertyParser(property)
            , new XmlElementParser(
                _objectParts[XmlObjectParts.ObjectPrefix],
                _objectParts[XmlObjectParts.ObjectName],
                _objectParts[XmlObjectParts.NewLine])
            , new XmlElementParser(
                _objectParts[XmlObjectParts.ObjectPrefix],
                _objectParts[XmlObjectParts.ObjectName],
                _objectParts[XmlObjectParts.NewLine]));
}