using Bereket.Domain;
using Bereket.Domain.Entity;
using Bereket.Repository.Base;

namespace Bereket.Repository.Repos{

    public class ProductRepository : BaseCrudRepository<Product, int>
    {
        public ProductRepository(BereketDbContext context):base(context)
        { }
    }
}