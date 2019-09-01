﻿using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace DecoratorWithFluentValidation
{

	public class CustomerValidator : AbstractValidator<Customer>
	{
		public CustomerValidator()
		{
			RuleFor(customer => customer.Forename).NotNull();
			RuleFor(customer => customer.Surname).NotNull();
			RuleFor(customer => customer.Address).NotNull();
			RuleFor(customer => customer.Id).GreaterThan(0);
		}
	}

	public class CustomerAnotherValidator : AbstractValidator<Customer>
	{
		public CustomerAnotherValidator()
		{
			RuleFor(customer => customer.CVV).NotNull();
		}
	}

	public class CustomerOneMoreValidator : AbstractValidator<Customer>
	{
		public CustomerOneMoreValidator()
		{
			RuleFor(customer => customer.Forename).Length(5, 8);
		}
	}
}