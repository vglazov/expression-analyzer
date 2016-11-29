using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpressionUtils.Expressions;
using ExpressionUtils.Parsing;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new ExpressionContext());
        }


        [HttpPost]
        public ActionResult Analyze([Bind] ExpressionContext context)
        {            
            ViewBag.AnalyzeError = null;
            context.Substitutions = null;
            ViewBag.ExpressionOutput = null;
            if (context.ExpressionInput != null && context.ExpressionInput.Trim().Length > 0)
            {
                try
                {
                    var expr = ExpressionParser.Parse(context.ExpressionInput);
                    context.Substitutions = expr.GetAllVariables().ToDictionary(v => v.Name, v => "");
                }
                catch (ExpressionParseException e)
                {
                    ViewBag.AnalyzeError = e.Message;
                }
            }

            return View("Index", context);
        }

        public ActionResult DoSubstitutions([Bind] ExpressionContext context)
        {
            var expr = ExpressionParser.Parse(context.ExpressionInput);
            var substitutions = (from sub in context.Substitutions
                where !String.IsNullOrEmpty(sub.Value)
                select new {sub.Key, Value = ParseSubstitutionValue(sub.Value)}).ToDictionary(sub => sub.Key, sub => sub.Value);
            ViewBag.ExpressionOutput = expr.CloneWithVariableSubstitute(substitutions);
            return View("Index", context);
        }

        private static Expression ParseSubstitutionValue(String value)
        {
            long longVal;
            decimal decVal;
            if (long.TryParse(value, out longVal))
            {
                return new IntegerConstantExpression(longVal);
            }
            else if(decimal.TryParse(value, out decVal))
            {
                return new DecimalConstantExpression(decVal);
            }
            else
            {
                return new StringConstantExpression(value);
            }
        }
    }
}