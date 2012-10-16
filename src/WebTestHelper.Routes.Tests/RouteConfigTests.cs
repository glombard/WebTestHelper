using System;
using System.Web.Routing;
using WebTestHelper.Routes.Tests.Controllers;
using Xunit;

namespace WebTestHelper.Routes.Tests
{
    public class RouteConfigTests
    {
        public class ShouldBeIgnored
        {
            [Fact]
            public void Resource_route_with_no_parameters_should_be_ignored()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                routes.Url("~/abc.axd").ShouldBeIgnored();
            }

            [Fact]
            public void Resource_route_with_pathinfo_should_be_ignored()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                routes.Url("~/abc.axd/x/y/z").ShouldBeIgnored();
            }

            [Fact]
            public void Root_url_should_not_be_ignored()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                Assert.Throws<AssertException>(() => routes.Url("~/").ShouldBeIgnored());
            }

            [Fact]
            public void Url_with_controller_action_and_parameter_should_not_be_ignored()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                Assert.Throws<AssertException>(() => routes.Url("~/product/details/123").ShouldBeIgnored());
            }
        }

        public class ShouldMapToController
        {
            [Fact]
            public void Root_url_should_map_to_default_controller()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                routes.Url("~/").ShouldMapTo<HomeController>();
            }

            [Fact]
            public void Url_with_static_action_should_map_to_correct_controller()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                routes.Url("~/search/string+to+search").ShouldMapTo<SearchController>();
            }

            [Fact]
            public void When_comparing_a_url_with_static_action_to_wrong_controller_then_throw_AssertException()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                Assert.Throws<AssertException>(() => routes.Url("~/search/string+to+search").ShouldMapTo<ProductController>());
            }

            [Fact]
            public void Url_with_controller_action_and_parameter_should_map_to_correct_controller()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                routes.Url("~/product/details/123").ShouldMapTo<ProductController>();
            }

            [Fact]
            public void When_comparing_a_url_with_controller_action_and_parameter_to_wrong_controller_then_throw_AssertException()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                Assert.Throws<AssertException>(() => routes.Url("~/product/details/123").ShouldMapTo<SearchController>());
            }

            [Fact]
            public void Url_with_no_controller_in_route_throws_AssertException()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                Assert.Throws<AssertException>(() => routes.Url("~/NoController/index").ShouldMapTo<HomeController>());
            }
        }

        public class ShouldMapToControllerAndAction
        {
            [Fact]
            public void Root_url_should_map_to_default_controller_and_default_action()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                routes.Url("~/").ShouldMapTo<HomeController>(x => x.Index());
            }

            [Fact]
            public void Url_with_controller_only_should_map_to_default_action()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                routes.Url("~/product").ShouldMapTo<ProductController>(x => x.Index());
            }

            [Fact]
            public void Url_with_controller_and_action_only_should_map_correctly()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                routes.Url("~/product/index").ShouldMapTo<ProductController>(x => x.Index());
            }

            [Fact]
            public void Url_with_controller_and_action_and_unexpected_parameter_throws_AssertException()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                Assert.Throws<AssertException>(() => routes.Url("~/product/index/123").ShouldMapTo<ProductController>(x => x.Index()));
            }

            [Fact]
            public void Url_with_controller_action_and_parameter_should_map_to_correct_controller_and_action()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                routes.Url("~/product/details/123").ShouldMapTo<ProductController>(x => x.Details(123));
            }

            [Fact]
            public void Url_with_controller_action_and_two_parameters_should_map_to_correct_controller_and_action()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                routes.Url("~/search/ASP.NET/2").ShouldMapTo<SearchController>(x => x.Result("ASP.NET", 2));
            }

            [Fact]
            public void Url_with_controller_and_action_and_missing_parameter_throws_AssertException()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                Assert.Throws<AssertException>(() => routes.Url("~/product/details").ShouldMapTo<ProductController>(x => x.Details(123)));
            }

            [Fact]
            public void Url_with_no_matching_route_throws_AssertException()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                Assert.Throws<AssertException>(() => routes.Url("~/product/details/123/456").ShouldMapTo<ProductController>(x => x.Details(123)));
            }

            [Fact]
            public void Url_with_no_action_in_route_throws_AssertException()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                Assert.Throws<AssertException>(() => routes.Url("~/HomeNoAction").ShouldMapTo<HomeController>(x => x.Index()));
            }

            [Fact]
            public void Url_with_controller_and_action_throws_AssertException_when_action_is_wrong()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                Assert.Throws<AssertException>(() => routes.Url("~/product/delete/123").ShouldMapTo<ProductController>(x => x.Details(123)));
            }

            [Fact]
            public void Url_with_controller_action_and_parameter_throws_AssertException_when_action_expects_a_missing_parameter()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                Assert.Throws<AssertException>(() => routes.Url("~/SearchMissingParam/abc").ShouldMapTo<SearchController>(x => x.Result("abc", 1)));
            }

            [Fact]
            public void Url_with_controller_action_and_parameter_should_map_correctly_to_action_with_nullable_parameter()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                routes.Url("~/product/list/ASP.NET/3").ShouldMapTo<ProductController>(x => x.List("ASP.NET", 3));
            }

            [Fact]
            public void Url_with_controller_action_and_missing_optional_parameter_should_map_correctly_to_action_with_nullable_parameter()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                routes.Url("~/product/list/ASP.NET").ShouldMapTo<ProductController>(x => x.List("ASP.NET", null));
            }

            [Fact]
            public void Url_with_controller_action_and_missing_optional_parameter_throws_AssertException_when_parameter_is_expected()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                Assert.Throws<AssertException>(() => routes.Url("~/product/list/ASP.NET").ShouldMapTo<ProductController>(x => x.List("ASP.NET", 123)));
            }

            [Fact]
            public void Url_with_controller_action_and_parameter_throws_AssertException_when_null_parameter_is_expected()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                Assert.Throws<AssertException>(() => routes.Url("~/product/list/ASP.NET/1").ShouldMapTo<ProductController>(x => x.List("ASP.NET", null)));
            }

            [Fact]
            public void Url_with_controller_action_and_parameter_should_map_correctly_when_parameter_is_variable()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                int variable123 = 123;
                routes.Url("~/product/details/123").ShouldMapTo<ProductController>(x => x.Details(variable123));
            }

            [Fact]
            public void Url_with_controller_action_and_parameter_should_map_correctly_when_parameter_is_cast_operator()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                long variable123 = 123;
                routes.Url("~/product/details/123").ShouldMapTo<ProductController>(x => x.Details((int)variable123));
            }

            private int MethodReturnsInt(int valueToReturn)
            {
                return valueToReturn;
            }

            [Fact]
            public void Url_with_controller_action_and_parameter_should_map_correctly_when_parameter_is_method_call()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                routes.Url("~/product/details/123").ShouldMapTo<ProductController>(x => x.Details(MethodReturnsInt(123)));
            }

            [Fact]
            public void Url_with_controller_action_and_parameter_throws_AssertException_when_parameter_is_unsupported_expression()
            {
                RouteCollection routes = new RouteCollection();
                RouteConfig.RegisterRoutes(routes);
                Func<int> func = () => 123;
                Assert.Throws<AssertException>(() => routes.Url("~/product/details/123").ShouldMapTo<ProductController>(x => x.Details(func())));
            }
        }
    }
}
