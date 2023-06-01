using AutoMapper;
using LeoShopping.CartAPI.Model;
using LeoShopping.CartAPI.Model.Context;
using LeoShopping.CartAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeoShopping.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MySQLContext _context = null;
        private readonly IMapper _mapper = null;

        public CartRepository(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeader = await _context.CartHeaders
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (cartHeader != null)
            {
                _context.CartDetails
                    .RemoveRange(
                        _context.CartDetails.Where(c => c.CartHeaderId == cartHeader.Id));

                _context.CartHeaders.Remove(cartHeader);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<CartDTO> FindCartbyUserId(string userId)
        {
            Cart cart = new Cart()
            {
                CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId),
            };

            cart.CartDetails = _context.CartDetails
                .Where(c => c.CartHeaderId == cart.CartHeader.Id)
                .Include(c => c.Product);

            return _mapper.Map<CartDTO>(cart);
        }

        public Task<bool> RemoveCoupon(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId)
        {
            try
            {
                CartDetail cartDetail = await _context.CartDetails.FirstOrDefaultAsync(c => c.Id == cartDetailsId);

                int total = _context.CartDetails.Where(c => c.CartHeaderId == cartDetail.CartHeaderId).Count();

                _context.CartDetails.Remove(cartDetail);

                if (total == 1)
                {
                    var cartHeadertoRemove = await _context.CartHeaders
                        .FirstOrDefaultAsync(x => x.Id == cartDetail.CartHeaderId);

                    _context.CartHeaders.Remove(cartHeadertoRemove);

                }
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<CartDTO> SaveOrUpdateCart(CartDTO dto)
        {
            try
            {
                Cart cart = _mapper.Map<Cart>(dto);

                cart.CartDetails = _mapper.Map<List<CartDetail>>(dto.CartDetail);

                var product = await _context.Products.FirstOrDefaultAsync(
                    p => p.Id == dto.CartDetail.FirstOrDefault().ProductId);

                var productAux = cart.CartDetails.FirstOrDefault().Product;

                if (product == null)
                {
                    _context.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                    await _context.SaveChangesAsync();
                }

                var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
                    c => c.UserId == cart.CartHeader.UserId);

                if (cartHeader == null)
                {
                    _context.CartHeaders.Add(cart.CartHeader);
                    await _context.SaveChangesAsync();
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var cartDetail = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                        p => p.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                        p.CartHeaderId == cartHeader.Id);

                    if (cartDetail == null)
                    {
                        cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
                        cart.CartDetails.FirstOrDefault().Product = null;
                        _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        cart.CartDetails.FirstOrDefault().Product = null;
                        cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                        cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                        cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                        _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                        await _context.SaveChangesAsync();
                    }
                }

                return _mapper.Map<CartDTO>(cart);
            }
            catch (Exception _error)
            {

                throw new Exception(_error.Message);
            }
        }
    }
}
