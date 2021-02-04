using System.Collections.Generic;

namespace TchiboFamilyCircle.Dto
{
    public class Article
    {
        public int ArticleNumber { get; set; }
        public int ProductId { get; set; }
        //public int webshop_product_id { get; set; }
        //public int td_order_number { get; set; }
        public long Ean { get; set; }
        //public string type { get; set; }
        public string Title { get; set; }

        public string ImageUrl { get; set; }
        public string PageUrl { get; set; }

        //public string size { get; set; }
        //public string color { get; set; }
        //public string material { get; set; }
        //public object pattern { get; set; }
        //public string gender { get; set; }
        //public string age { get; set; }
        //public object energy_efficiency { get; set; }
        //public List<string> logos { get; set; }
        //public int project_number { get; set; }
        //public string visibility { get; set; }
        //public bool has_no_productview { get; set; }
        //public Description description { get; set; }
        //public Image image { get; set; }
        //public string ImageDefaultUrl { get; set; }
        //public Category category { get; set; }
        //public Price price { get; set; }
        //public Delivery delivery { get; set; }
        //public Availability availability { get; set; }
        //public Planning planning { get; set; }
        //public Url url { get; set; }

        public string PriceAmount { get; set; }
        public string PriceOldAmount { get; set; }
        public string PriceCurrency { get; set; }
        public string DeliveryDate { get; set; }
    }
}
