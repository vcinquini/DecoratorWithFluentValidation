using System;
using FluentValidation;

namespace DecoratorWithFluentValidation
{
	internal class CardValidator : AbstractValidator<Customer>
	{
		public CardValidator()
		{
			RuleFor(card => card.Card.CardHolder).NotNull().WithMessage("Please supply the cardholder's name");
			RuleFor(card => card.Card.CardNumber).NotNull().WithMessage("Please supply the card's number");
			RuleFor(card => card.Card.CVV).NotNull().WithMessage("Please supply the card's CVV");

			RuleFor(card => card.Card.ExpirationDate).Custom((expDate, context) => {
				if (String.IsNullOrEmpty(expDate))
				{
					context.AddFailure("please supply the Expiration Date");
				}
			});
		}
	}
}