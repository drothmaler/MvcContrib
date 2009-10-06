namespace MvcContrib.FluentHtml.Elements
{
    internal interface ICheckable<T> where T : ICheckable<T>
    {
        T Checked(bool value);
    }
}