namespace TechnicalStation.UI.Contract
{
    public interface IControlFactory
    {
        T Create<T>();
    }
}
