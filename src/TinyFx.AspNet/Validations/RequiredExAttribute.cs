﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyFx.Net;

namespace TinyFx.AspNet
{
    public class RequiredExAttribute : RequiredAttribute
    {
        public string Code { get; set; }
        public RequiredExAttribute(string code, string message = null)
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
