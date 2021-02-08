using AutoMapper;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<FamilyCircleService> _logger;
        private readonly IMapper _mapper;
        private RestClient _restClient;
        private IOccasionService _occasionService;
        private IDictionary<Occasion, Article> _flowersPerOccassion = new Dictionary<Occasion, Article>
        {
            { new Occasion { Id = 1, Name = "Birthday"}, new Article { ArticleNumber = 1607414798, ProductId = 20003025, Ean = 1607414798, Title = "Tulpen Mix 30 Stiele", ImageUrl = "https://static.blume2000.de/pictures/20003025/472_Tulpen-Mix-30-Stiele.jpg?1607414798", PageUrl = "https://www.blume2000.de/geburtstagsblumen/?searchref=geburtstag", PriceAmount = "17", PriceAmountFractal = "99", PriceOldAmount = "", PriceOldAmountFractal = "", PriceCurrency = "EUR" } },
            { new Occasion { Id = 4, Name = "Mother'sDay" }, new Article { ArticleNumber = 1599226749, ProductId = 20001956, Ean = 1599226749, Title = "Tulpentraum", ImageUrl = "https://static.blume2000.de/pictures/20001956/472_Pinke-Tulpen-mit-Eukalyptus.jpg?1599226749", PageUrl = "https://www.blume2000.de/blumen-muttertag/", PriceAmount = "19", PriceAmountFractal = "99", PriceOldAmount = "", PriceOldAmountFractal = "", PriceCurrency = "EUR" } },
            { new Occasion { Id = 6, Name = "Anniversary"}, new Article { ArticleNumber = 1599227103, ProductId = 20002304, Ean = 1599227103, Title = "Frühlingsglück ", ImageUrl = "https://static.blume2000.de/pictures/20002304/472_Fruehlingserwachen.jpg?1599227103", PageUrl = "https://tchibo.blume2000.de/jubilaeum/", PriceAmount = "22", PriceAmountFractal = "99", PriceOldAmount = "", PriceOldAmountFractal = "", PriceCurrency = "EUR" } },
            { new Occasion { Id = 9, Name = "Graduation"}, new Article { ArticleNumber = 1599230793, ProductId = 20002144, Ean = 1599230793, Title = "Magischer Moment ", ImageUrl = "https://static.blume2000.de/pictures/20002144/047_Magischer-Moment.jpg?15992307853", PageUrl = "https://www.blume2000.de/abschied/", PriceAmount = "24", PriceAmountFractal = "99", PriceOldAmount = "", PriceOldAmountFractal = "", PriceCurrency = "EUR" } },
        };

        //8 artilces
        private IList<Article> _articlesHausWarming = new List<Article>
        {
            new Article {
                ArticleNumber = 400164285,
                ProductId = 400164285,
                Ean = 400164285,
                Title = "9 selbstklebende Spiegelsticker",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/1eeb406eb459ddf1/.jpg",
                PageUrl = "https://www.tchibo.de/9-selbstklebende-spiegelsticker-p400164285.html",
                PriceAmount = "8",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
            new Article {
                ArticleNumber = 400166676,
                ProductId = 400166676,
                Ean = 400166676,
                Title = "Badwanduhr mit Thermometer",
                ImageUrl = "https://www.tchibo.de//newmedia/art_img/PRODUCT_QUARTER-CENSHARE/b99adfacdb59467f/badwanduhr-mit-thermometer.jpg",
                PageUrl = "https://www.tchibo.de/badwanduhr-mit-thermometer-p400166676.html",
                PriceAmount = "17",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
             new Article {
                ArticleNumber = 400152619,
                ProductId = 400152619,
                Ean = 400152619,
                Title = "LED-Leuchte mit Farbwechsel",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/29b46a87f1b0a85/.jpg",
                PageUrl = "https://www.tchibo.de/led-leuchte-mit-farbwechsel-p400152619.html",
                PriceAmount = "12",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
               new Article {
                ArticleNumber = 400164526,
                ProductId = 400164526,
                Ean = 400164526,
                Title = "Dekokissen mit Füllung, ca. 45 x 45 cm",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/b99b614a183b910f/dekokissen-mit-fuellung-ca-45-x-45-cm.jpg",
                PageUrl = "https://www.tchibo.de/dekokissen-mit-fuellung-ca-45-x-45-cm-p400164526.html",
                PriceAmount = "29",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
                new Article {
                ArticleNumber = 400142477,
                ProductId = 400142477,
                Ean = 400142477,
                Title = "Tischdecke",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/a83c384695e29b37/.jpg",
                PageUrl = "https://www.tchibo.de/tischdecke-p400142477.html",
                PriceAmount = "15",
                PriceAmountFractal = "00",
                PriceOldAmount = "19",
                PriceOldAmountFractal = "99",
                PriceCurrency = "EUR"
            } ,
                 new Article {
                ArticleNumber = 400149845,
                ProductId = 400149845,
                Ean = 400149845,
                Title = "Aufbewahrungstruhe, klein",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/5c8af2b875eea856/.jpg",
                PageUrl = "https://www.tchibo.de/aufbewahrungstruhe-klein-p400149845.html",
                PriceAmount = "15",
                PriceAmountFractal = "00",
                PriceOldAmount = "19",
                PriceOldAmountFractal = "99",
                PriceCurrency = "EUR"
            } ,
                  new Article {
                ArticleNumber = 400144106,
                ProductId = 400144106,
                Ean = 400144106,
                Title = "Teppich",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/af4995c72b0869e7/teppich.jpg",
                PageUrl = "https://www.tchibo.de/teppich-p400144106.html",
                PriceAmount = "39",
                PriceAmountFractal = "00",
                PriceOldAmount = "49",
                PriceOldAmountFractal = "95",
                PriceCurrency = "EUR"
            } ,

            new Article {
                ArticleNumber = 400163683,
                ProductId = 400163683,
                Ean = 400163683,
                Title = "Schuhregal, schwarz",
                ImageUrl = "https://www.tchibo.de//newmedia/art_img/PRODUCT_QUARTER-CENSHARE/b99f763e0f4e33df/schuhregal-schwarz.jpg",
                PageUrl = "https://www.tchibo.de/schuhregal-schwarz-p400163683.html",
                PriceAmount = "49",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            }
        };

        //4 artilces
        private IList<Article> _articlesGarden = new List<Article>
        {
            new Article {
                    ArticleNumber = 400158525,
                    ProductId = 400158525,
                    Ean = 400158525,
                    Title = "3 Kletterpflanzen",
                    ImageUrl = "https://www.tchibo.de//newmedia/art_img/PRODUCT_QUARTER-CENSHARE/5e5af06dddcb09e6/.jpg",
                    PageUrl = "https://www.tchibo.de/3-kletterpflanzen-p400158525.html",
                    PriceAmount = "26",
                    PriceAmountFractal = "45",
                    PriceOldAmount = "",
                    PriceOldAmountFractal = "",
                    PriceCurrency = "EUR"
                } ,
              new Article {
                ArticleNumber = 400165217,
                ProductId = 400165217,
                Ean = 400165217,
                Title = "Wolf-Unkrautstecher und Gartenhandschuhe",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/68d806dd9db0a85/wolf-unkrautstecher-und-gartenhandschuhe.jpg",
                PageUrl = "https://www.tchibo.de/wolf-unkrautstecher-und-gartenhandschuhe-p400165217.html",
                PriceAmount = "44",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
                new Article {
                ArticleNumber = 400158262,
                ProductId = 400158262,
                Ean = 400158262,
                Title = "Premium-Schutzhülle für Strandkörbe XL",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/68c31db1abdc855/.jpg",
                PageUrl = "https://www.tchibo.de/premium-schutzhuelle-fuer-strandkoerbe-xl-p400158262.html",
                PriceAmount = "49",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
                  new Article {
                ArticleNumber = 400142630,
                ProductId = 400142630,
                Ean = 400142630,
                Title = "Breites Teleskop-Gartenregal",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/859337187453207b/breites-teleskop-gartenregal.jpg",
                PageUrl = "https://www.tchibo.de/breites-teleskop-gartenregal-p400142630.html",
                PriceAmount = "24",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } 
        };

        //4 artilces
        private readonly IList<Article> _articlesCooking = new List<Article>
        {
            new Article {
                    ArticleNumber = 400155976,
                    ProductId = 400155976,
                    Ean = 400155976,
                    Title = "Küchenschürze",
                    ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/684147dd9d32855/.jpg",
                    PageUrl = "https://www.tchibo.de/kuechenschuerze-p400155976.html",
                    PriceAmount = "12",
                    PriceAmountFractal = "99",
                    PriceOldAmount = "",
                    PriceOldAmountFractal = "",
                    PriceCurrency = "EUR"
                } ,
              new Article {
                ArticleNumber = 400161262,
                ProductId = 400161262,
                Ean = 400161262,
                Title = "Gusseisen-Servierpfanne",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/4ffec78fbefb9d3e/.jpg",
                PageUrl = "https://www.tchibo.de/gusseisen-servierpfanne-p400161262.html",
                PriceAmount = "39",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
                new Article {
                ArticleNumber = 400086797,
                ProductId = 400086797,
                Ean = 400086797,
                Title = "Edelstahl-Pfanne, 28 cm",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-IMPORTED/fea33479cb032e94/edelstahl-pfanne-28-cm.jpg",
                PageUrl = "https://www.tchibo.de/edelstahl-pfanne-p400086797.html",
                PriceAmount = "34",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
                new Article {
                ArticleNumber = 400137049,
                ProductId = 400137049,
                Ean = 400137049,
                Title = "Aluguss-Bräter",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/89a3384f0c7c79eb/.jpg",
                PageUrl = "https://www.tchibo.de/aluguss-braeter-p400137049.html",
                PriceAmount = "39",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
        };

        //4 articles 
        private readonly IList<Article> _articlesBacking = new List<Article> 
        {
              new Article {
                ArticleNumber = 400160684,
                ProductId = 400160684,
                Ean = 400160684,
                Title = "GOURMETmaxx Küchenmaschine, 1.500 W",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/29f5069c5eab9e5/gourmetmaxx-kuechenmaschine-1-500-w.jpg",
                PageUrl = "https://www.tchibo.de/gourmetmaxx-kuechenmaschine-1-500-w-p400160684.html",
                PriceAmount = "99",
                PriceAmountFractal = "95",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
                new Article {
                ArticleNumber = 400141244,
                ProductId = 400141244,
                Ean = 400141244,
                Title = "Platzsparendes Backformen-Set",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/aae9906aaf1c7f27/platzsparendes-backformen-set.jpg",
                PageUrl = "https://www.tchibo.de/platzsparendes-backformen-set-p400141244.html",
                PriceAmount = "39",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
                  new Article {
                ArticleNumber = 402006468,
                ProductId = 402006468,
                Ean = 402006468,
                Title = "BODUM® Barista-Waage »BISTRO« mit LCD-Anzeige",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/b36adfa97f4ce10f/.jpg",
                PageUrl = "https://www.tchibo.de/bodum-barista-waage-bistro-mit-lcd-anzeige-p402006468.html",
                PriceAmount = "49",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
            new Article {
                ArticleNumber = 402006429,
                ProductId = 402006429,
                Ean = 402006429,
                Title = "BODUM® Küchenmaschine »BISTRO«, ca. 4,7 l, 700 W",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/b999b398cf4ce10f/.jpg",
                PageUrl = "https://www.tchibo.de/bodum-kuechenmaschine-bistro-ca-4-7-l-700-w-p402006429.html",
                PriceAmount = "259",
                PriceAmountFractal = "00",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } 
        };

        //4 articles
        private readonly IList<Article> _articlesFemaleYoga = new List<Article> 
        {
             new Article {
                ArticleNumber = 402003160,
                ProductId = 402003160,
                Ean = 402003160,
                Title = "Tchibo Yoga – Jetzt Mitglied werden 12-Monats-Mitgliedschaft",
                ImageUrl = "https://www.tchibo.de/media/newmedia/structure_node/search/488ece1cdc257c8e/2020kw47_tchibo_fitness_gutschein_de_search_263x225.jpg",
                PageUrl = "https://www.tchibo.de/tchibo-fitness-mitglied-werden-c402003160.html",
                PriceAmount = "99",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
              new Article {
                ArticleNumber = 402003160,
                ProductId = 402003160,
                Ean = 402003160,
                Title = "Tchibo Fitness – Jetzt Mitglied werden 12-Monats-Mitgliedschaft",
                ImageUrl = "https://www.tchibo.de/newmedia/page/img/4889b228c984c10e/image_classic.jpg",
                PageUrl = "https://www.tchibo.de/tchibo-fitness-mitglied-werden-c402003160.html",
                PriceAmount = "69",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
              new Article {
                ArticleNumber = 402011144,
                ProductId = 402011144,
                Ean = 402011144,
                Title = "Yoga- und Fitnessmatte",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/bc380fad6d98f0bf/.jpg",
                PageUrl = "https://www.tchibo.de/yoga-und-fitnessmatte-p402011144.html?dim1=s004",
                PriceAmount = "19",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
               new Article {
                ArticleNumber = 400157320,
                ProductId = 400157320,
                Ean = 400157320,
                Title = "Sportrucksack für yogamatt",
                ImageUrl = "https://www.tchibo.de//newmedia/art_img/MAIN-CENSHARE/1b4b41debdceac41/.jpg",
                PageUrl =  "https://www.tchibo.de/sportrucksack-p400157320.html",
                PriceAmount = "25",
                PriceAmountFractal = "50",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } 
        };

        //2 articles
        private readonly IList<Article> _articlesCoffeeMaschines = new List<Article> 
        {
              new Article {
                ArticleNumber = 402006504,
                ProductId = 402006504,
                Ean = 402006504,
                Title = "Tchibo Kaffeevollautomat »Esperto Caffè«, Anthrazit",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/COFFEE_TILE_THIRD-IMPORTED/4e4ad14e0f4947ce/tchibo-kaffeevollautomat-esperto-caffe-anthrazit.jpg",
                PageUrl = "https://www.tchibo.de/tchibo-kaffeevollautomat-esperto-caffe-p402006504.html",
                PriceAmount = "219",
                PriceAmountFractal = "00",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
                new Article {
                ArticleNumber = 402009684,
                ProductId = 402009684,
                Ean = 402009684,
                Title = "Cafissimo easy PETROL",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/COFFEE_TILE_THIRD-IMPORTED/4be0263fbf4947ce/cafissimo-easy-petrol.jpg",
                PageUrl = "https://www.tchibo.de/cafissimo-easy-p402009684.html",
                PriceAmount = "39",
                PriceAmountFractal = "00",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
        };

        //2 articles traveling
        private readonly IList<Article> _articlesTraveling = new List<Article>
        {
                new Article {
                ArticleNumber = 0,
                ProductId = 0,
                Ean = 0,
                Title = "Skireisen Tour",
                ImageUrl = "https://reisen.tchibo.de/media/resize=w:640,h:386,f:crop,a:faces/output=q:60,f:pjpg/compress/VpAZXnnSI6oLhb7a62WA",
                PageUrl = "https://reisen.tchibo.de/reisearten/skireisen",
                PriceAmount = "ab 199",
                PriceAmountFractal = "00",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
                 new Article {
                ArticleNumber = 400162369,
                ProductId = 400162369,
                Ean = 400162369,
                Title = "Ski- und Snowboard-Rucksack",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/466020fbad9943de/.jpg",
                PageUrl = "https://www.tchibo.de/ski-und-snowboard-rucksack-p400162369.html",
                PriceAmount = "50",
                PriceAmountFractal = "00",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
            new Article {
                ArticleNumber = 400151967,
                ProductId = 400151967,
                Ean = 400151967,
                Title = "Hartschalen-Koffer, groß",
                ImageUrl = "https://www.tchibo.de/newmedia/art_img/PRODUCT_QUARTER-CENSHARE/f54c32bb1c7c7b34/hartschalen-koffer-gross.jpg",
                PageUrl = "https://www.tchibo.de/hartschalen-koffer-gross-p400151967.html",
                PriceAmount = "79",
                PriceAmountFractal = "99",
                PriceOldAmount = "",
                PriceOldAmountFractal = "",
                PriceCurrency = "EUR"
            } ,
        };

        public FamilyCircleService(ILogger<FamilyCircleService> logger, IOptions<AppSettings> settings, IMapper mapper, IOccasionService occasionService)
        {
            _logger = logger;
            _restClient = new RestClient(settings.Value.TchiboApiUrl);
            _mapper = mapper;
            _occasionService = occasionService;
        }       

        public IList<Article> GetArticlesPerFamilyMember(FamilyMember familyMember, int occasionId)
        {
            var articles = new List<Article>();

            var occasion = familyMember.Occasions.Where(x => x.Id == occasionId).FirstOrDefault();

            if (occasion == null)
                return articles;

            var deliveryDateEnd = occasion.Date.Subtract(new TimeSpan(2, 0, 0, 0)).ToString("yyyy-MM-dd");

            //----------------------------------------------Add flowers to items---------------------------------------------------
            if (familyMember.Type.IsFemale() && occasion.CanBeFlowers())
            {
                var occasionKey = new Occasion { Id = occasion.Id, Name = occasion.Name };

                var flowerArticles = _flowersPerOccassion.Where(x => x.Key.Id == occasion.Id).Select(y => y.Value).FirstOrDefault();

                if (flowerArticles != null)
                {
                    flowerArticles.DeliveryDate = deliveryDateEnd;
                    articles.Add(flowerArticles);
                }
            }

            //-------------------------------------------- Add proposals for traveling (weekend travel options, suite cases)--------
            if (familyMember.Interests.Contains("traveling"))
            {
                foreach (var article in _articlesTraveling)
                {
                    article.DeliveryDate = deliveryDateEnd;
                }
                articles.AddRange(_articlesTraveling);
            }


            //--------------------------------------------Add request for bithdat male and female
            var request = new RestRequest("api/v1/products", Method.GET);
            request.AddHeader("Accept", "application/json");

            if (occasion.Name == "Birthday" || occasion.Name == "Anniversary")
            {
                //add filter on sex: male female
                if (familyMember.Type.IsMale())
                {
                    request.AddQueryParameter("filter[gender][eq]", "male");
                    request.AddQueryParameter("filter[price.amount][lte]", "350");
                    request.AddQueryParameter("filter[price.amount][gte]", "200");
                }
                else if (familyMember.Type.IsFemale())
                {
                    request.AddQueryParameter("filter[gender][eq]", "female");
                    request.AddQueryParameter("filter[price.amount][lte]", "500");
                    request.AddQueryParameter("filter[price.amount][gte]", "350");
                }

                //add search by size
                if (familyMember.Sizes.Any())
                {
                    request.AddQueryParameter("filter[has_size]", "true");
                }

                //add sorting by price
                request.AddQueryParameter("sort", "-price.amount");

                var articlesBirthday = GetArticlesBySearchCriteria(request);

                if (articlesBirthday != null && articlesBirthday.Any())
                {
                    var articlesMapped = _mapper.Map<IList<ArticleEntity>, IList<Article>>(articlesBirthday);
                    articles.AddRange(articlesMapped);
                }
            }

            //---------------------------------------------Add items by interests ------------------------------------------------------
            if (familyMember.Interests.Contains("yoga"))
            {
                foreach (var article in _articlesFemaleYoga)
                {
                    article.DeliveryDate = deliveryDateEnd;
                }
                articles.AddRange(_articlesFemaleYoga);
            }

            if (familyMember.Interests.Contains("cooking"))
            {
                foreach (var article in _articlesCooking)
                {
                    article.DeliveryDate = deliveryDateEnd;
                }
                articles.AddRange(_articlesCooking);
            }

            if (familyMember.Interests.Contains("garden"))
            {
                foreach (var article in _articlesGarden)
                {
                    article.DeliveryDate = deliveryDateEnd;
                }
                articles.AddRange(_articlesGarden);
            }

            if (familyMember.Interests.Contains("backing"))
            {
                foreach (var article in _articlesBacking)
                {
                    article.DeliveryDate = deliveryDateEnd;
                }
                articles.AddRange(_articlesBacking);
            }

            if (occasion.Name == "Housewarming")
            {
                foreach (var article in _articlesHausWarming)
                {
                    article.DeliveryDate = deliveryDateEnd;
                }
                articles.AddRange(_articlesHausWarming);
            }           

            if (occasion.Name == "Graduation")
            {
                //add coffe machines
                foreach (var article in _articlesCoffeeMaschines)
                {
                    article.DeliveryDate = deliveryDateEnd;
                }
                articles.AddRange(_articlesCoffeeMaschines);

                request = new RestRequest("api/v1/products", Method.GET);
                request.AddHeader("Accept", "application/json");
                request.AddQueryParameter("type", "coffee");
                          
                var articlesCoffee = GetArticlesBySearchCriteria(request);

                if (articlesCoffee != null && articlesCoffee.Any())
                {
                    var articlesMapped = _mapper.Map<IList<ArticleEntity>, IList<Article>>(articlesCoffee);
                    articles.AddRange(articlesMapped);
                }
            }

            return articles;
        }

        private IList<ArticleEntity> GetArticlesBySearchCriteria(IRestRequest restRequest)
        {
            try
            {
                var queryResult = _restClient.Execute<DataSetEntity>(restRequest);

                var data = queryResult.Data;

                if (data != null)
                {
                    return data.data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occured during sending request to TchiboApi", ex);
            }

            return new List<ArticleEntity>();
        }
    }
}
