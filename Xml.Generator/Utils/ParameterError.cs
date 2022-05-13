namespace Xml.Generator;

public class ParameterError
    : IParameterError
{
    public void NullParam(params object[] parameters) =>
        Array.ForEach(parameters
            , p => 
            { 
                if (p == null) 
                    throw new ArgumentNullException(nameof(p)
                        , "Parameter cant be null"); 
            });

    public void NullText(string text)
    {
        if (text == null)
            throw new ArgumentException("Provide text to parameter");
    }

    public void NoParam<T>(params T[] separator)
    {
        if (separator == null || separator.Length == 0)
            throw new ArgumentException("Provide at least one parameter");
    }
}