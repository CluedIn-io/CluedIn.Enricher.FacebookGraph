namespace CluedIn.ExternalSearch.Providers.FacebookGraph.Model
{
	public class VoipInfo
	{
		public bool has_permission { get; set; }
		public bool has_mobile_app { get; set; }
		public bool is_pushable { get; set; }
		public bool is_callable { get; set; }
		public bool is_callable_webrtc { get; set; }
		public int reason_code { get; set; }
		public string reason_description { get; set; }
	}
}