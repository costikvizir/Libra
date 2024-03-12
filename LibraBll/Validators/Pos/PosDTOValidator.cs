using FluentValidation;
using LibraBll.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraBll.Validators.Pos
{
	public class PosDTOValidator : AbstractValidator<PosDTO>
	{
		public PosDTOValidator()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Missing Name");
			RuleFor(x => x.Address).NotEmpty().WithMessage("Missing Address");
			RuleFor(x => x.Telephone).NotEmpty().WithMessage("Missing Telephone");
			RuleFor(x => x.Cellphone).NotEmpty().WithMessage("Missing Cellphone");
			RuleFor(x => x.City).NotEmpty().WithMessage("Missing City");
			RuleFor(x => x.Model).NotEmpty().WithMessage("Missing Model");
			RuleFor(x => x.Brand).NotEmpty().WithMessage("Missing Brand");
			RuleFor(x => x.ConnectionTypeId).NotEmpty().WithMessage("Missing Connection Type");
			RuleFor(x => x.MorningOpening).NotEmpty().WithMessage("Missing Morning Opening");
			RuleFor(x => x.MorningClosing).NotEmpty().WithMessage("Missing Morning Closing");
			RuleFor(x => x.AfternoonOpening).NotEmpty().WithMessage("Missing Afternoon Opening");
			RuleFor(x => x.AfternoonClosing).NotEmpty().WithMessage("Missing Afternoon Closing");
			RuleFor(x => x.DaysClosed).NotEmpty().WithMessage("Missing Days Closed");
			RuleFor(x => x.InsertDate).NotEmpty().WithMessage("Missing Insert Date");

		}

	}
}
