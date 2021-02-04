using AutoMapper;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using TchiboFamilyCircle.Dto;
using TchiboFamilyCircle.Entities;
using TchiboFamilyCircle.Settings;

namespace TchiboFamilyCircle.DomainService
{
    public class FamilyCircleService : IFamilyCircleService
    {
        private readonly IMapper _mapper;
        private RestClient _restClient;
        private IOccasionService _occasionService;
        public FamilyCircleService(IOptions<AppSettings> settings, IMapper mapper, IOccasionService occasionService)
        {
            _restClient = new RestClient(settings.Value.TchiboApiUrl);
            _mapper = mapper;
            _occasionService = occasionService;
        }
        public IList<string> GetArticles(IList<FamilyMember> familyMembers)
        {
            var request = new RestRequest("api/v1/data", Method.GET);
            request.AddHeader("Accept", "application/json");

            //request.AddParameter("");

            return new List<string>();
        }

        public IList<Article> GetArticlesPerFamilyMember(FamilyMember familyMember, int occasionId)
        {
            var occasion = familyMember.Occasions.Where(x => x.Id == occasionId).FirstOrDefault();

            if (occasion == null)
                return new List<Article>();                       
           
            var deliveryDateEnd = occasion.Date.Subtract(new TimeSpan(2, 0, 0, 0)).ToString("yyyy-MM-dd").Replace("2021", "2020");             

            var request = new RestRequest("api/v1/products", Method.GET);           
            request.AddHeader("Accept", "application/json");

            //check for availability
            //request.AddQueryParameter("filter[availability.default]", "true");
          
            //added check for delivery date in 2 days before occasion
            request.AddQueryParameter("filter[delivery.date][lte]", deliveryDateEnd);
            request.AddQueryParameter("sort", "-delivery.date");

            //add filter on sex: male female
            if (familyMember.Type.IsMale())
            {
                request.AddQueryParameter("filter[gender][eq]", "male");              
            }
            else if (familyMember.Type.IsFemale())
            {
                request.AddQueryParameter("filter[gender][eq]", "female");
            }

            //add search by size
            if (familyMember.Sizes.Any())
            {
                request.AddQueryParameter("filter[has_size]", "true");
               
                //foreach (var size in familyMember.Sizes)
                //{
                //    request.AddQueryParameter("filter[size]", size);
                //}
            }

            try
            {
                var queryResult = _restClient.Execute<DataSetEntity>(request);

                var data = queryResult.Data;

                if (data != null)
                {
                    var articlesList = new List<ArticleEntity>();

                    articlesList.AddRange(data.data);

                    //int currentPage = data.meta.current_page;
                    //do
                    //{
                    //    articlesList.AddRange(data.data);

                    //    request = new RestRequest("api/v1/articles", Method.GET);
                    //    request.AddHeader("Accept", "application/json");                       

                    //    //check for availability
                    //    request.AddQueryParameter("filter[availability.default]", "");

                    //    //added check for delivery date in 2 days before occasion
                    //    request.AddQueryParameter("filter[delivery.date][lte]", deliveryDateEnd);
                    //    request.AddQueryParameter("sort", "-delivery.date");

                    //    currentPage++;
                    //    request.AddQueryParameter("page", currentPage.ToString());

                    //    queryResult = _restClient.Execute<DataSetEntity>(request);

                    //    data = queryResult.Data;
                    //}
                    //while (currentPage<= data.meta.to);

                    var articles = _mapper.Map<IList<ArticleEntity>, IList<Article>>(articlesList);

                    return articles;
                }
            }
            catch (Exception ex)
            { 
                
            }           

            return new List<Article>();
        }
    }
}
