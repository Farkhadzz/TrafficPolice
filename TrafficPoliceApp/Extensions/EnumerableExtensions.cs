namespace TrafficPolice.Extensions;
using System.Text;

public static class EnumerableExtensions
{
    public static string GetHtml<T>(this IEnumerable<T> fines)
    {
        Type type = typeof(T);

        var props = type.GetProperties();

        StringBuilder sb = new StringBuilder(100);
        sb.Append("<ul>");

        foreach (var fine in fines)
        {
            foreach (var prop in props)
            {
                sb.Append($"<li><span>{prop.Name}: </span>{prop.GetValue(fine)}</li>");
            }
            sb.Append("<br/>");
        }
        sb.Append("</ul>");

        return sb.ToString();
    }
}