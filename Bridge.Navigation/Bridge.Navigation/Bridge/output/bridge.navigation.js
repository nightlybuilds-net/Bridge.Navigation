/**
 * @version 1.0.0.0
 * @copyright Copyright ©  2017
 * @compiler Bridge.NET 15.7.0
 */
Bridge.assembly("Bridge.Navigation", function ($asm, globals) {
    "use strict";

    Bridge.define("Bridge.Navigation.Abstraction.IAmLoadable", {
        $kind: "interface"
    });

    Bridge.define("Bridge.Navigation.Abstraction.INavigator", {
        $kind: "interface"
    });

    Bridge.define("Bridge.Navigation.Abstraction.INavigatorConfigurator", {
        $kind: "interface"
    });

    Bridge.define("Bridge.Navigation.Abstraction.IPageDescriptor", {
        $kind: "interface"
    });

    Bridge.define("Bridge.Navigation.App", {
        $main: function () {
            // Create a new Button
            var button = Bridge.merge(document.createElement('button'), {
                innerHTML: "Click Me",
                onclick: $asm.$.Bridge.Navigation.App.f1
            } );

            // Add the Button to the page
            document.body.appendChild(button);

            // To confirm Bridge.NET is working: 
            // 1. Build this project (Ctrl + Shift + B)
            // 2. Browse to file /Bridge/www/demo.html
            // 3. Right-click on file and select "View in Browser" (Ctrl + Shift + W)
            // 4. File should open in a browser, click the "Submit" button
            // 5. Success!
        }
    });

    Bridge.ns("Bridge.Navigation.App", $asm.$);

    Bridge.apply($asm.$.Bridge.Navigation.App, {
        f1: function (ev) {
            // When Button is clicked, 
            // the Bridge Console should open.
            Bridge.Console.log("Success!");
        }
    });

    /** @namespace Bridge.Navigation.Impl */

    /**
     * INavigator implementation
     *
     * @public
     * @class Bridge.Navigation.Impl.BridgeNavigator
     * @implements  Bridge.Navigation.Abstraction.INavigator
     */
    Bridge.define("Bridge.Navigation.Impl.BridgeNavigator", {
        inherits: [Bridge.Navigation.Abstraction.INavigator],
        _configuration: null,
        config: {
            alias: [
            "navigate", "Bridge$Navigation$Abstraction$INavigator$navigate",
            "initNavigation", "Bridge$Navigation$Abstraction$INavigator$initNavigation"
            ]
        },
        ctor: function (configuration) {
            this.$initialize();
            this._configuration = configuration;
        },
        /**
         * Navigate to a page ID.
         The ID must be registered.
         *
         * @instance
         * @public
         * @this Bridge.Navigation.Impl.BridgeNavigator
         * @memberof Bridge.Navigation.Impl.BridgeNavigator
         * @param   {string}    pageId
         * @return  {void}
         */
        navigate: function (pageId) {
            var page = this._configuration.Bridge$Navigation$Abstraction$INavigatorConfigurator$getPageDescriptorByKey(pageId);
            if (page == null) {
                throw new System.Exception(System.String.format("Page not found with ID {0}", pageId));
            }

            var body = this._configuration.Bridge$Navigation$Abstraction$INavigatorConfigurator$getBody();

            if (body == null) {
                throw new System.Exception("Cannot find navigation body element.");
            }

            this._configuration.Bridge$Navigation$Abstraction$INavigatorConfigurator$getBody().load(page.Bridge$Navigation$Abstraction$IPageDescriptor$getHtmlLocation(), null, function (o, s, a) {
                if (!Bridge.staticEquals(page.Bridge$Navigation$Abstraction$IPageDescriptor$getPageController(), null)) {
                    page.Bridge$Navigation$Abstraction$IPageDescriptor$getPageController()().Bridge$Navigation$Abstraction$IAmLoadable$onLoad();
                }

                if (page.Bridge$Navigation$Abstraction$IPageDescriptor$getJsDependencies() != null) {
                    Bridge.Linq.Enumerable.from(page.Bridge$Navigation$Abstraction$IPageDescriptor$getJsDependencies()).forEach($asm.$.Bridge.Navigation.Impl.BridgeNavigator.f1);
                }
            });
        },
        /**
         * Subscribe to anchors click
         *
         * @instance
         * @public
         * @this Bridge.Navigation.Impl.BridgeNavigator
         * @memberof Bridge.Navigation.Impl.BridgeNavigator
         * @return  {void}
         */
        initNavigation: function () {
            var allAnchors = $("a");
            allAnchors.click(Bridge.fn.bind(this, $asm.$.Bridge.Navigation.Impl.BridgeNavigator.f2));

            // go home
            this.navigate(this._configuration.Bridge$Navigation$Abstraction$INavigatorConfigurator$getHomeId());
        }
    });

    Bridge.ns("Bridge.Navigation.Impl.BridgeNavigator", $asm.$);

    Bridge.apply($asm.$.Bridge.Navigation.Impl.BridgeNavigator, {
        f1: function (f) {
            $.getScript(f);
        },
        f2: function (ev) {
            var anchor = ev.target;
            var href = anchor.getAttribute("href");
            var isMyHref = System.String.startsWith(href, "@");

            // if is my href
            if (isMyHref) {
                ev.preventDefault();
                var pageId = System.String.trimStart(href, [64]);
                this.navigate(pageId);
            }
            // anchor default behaviour
        }
    });

    /**
     * INavigatorConfigurator Implementation. Must be extended.
     *
     * @abstract
     * @public
     * @class Bridge.Navigation.Impl.BridgeNavigatorConfigBase
     * @implements  Bridge.Navigation.Abstraction.INavigatorConfigurator
     */
    Bridge.define("Bridge.Navigation.Impl.BridgeNavigatorConfigBase", {
        inherits: [Bridge.Navigation.Abstraction.INavigatorConfigurator],
        _routes: null,
        config: {
            alias: [
            "getPageDescriptorByKey", "Bridge$Navigation$Abstraction$INavigatorConfigurator$getPageDescriptorByKey"
            ]
        },
        ctor: function () {
            this.$initialize();
            this._routes = this.createRoutes();
        },
        getPageDescriptorByKey: function (key) {
            return System.Linq.Enumerable.from(this._routes).singleOrDefault(function (s) {
                    return System.String.equals(s.Bridge$Navigation$Abstraction$IPageDescriptor$getKey(), key, 1);
                }, null);
        }
    });

    Bridge.define("Bridge.Navigation.Impl.PageDescriptor", {
        inherits: [Bridge.Navigation.Abstraction.IPageDescriptor],
        config: {
            properties: {
                Key: null,
                HtmlLocation: null,
                PageController: null,
                JsDependencies: null
            },
            alias: [
            "getKey", "Bridge$Navigation$Abstraction$IPageDescriptor$getKey",
            "setKey", "Bridge$Navigation$Abstraction$IPageDescriptor$setKey",
            "getHtmlLocation", "Bridge$Navigation$Abstraction$IPageDescriptor$getHtmlLocation",
            "setHtmlLocation", "Bridge$Navigation$Abstraction$IPageDescriptor$setHtmlLocation",
            "getPageController", "Bridge$Navigation$Abstraction$IPageDescriptor$getPageController",
            "setPageController", "Bridge$Navigation$Abstraction$IPageDescriptor$setPageController",
            "getJsDependencies", "Bridge$Navigation$Abstraction$IPageDescriptor$getJsDependencies",
            "setJsDependencies", "Bridge$Navigation$Abstraction$IPageDescriptor$setJsDependencies"
            ]
        }
    });

    var $m = Bridge.setMetadata,
        $n = [Bridge.Navigation.Abstraction,System.Collections.Generic,Bridge.Navigation.Impl];
    $m($n[2].BridgeNavigator, function () { return {"att":1048577,"a":2,"m":[{"a":2,"n":".ctor","t":1,"p":[$n[0].INavigatorConfigurator],"pi":[{"n":"configuration","pt":$n[0].INavigatorConfigurator,"ps":0}],"sn":"ctor"},{"a":2,"n":"InitNavigation","t":8,"sn":"initNavigation","rt":Object},{"a":2,"n":"Navigate","t":8,"pi":[{"n":"pageId","pt":String,"ps":0}],"sn":"navigate","rt":Object,"p":[String]},{"a":1,"n":"_configuration","t":4,"rt":$n[0].INavigatorConfigurator,"sn":"_configuration","ro":true}]}; });
    $m($n[2].BridgeNavigatorConfigBase, function () { return {"att":1048705,"a":2,"m":[{"a":3,"n":".ctor","t":1,"sn":"ctor"},{"ab":true,"a":2,"n":"CreateRoutes","t":8,"sn":"createRoutes","rt":$n[1].IList$1(Bridge.Navigation.Abstraction.IPageDescriptor)},{"a":2,"n":"GetPageDescriptorByKey","t":8,"pi":[{"n":"key","pt":String,"ps":0}],"sn":"getPageDescriptorByKey","rt":$n[0].IPageDescriptor,"p":[String]},{"ab":true,"a":2,"n":"Body","t":16,"rt":$,"g":{"ab":true,"a":2,"n":"get_Body","t":8,"sn":"getBody","rt":$}},{"ab":true,"a":2,"n":"HomeId","t":16,"rt":String,"g":{"ab":true,"a":2,"n":"get_HomeId","t":8,"sn":"getHomeId","rt":String}},{"a":1,"n":"_routes","t":4,"rt":$n[1].IList$1(Bridge.Navigation.Abstraction.IPageDescriptor),"sn":"_routes","ro":true}]}; });
    $m($n[2].PageDescriptor, function () { return {"att":1048577,"a":2,"m":[{"a":2,"isSynthetic":true,"n":".ctor","t":1,"sn":"ctor"},{"a":2,"n":"HtmlLocation","t":16,"rt":String,"g":{"a":2,"n":"get_HtmlLocation","t":8,"sn":"getHtmlLocation","rt":String},"s":{"a":2,"n":"set_HtmlLocation","t":8,"pi":[{"n":"value","pt":String,"ps":0}],"sn":"setHtmlLocation","rt":Object,"p":[String]}},{"a":2,"n":"JsDependencies","t":16,"rt":$n[1].IEnumerable$1(String),"g":{"a":2,"n":"get_JsDependencies","t":8,"sn":"getJsDependencies","rt":$n[1].IEnumerable$1(String)},"s":{"a":2,"n":"set_JsDependencies","t":8,"pi":[{"n":"value","pt":$n[1].IEnumerable$1(String),"ps":0}],"sn":"setJsDependencies","rt":Object,"p":[$n[1].IEnumerable$1(String)]}},{"a":2,"n":"Key","t":16,"rt":String,"g":{"a":2,"n":"get_Key","t":8,"sn":"getKey","rt":String},"s":{"a":2,"n":"set_Key","t":8,"pi":[{"n":"value","pt":String,"ps":0}],"sn":"setKey","rt":Object,"p":[String]}},{"a":2,"n":"PageController","t":16,"rt":Function,"g":{"a":2,"n":"get_PageController","t":8,"sn":"getPageController","rt":Function},"s":{"a":2,"n":"set_PageController","t":8,"pi":[{"n":"value","pt":Function,"ps":0}],"sn":"setPageController","rt":Object,"p":[Function]}}]}; });
});
