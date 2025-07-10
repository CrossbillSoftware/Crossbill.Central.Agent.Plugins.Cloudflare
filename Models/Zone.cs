namespace Crossbill.Central.Agent.Plugins.Cloudflare.Models
{
	public class Zone
	{
		public Account account { get; set; }
		public string activated_on { get; set; }
		public string created_on { get; set; }
		public int development_mode { get; set; }
		public string id { get; set; }
		public ZoneMeta meta { get; set; }
		public string modified_on { get; set; }
		public string name { get; set; }
		public string[] name_servers { get; set; }
		public string original_dnshost { get; set; }
		public string[] original_name_servers { get; set; }
		public string original_registrar { get; set; }
		public ZoneOwner owner { get; set; }
		public string[] vanity_name_servers { get; set; }
	}
}
