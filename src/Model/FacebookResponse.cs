using System.Collections.Generic;

namespace CluedIn.ExternalSearch.Providers.FacebookGraph.Model
{
	public class FacebookResponse
	{
		public string about { get; set; }
		public string name { get; set; }
		public string name_with_location_descriptor { get; set; }
		public AppLinks app_links { get; set; }
		public string awards { get; set; }
		public string category { get; set; }
		public List<CategoryList> category_list { get; set; }
		public string company_overview { get; set; }
		public Context context { get; set; }
		public Cover cover { get; set; }
		public string description { get; set; }
		public string description_html { get; set; }
		public string display_subtext { get; set; }
		public List<string> emails { get; set; }
		public Engagement engagement { get; set; }
		public int fan_count { get; set; }
		public string founded { get; set; }
		public string global_brand_page_name { get; set; }
		public string impressum { get; set; }
		public bool is_always_open { get; set; }
		public bool is_community_page { get; set; }
		public bool is_permanently_closed { get; set; }
		public bool is_chain { get; set; }
		public bool is_owned { get; set; }
		public bool has_whatsapp_number { get; set; }
		public string link { get; set; }
		public Location location { get; set; }
		public string mission { get; set; }
		public double overall_star_rating { get; set; }
		public Parking parking { get; set; }
		public string phone { get; set; }
		public string place_type { get; set; }
		public string products { get; set; }
		public string username { get; set; }
		public VoipInfo voip_info { get; set; }
		public string website { get; set; }
		public string id { get; set; }
		public string verification_status { get; set; }
		public Picture picture { get; set; }
	}
}