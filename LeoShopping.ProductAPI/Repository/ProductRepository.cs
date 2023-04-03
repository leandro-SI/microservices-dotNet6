using AutoMapper;
using LeoShopping.ProductAPI.Data.Dtos;
using LeoShopping.ProductAPI.Model;
using LeoShopping.ProductAPI.Model.Context;
using LeoShopping.ProductAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeoShopping.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySQLContext _context = null;
        private readonly IMapper _mapper = null;

        public ProductRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> FindAll()
        {
            List<Product> products = await _context.Products.ToListAsync();

            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<ProductDTO> FindById(long id)
        {
            Product product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> Create(ProductDTO dto)
        {
            Product product = _mapper.Map<Product>(dto);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> Update(ProductDTO dto)
        {
            Product product = _mapper.Map<Product>(dto);

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> DeleteById(long id)
        {
            try
            {
                Product product = await _context.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

                if (product == null) return false;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
