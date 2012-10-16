using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebTestHelper.Routes
{
    public static class RouteDataExtensions
    {
        public static void ShouldMapTo<T>(this RouteData routeData)
        {
            string controllerName = typeof(T).Name;
            if (controllerName.EndsWith("Controller"))
            {
                controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);
            }

            if (routeData == null)
            {
                throw new AssertException(string.Format("No route matched controller '{0}'", controllerName));
            }

            object value;
            if (!routeData.Values.TryGetValue("controller", out value))
            {
                throw new AssertException(string.Format("Expected controller name '{0}', but none specified in route", controllerName));
            }

            if (!string.Equals(controllerName, value.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                throw new AssertException(string.Format("Expected controller name '{0}', but it was '{1}'", controllerName, value));
            }
        }

        public static void ShouldMapTo<T>(this RouteData routeData, Expression<Action<T>> action) where T : Controller
        {
            routeData.ShouldMapTo<T>();

            VerifyActionMatches<T>(routeData, action);

            IDictionary<string, object> expectedParameterValues = GetParameterValuesFromExpression<T>(action);
            VerifyActionParametersMatch(routeData, expectedParameterValues);
        }

        private static void VerifyActionMatches<T>(RouteData routeData, Expression<Action<T>> action) where T : Controller
        {
            string methodName = ((MethodCallExpression)action.Body).Method.Name;

            object actionName;
            if (!routeData.Values.TryGetValue("action", out actionName))
            {
                throw new AssertException(string.Format("Expected action '{0}', but none specified in route", methodName));
            }

            if (!string.Equals(methodName, actionName.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                throw new AssertException(string.Format("Expected action '{0}', but it was '{1}'", methodName, actionName));
            }
        }

        private static void VerifyActionParametersMatch(RouteData routeData, IDictionary<string, object> expectedParameterValues)
        {
            List<string> valuesNotInRoute = new List<string>();
            List<string> valuesNotInParameters = new List<string>();

            foreach (KeyValuePair<string, object> actionParameter in expectedParameterValues)
            {
                object routeParameter;
                if (!routeData.Values.TryGetValue(actionParameter.Key, out routeParameter))
                {
                    valuesNotInRoute.Add(actionParameter.Key);
                }
                else
                {
                    bool bothNull = actionParameter.Value == null && routeParameter == UrlParameter.Optional;
                    if (!bothNull && (actionParameter.Value == null || actionParameter.Value.ToString() != routeParameter.ToString()))
                    {
                        throw new AssertException(string.Format("Expected route parameter '{0}' value '{1}', but it was '{2}'", actionParameter.Key, actionParameter.Value, routeParameter));
                    }
                }
            }

            foreach (string routeParameter in routeData.Values.Keys)
            {
                if (!string.Equals(routeParameter, "action", StringComparison.InvariantCultureIgnoreCase) && !string.Equals(routeParameter, "controller", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (!expectedParameterValues.ContainsKey(routeParameter))
                    {
                        string routeValue = routeData.Values[routeParameter].ToString();
                        if (!string.IsNullOrEmpty(routeValue))
                        {
                            valuesNotInParameters.Add(routeParameter);
                        }
                    }
                }
            }

            if (valuesNotInRoute.Count > 0)
            {
                throw new AssertException(string.Format("Parameters were expected, but not found in route: '{0}'", string.Join(", ", valuesNotInRoute)));
            }

            if (valuesNotInParameters.Count > 0)
            {
                throw new AssertException(string.Format("Unexpected parameters in route: '{0}'", string.Join(", ", valuesNotInParameters)));
            }
        }

        // TODO: Consider replacing with Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression ...
        private static IDictionary<string, object> GetParameterValuesFromExpression<T>(Expression<Action<T>> action) where T : Controller
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            MethodCallExpression body = action.Body as MethodCallExpression;
            if (body != null)
            {
                ParameterInfo[] parameters = body.Method.GetParameters();

                for (int i = 0; i < parameters.Length; i++)
                {
                    Expression argument = body.Arguments[i];

                    if (argument.NodeType == ExpressionType.Convert && argument is UnaryExpression)
                    {
                        argument = ((UnaryExpression)argument).Operand;
                    }

                    object parameterValue = null;
                    ConstantExpression constantExpression = argument as ConstantExpression;
                    if (constantExpression != null)
                    {
                        parameterValue = constantExpression.Value;
                    }
                    else if (argument.NodeType == ExpressionType.Call || argument.NodeType == ExpressionType.New || argument.NodeType == ExpressionType.MemberAccess)
                    {
                        parameterValue = Expression.Lambda(argument).Compile().DynamicInvoke();
                    }
                    else
                    {
                        throw new AssertException(string.Format("Argument type '{0}' not supported", argument.NodeType));
                    }

                    result.Add(parameters[i].Name, parameterValue);
                }
            }

            return result;
        }

        public static void ShouldBeIgnored(this RouteData routeData)
        {
            if (!(routeData.RouteHandler is StopRoutingHandler))
            {
                throw new AssertException("Expected route to be ignored, but it isn't");
            }
        }
    }
}
