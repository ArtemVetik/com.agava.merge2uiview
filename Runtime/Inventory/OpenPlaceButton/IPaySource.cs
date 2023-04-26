namespace Agava.Merge2UIView
{
    public interface IPaySource
    {
        bool Has(int value);
        void Pay(int value);
    }
}