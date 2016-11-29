using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExpressionUtils.Expressions;

namespace Web.Models
{
    public class ExpressionContext
    {
        public string ExpressionInput { get; set; }        
        public IDictionary<String, String> Substitutions { get; set; }
    }
}