using AutoMapper;
using LeoShopping.CouponAPI.Data.Dtos;
using LeoShopping.CouponAPI.Model;

namespace LeoShopping.CouponAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDTO, Coupon>().ReverseMap();

            });

            return mappingConfig;
        }
    }
}
