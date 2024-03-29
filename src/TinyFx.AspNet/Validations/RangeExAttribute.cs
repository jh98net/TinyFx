﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Net;

namespace TinyFx.AspNet
{
    public class RangeExAttribute : RangeAttribute
    {
        public string Code { get; set; }
        public RangeExAttribute(int minimum, int maximum, string code, string message = null)
            : base(minimum, maximum)
        {
            Code = code ?? ResponseCode.G_BadRequest;
            ErrorMessage = message;
        }
        public RangeExAttribute(double minimum, double maximum, string code, string message = null)
            : base(minimum, maximum)
        {
            Code = code ?? ResponseCode.G_BadRequest;
            ErrorMessage = message;
        }
        public RangeExAttribute(Type type, string minimum, string maximum, string code, string message = null)
            : base(type, minimum, maximum)
        {
            Code = code ?? ResponseCode.G_BadRequest;
            ErrorMessage = message;
        }
        public override string FormatErrorMessage(string name)
        {
            return $"{ErrorMessage}|{Code}";
        }
    }

}
