using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbill.Central.Agent.Plugins.Cloudflare.Models
{
	public class ZoneMeta
	{
		public bool cdn_only { get; set; }
		public bool dns_only { get; set; }
		public bool foundation_dns { get; set; }
		public bool phishing_detected { get; set; }
		
		public int custom_certificate_quota { get; set; }
		public int page_rule_quota { get; set; }
		public int step { get; set; }
	}
}
