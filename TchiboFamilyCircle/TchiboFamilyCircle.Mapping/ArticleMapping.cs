using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TchiboFamilyCircle.Dto;
using TchiboFamilyCircle.Entities;

namespace TchiboFamilyCircle.Mapping
{
    public class ArticleMapping : Profile
    {
        public ArticleMapping()
        {
            CreateMap<ArticleEntity, Article>()
                .ForMember(destination => destination.ArticleNumber, option => option.MapFrom(source => source.tcm_article_number))
                .ForMember(destination => destination.ProductId, option => option.MapFrom(source => source.product_id))
                .ForMember(destination => destination.Ean, option => option.MapFrom(source => source.ean))
                .ForMember(destination => destination.Title, option => option.MapFrom(source => source.title))
                .ForMember(destination => destination.ImageUrl, option => option.MapFrom(source => source.image.@default))
                .ForMember(destination => destination.PageUrl, option => option.MapFrom(source => string.Format("https://www.tchibo.de/{0}-p{1}.html", source.title.ToLower().Replace(" ", "-"), source.product_id)))
                .ForMember(destination => destination.PriceAmount, option => option.MapFrom(source => source.price.amount.Substring(0, source.price.amount.IndexOf('.'))))
                .ForMember(destination => destination.PriceAmountFractal, option => option.MapFrom(source => source.price.amount.Substring(source.price.amount.IndexOf('.') + 1, 2)))
                .ForMember(destination => destination.PriceOldAmount, option => option.MapFrom(source => source.price.old_amount.Substring(0, source.price.old_amount.IndexOf('.'))))
                .ForMember(destination => destination.PriceOldAmountFractal, option => option.MapFrom(source => source.price.old_amount.Substring(source.price.old_amount.IndexOf('.') + 1, 2)))
                .ForMember(destination => destination.PriceCurrency, option => option.MapFrom(source => source.price.currency))
                .ForMember(destination => destination.DeliveryDate, option => option.MapFrom(source => source.delivery.date.Replace("2020", "2021")));

        }
    }
}
