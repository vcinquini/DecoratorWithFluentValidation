using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace DecoratorWithFluentValidation
{

	public class CustomerValidator : AbstractValidator<Customer>
	{
		public CustomerValidator()
		{
			RuleFor(customer => customer.FirstName).NotNull();
			RuleFor(customer => customer.LastName).NotNull();
			RuleFor(customer => customer.Address).NotNull();
			RuleFor(customer => customer.Id).GreaterThan(0);
		}
	}

	public class AnotherCustomerValidator : AbstractValidator<Customer>
	{
		public AnotherCustomerValidator()
		{
			RuleFor(customer => customer.Card).NotNull();
		}
	}

	public class OneMoreCustomerValidator : AbstractValidator<Customer>
	{
		public OneMoreCustomerValidator()
		{
			RuleFor(customer => customer.FirstName + customer.LastName).Length(10, 100);
		}
	}
}
