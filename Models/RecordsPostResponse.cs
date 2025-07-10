using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbill.Central.Agent.Plugins.Cloudflare.Models
{
	public class RecordsPostResponse
	{
		public bool success { get; set; }
		public Message[] messages { get; set; }
		public Message[] errors { get; set; }
	}
}
