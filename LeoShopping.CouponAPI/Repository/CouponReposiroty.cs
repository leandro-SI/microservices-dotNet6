using AutoMapper;
using LeoShopping.CouponAPI.Data.Dtos;
using LeoShopping.CouponAPI.Model.Context;
using LeoShopping.CouponAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeoShopping.CouponAPI.Repository
{
    public class CouponReposiroty : ICouponReposiroty
    {
        private readonly MySQLContext _context = null;
        private readonly IMapper _mapper = null;

        public CouponReposiroty(MySQLContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CouponDTO> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);

            return _mapper.Map<CouponDTO>(coupon);
        }
    }
}
