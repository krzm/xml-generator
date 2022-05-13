namespace Xml.Generator;

public interface IParameterError
{
    void NullParam(params object[] parameters);

    void NullText(string separetedParameters);

    void NoParam<T>(params T[] separator);
}