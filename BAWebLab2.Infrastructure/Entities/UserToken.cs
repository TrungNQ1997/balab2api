using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Infrastructure.Entities
{
    [NotMapped]
    public class UserToken
	{
		public int UserID { get; set; }

		public string Token { get; set; }

		public int CompanyID { get; set; }

		public DateTime ExpiredDate { get; set; }

		public UserToken() { }
	}
}
