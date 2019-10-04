using System.Collections.Generic;

namespace CluedIn.ExternalSearch.Providers.FacebookGraph.Model
{
	public class Relationship
	{
		public string about { get; set; }
		public string bio { get; set; }
		public List<string> emails { get; set; }
		public string name { get; set; }
		public string category { get; set; }
		public string description { get; set; }
		public string single_line_address { get; set; }
		public string website { get; set; }
		public string personal_info { get; set; }
		public string id { get; set; }
		public string founded { get; set; }
		public string mission { get; set; }
		public string company_overview { get; set; }
		public string birthday { get; set; }
		public string phone { get; set; }
	}
}