namespace ProjectX.Interfaces
{
    public interface IRazorRendererHelper
    {
        string RenderPartialToString<TModel>(string partialName, TModel model);
        string RenderPartialToStringwithout(string partialName);

    }
}
