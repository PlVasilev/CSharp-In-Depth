using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace ExpresionTreesAddedToASP.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult RedirectToAction<TController>(this TController controller, Expression<Action<TController>> redirectExpression)
        {
            throw new Exception();
        }

        public static IActionResult RedirectToAction<TController>(this Controller controller, Expression<Action<TController>> redirectExpression)
        {
            if (redirectExpression.Body is MethodCallExpression methodCall)
            {
              var actionName = methodCall.Method.Name;
              var controllerName = typeof(TController).Name.Replace(nameof(Controller), string.Empty);

              var routeValueDictionary = new RouteValueDictionary();
              var parameters = methodCall.Method.GetParameters().Select(p => p.Name).ToArray();
              var values = methodCall.Arguments.Select(a =>
              {
                  var constant = (ConstantExpression) a;
                  return constant.Value;
              }).ToArray();

              for (int i = 0; i < parameters.Length; i++)
              {
                  routeValueDictionary.Add(parameters[i],values[i]);
              }

              return controller.RedirectToAction(actionName, controllerName, routeValueDictionary);
            }
            else
            {
                throw new InvalidOperationException("Expression is not valid");
            }
        }
    }
}
