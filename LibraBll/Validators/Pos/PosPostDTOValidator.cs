using FluentValidation;
using LibraBll.Common;
using System;

namespace LibraBll.Validators.Pos
{
    public class PosPostDTOValidator : AbstractValidator<PosPostDTO>
    {
		public PosPostDTOValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

			RuleFor(x => x.Telephone)
				.NotEmpty().WithMessage("Missing Telephone")
				.MinimumLength(9).WithMessage("Not a phone number")
				.Matches(@"^\+?[0-9]*$").WithMessage("Phone number should contain only numbers");

			RuleFor(x => x.Telephone)
				.NotEmpty().WithMessage("Missing Telephone")
				.MinimumLength(9).WithMessage("Not a phone number")
				.Matches(@"^\+?[0-9]*$").WithMessage("Phone number should contain only numbers");

			RuleFor(x => x.Address).NotEmpty().WithMessage("Address is required");

			RuleFor(x => x.City).NotEmpty().WithMessage("City is required");

			RuleFor(x => x.Model).NotEmpty().WithMessage("Model is required");

			RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand is required");

			RuleFor(x => x)
				.Must(x => Convert.ToInt32(x.MorningClosing) > Convert.ToInt32(x.MorningOpening))
				.WithMessage("Morning closing must be greater than morning opening");

			//RuleFor(x => x)
			//	.Must(x => Convert.ToInt32(x.AfternoonOpening) > Convert.ToInt32(x.MorningClosing))
			//	.WithMessage("Morning closing must be greater than morning opening");

			//RuleFor(x => x)
			//	.Must(x => Convert.ToInt32(x.AfternoonOpening) > Convert.ToInt32(x.AfternoonOpening))
			//	.WithMessage("Morning closing must be greater than morning opening");

			// RuleFor(x => x.ConnectionType).NotEmpty().WithMessage("Connection Type is required");
			//RuleFor(x => x.MorningOpening).NotEmpty().WithMessage("Morning Opening is required");
			//RuleFor(x => Convert.ToInt32(x.MorningClosing) > Convert.ToInt32(x.MorningOpening))
			//RuleFor(x => x.AfternoonOpening).NotEmpty().WithMessage("Afternoon Opening is required");
			//RuleFor(x => x.AfternoonClosing).NotEmpty().WithMessage("Afternoon Closing is required");
		}
	}
}