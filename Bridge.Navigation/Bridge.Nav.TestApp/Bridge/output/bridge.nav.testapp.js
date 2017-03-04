/**
 * @version 1.0.0.0
 * @copyright Copyright ©  2017
 * @compiler Bridge.NET 15.7.0
 */
Bridge.assembly("Bridge.Nav.TestApp", function ($asm, globals) {
    "use strict";

    Bridge.define("Bridge.Nav.TestApp.App", {
        statics: {
            nav: null
        },
        $main: function () {
            var navConfig = new Bridge.Nav.TestApp.TestNavConfig();
            Bridge.Nav.TestApp.App.nav = new Bridge.Navigation.Impl.BridgeNavigator(navConfig);

            Bridge.Nav.TestApp.App.nav.Bridge$Navigation$Abstraction$INavigator$initNavigation();
        }
    });

    Bridge.define("Bridge.Nav.TestApp.Controllers.Page1Controller", {
        inherits: [Bridge.Navigation.Abstraction.IAmLoadable],
        config: {
            alias: [
            "onLoad", "Bridge$Navigation$Abstraction$IAmLoadable$onLoad"
            ]
        },
        ctor: function (nav) {
            this.$initialize();
            var button = $("#myButton");
            button.click(function () {
                nav.Bridge$Navigation$Abstraction$INavigator$navigate("page2");
            });
        },
        onLoad: function () {
            Bridge.Console.log("OnLoad() on Page 1 controller");
        }
    });

    Bridge.define("Bridge.Nav.TestApp.Controllers.Page2Controller", {
        inherits: [Bridge.Navigation.Abstraction.IAmLoadable],
        config: {
            alias: [
            "onLoad", "Bridge$Navigation$Abstraction$IAmLoadable$onLoad"
            ]
        },
        onLoad: function () {
            Bridge.global.alert("Hello from Page 2 Controller");
        }
    });

    Bridge.define("Bridge.Nav.TestApp.TestNavConfig", {
        inherits: [Bridge.Navigation.Impl.BridgeNavigatorConfigBase],
        config: {
            alias: [
            "getHomeId", "Bridge$Navigation$Abstraction$INavigatorConfigurator$getHomeId",
            "createRoutes", "Bridge$Navigation$Abstraction$INavigatorConfigurator$createRoutes",
            "getBody", "Bridge$Navigation$Abstraction$INavigatorConfigurator$getBody"
            ]
        },
        getHomeId: function () {
            return "home";
        },
        getBody: function () {
            return $("#myContent");
        },
        createRoutes: function () {
            return $asm.$.Bridge.Nav.TestApp.TestNavConfig.f3(new (System.Collections.Generic.List$1(Bridge.Navigation.Abstraction.IPageDescriptor))());
        }
    });

    Bridge.ns("Bridge.Nav.TestApp.TestNavConfig", $asm.$);

    Bridge.apply($asm.$.Bridge.Nav.TestApp.TestNavConfig, {
        f1: function () {
            return new Bridge.Nav.TestApp.Controllers.Page1Controller(Bridge.Nav.TestApp.App.nav);
        },
        f2: function () {
            return new Bridge.Nav.TestApp.Controllers.Page2Controller();
        },
        f3: function (_o1) {
            _o1.add(Bridge.merge(new Bridge.Navigation.Impl.PageDescriptor(), {
                setKey: "home",
                setHtmlLocation: "home.html"
            } ));
            _o1.add(Bridge.merge(new Bridge.Navigation.Impl.PageDescriptor(), {
                setKey: "page1",
                setHtmlLocation: "page1.html",
                setPageController: $asm.$.Bridge.Nav.TestApp.TestNavConfig.f1
            } ));
            _o1.add(Bridge.merge(new Bridge.Navigation.Impl.PageDescriptor(), {
                setKey: "page2",
                setHtmlLocation: "page2.html",
                setPageController: $asm.$.Bridge.Nav.TestApp.TestNavConfig.f2
            } ));
            return _o1;
        }
    });
});
