namespace Checkout.Contracts
{
    public interface IProductRepository
    {
        IProduct GetById(string SKU);

        void Save(IProduct product);
    }
}
