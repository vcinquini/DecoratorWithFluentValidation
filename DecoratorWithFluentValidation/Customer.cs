using System;
using System.Collections.Generic;
using System.Text;

namespace DecoratorWithFluentValidation
{
	public class Customer
	{
		public int Id { get; set; }
		public string Surname { get; set; }
		public string Forename { get; set; }
		public string Address { get; set; }

		public string CVV { get; set; }
	}
}
