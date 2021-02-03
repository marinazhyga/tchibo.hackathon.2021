using System.Collections.Generic;

namespace TchiboFamilyCircle.Entities
{
    public class Description
    {
        public object @default { get; set; }
        public string @long { get; set; }
        public string structured { get; set; }
    }

    public class Image
    {
        public string @default { get; set; }
        public List<string> gallery { get; set; }
        public object isolated { get; set; }
    }

    public class Category
    {
        public string @default { get; set; }
        public string google_shopping_api { get; set; }
        public string retargeting { get; set; }
        public string assortment1 { get; set; }
        public string assortment2 { get; set; }
        public string assortment3 { get; set; }
        public string assortment4 { get; set; }
    }

    public class Price
    {
        public string old_amount { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
    }

    public class Delivery
    {
        public string amount { get; set; }
        public string currency { get; set; }
        public string date { get; set; }
        public bool affiliate_delivery_possible { get; set; }
        public bool depot_delivery_possible { get; set; }
    }

    public class Availability
    {
        public bool @default { get; set; }
        public bool affiliate { get; set; }
        public bool back_in_stock { get; set; }
    }

    public class Planning
    {
        public object week { get; set; }
        public object subshop { get; set; }
        public object order_channel_code { get; set; }
    }

    public class Url
    {
        public string @default { get; set; }
        public string mobile { get; set; }
    }

    public class Article
    {
        public int tcm_article_number { get; set; }
        public int product_id { get; set; }
        public int webshop_product_id { get; set; }
        public int td_order_number { get; set; }
        public object ean { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public object size { get; set; }
        public string color { get; set; }
        public string material { get; set; }
        public object pattern { get; set; }
        public string gender { get; set; }
        public string age { get; set; }
        public object energy_efficiency { get; set; }
        public List<string> logos { get; set; }
        public int project_number { get; set; }
        public string visibility { get; set; }
        public bool has_no_productview { get; set; }
        public Description description { get; set; }
        public Image image { get; set; }
        public Category category { get; set; }
        public Price price { get; set; }
        public Delivery delivery { get; set; }
        public Availability availability { get; set; }
        public Planning planning { get; set; }
        public Url url { get; set; }
    }

    public class Links
    {
        public string first { get; set; }
        public object last { get; set; }
        public object prev { get; set; }
        public string next { get; set; }
    }

    public class Meta
    {
        public int current_page { get; set; }
        public int from { get; set; }
        public string path { get; set; }
        public int per_page { get; set; }
        public int to { get; set; }
    }

    public class DataSetEntity
    {
        public List<Article> data { get; set; }
        public Links links { get; set; }
        public Meta meta { get; set; }
    }
}
