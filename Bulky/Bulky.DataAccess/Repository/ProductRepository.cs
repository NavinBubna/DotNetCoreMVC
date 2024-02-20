using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBooks.DataAccess.Data;
using BulkyBooks.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<ProductModel> , IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(ProductModel obj)
        {
            _db.Products.Update(obj);
        }
    }
}
