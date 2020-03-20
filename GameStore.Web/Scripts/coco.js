;
(function ($) {
    var defaultIfEmptyArray = function (x) {
        return x.length == 0 ? null : x;
    };
    var tailzero = function (v, len) {
        var s = v.toString();
        while (len--) {
            if (v < Math.pow(10, len))
                return "0" + s;
        }
        return s;
    }
    if (!String.prototype.split)
        String.prototype.split = function (c) {
            var arr = [];
            var globalIdx = 0;
            while (globalIdx < c.length) {
                var idx = this.indexOf(c, globalIdx);
                if (idx < 0) {
                    arr.push(c.substring(globalIdx, c.length));
                    globalIdx = c.length;
                } else {
                    arr.push(c.substring(globalIdx, idx));
                    globalIdx = idx;
                }
            }
            return arr;
        };

    if (!Array.prototype.map)
        Array.prototype.map = function (fn) {
            return _.map(this, fn);
        };
    if (!Array.prototype.indexOf)
        Array.prototype.indexOf = function (c) {
            for (var i = 0; i < this.length; i++) {
                if (this[i] == c) {
                    return i;
                }
            }
            return -1;
        };
    var coco = {
        template: function (key, template) {
            this._templates = this._templates || {};
            if (!template)
                return this._templates[key];

            this._templates[key] = template;
        },
        util: {
            formatDate: function (date, fmt) { //author: meizz  
                if (date == null || date == undefined || date == "")
                    return;
                if (typeof date === "string")
                    date = new Date(date);
                var o = {
                    "yyyy": tailzero(date.getFullYear() + 1, 4),
                    "MM": tailzero(date.getMonth() + 1, 2),
                    "dd": tailzero(date.getDate(), 2),
                    "hh": tailzero(date.getHours(), 2),
                    "mm": tailzero(date.getMinutes(), 2),
                    "ss": tailzero(date.getSeconds(), 2),
                    "q": Math.floor((date.getMonth() + 3) / 3),
                    "S": tailzero(date.getMilliseconds(), 3)
                };
                for (var k in o)
                    if (new RegExp("(" + k + ")").test(fmt))
                        fmt = fmt.replace(RegExp.$1, o[k]);
                return fmt;
            }
        }
    };
    if (!window.console) {
        window.console = {
        };
    }
    if (!console.debug) {
        console.debug = function () {

        }
    }
    if (!console.warn) {
        console.warn = function () {

        }
    }
    coco.replaceQueryString = function (url, key, value) {
        if (!url) return url;
        key = encodeURIComponent(key);
        value = encodeURIComponent(value);
        if (url.indexOf("?") >= 0) {
            var re = new RegExp("([?|&])" + key + "=.*?(&|$)", "i");
            if (url.match(re))
                return url.replace(re, '$1' + key + "=" + value + '$2');
            else
                return url + '&' + key + "=" + value;
        } else {
            return url + "?" + key + "=" + value;
        }
    };
    coco.combimeUrlIfPartial = function (url1, url2) {
        if (url2.indexOf("&") == 0) {
            var segs = url2.split("&");
            for (var i = 0; i < segs.length; i++) {
                if (!segs[i]) {
                    continue;
                }
                var kv = segs[i].split("=");
                if (kv.length == 1) {
                    kv.push("true");
                }
                url1 = coco.replaceQueryString(url1, kv[0], kv[1]);
            }
            return url1;
        }
        return url2;
    };
    coco.queryString = function (tolowercase, href) {
        var search = href || window.location.search;
        var idxQ = search.indexOf("?");
        if (idxQ >= 0) {
            search = search.slice(idxQ);
        }
        var reg = /\??(.+?)=(.+?)(?:&|$)/;
        var params = {};
        var g1 = reg.exec(search);
        while (g1 && g1.length > 2) {
            if (tolowercase)
                params[decodeURIComponent(g1[1]).toLowerCase()] = decodeURIComponent(g1[2]);
            else
                params[decodeURIComponent(g1[1])] = decodeURIComponent(g1[2]);
            search = search.replace(reg, "");
            g1 = reg.exec(search);
        }
        return params;
    };
    $.fn.serializeObject = function () {
        var form = this;
        if (!this.is("form"))
            form = this.parents("form:eq(0)");
        form = form[0];

        return form2js(form, ".", true);
    };

    coco.StatusCodes = new function () {
        this._statusCodes = {};
        var that = this;
        this.match = function (statusArray, status) {
            for (var i = 0; i < statusArray.length; i++) {
                if (statusArray[i].toLowerCase() == status)
                    return true;
                var name = statusArray[i];
                if (that._statusCodes[name] != undefined && that._statusCodes[name](status) == true) {
                    return true;
                }
            }
            return false;
        };
        this.register = function (name, fn) {
            that._statusCodes[name] = fn;
        };

        this.register("*", function (status) { return true; })
        this.register("?", function (status) {
            return (status == "init" || parseInt(status) != NaN)
        });
    };
    $(function () {
        $("[data-readonly] input, [data-readonly] select, [data-readonly] textarea").attr("disabled", "disabled");
        // data-toggle.click
        $("body").on("click.toggle-click", "[data-toggle='click']", function (event) {
            event.preventDefault();
            event.stopImmediatePropagation();
            var target = $(this).attr("target") || $(this).attr("data-target");
            $(target).click();
        });
        // upload
        $("body").on("click.upload-button", ".upload-button", function (event) {
            var uploadButton = $(this);
            event.preventDefault();
            event.stopImmediatePropagation();
            var name = uploadButton.attr("name");
            var file = $('<input type="file" style="position: absolute;left: -10000px;" ></input>').appendTo($("body")).attr("name", "N" + new Date().tick);
            uploadButton.data("upload-input", file);

            file.bind("change", function () {
                var that = this;
                $.ajax(uploadButton.attr("data-href"), {
                    files: that,
                    iframe: true,
                    processData: false
                }).complete(function (rsp) {
                    var data = rsp.responseJSON;
                    if (data && data.ok == true) {
                        $(":not(button)[name='" + name + "']").text(data.fileName).val(data.fileName).prop("src", data.url).prop("href", data.url);
                        $(":not(button)[name='" + name + ".name']").text(data.name).val(data.name).prop("src", data.url).prop("href", data.url);;
                    }
                });
            })
            file.click();
        });
        // confirm
        $("body").on("click.confirm", "[data-confirm]", function (event) {
            if (!confirm($(this).attr("data-confirm"))) {
                event.preventDefault();
                event.stopImmediatePropagation();
            }
        });

        // disable / enable submit button when form submitting
        $("body").on("submitting.disable-buttons", "form", function () {
            var buttons = $("button[type=submit]:not(:disabled)", this);
            buttons.attr("disabled", "disabled")
                .attr("data-dynamic-disabled", "true")
                .addClass("disabled", "disabled");
        });
        $(document).ajaxComplete(function (event, xhr, settings) {
            var buttons = $("[data-dynamic-disabled]", settings.target);
            buttons.removeAttr("disabled").removeAttr("data-dynamic-disabled").removeClass("disabled");
        });

        // captcha-image
        $("body").on("click.captcha-image", ".captcha-image", function () {
            var src = coco.replaceQueryString($(this).attr("src"), "$t", new Date());
            $(this).attr("src", src);
        });
        $(".captcha-image").each(function () {
            var src = coco.replaceQueryString($(this).attr("src"), "$t", new Date());
            $(this).attr("src", src);
        });
        $(document).ajaxComplete(function (event, xhr, settings) {
            $(".captcha-image").each(function () {
                var src = coco.replaceQueryString($(this).attr("src"), "$t", new Date());
                $(this).attr("src", src);
            });
        });


        var renderTemplate = function (target, data, statusCode) {
            console.debug("render template: " + statusCode);

            data = data || { data: {}, local: {}, ajax: {} };
            var $target = $(target);
            var templates = $target.data("templates");
            if (!templates) {
                return;
            }

            _.each(templates, function (x) {
                if (coco.StatusCodes.match(x.statusCodes, statusCode))
                    if (x.container.parents("[data-template]")[0] == $target[0] || x.container[0] == $target[0]) {
                        x.container.html(x.template(data));
                    }
            });
        };

        $.ajaxSetup({
            contentType: "application/json",
            beforeSend: function (xhr, settings) {
                if (!settings.target)
                    return;
                var $this = $(settings.target);
                var target = $this;
                if (!$this.is("[data-template]") || $this.attr("data-target")) {
                    var targetExpr = $this.attr("data-target") || "[data-template]:eq(0)";
                    target = defaultIfEmptyArray($this.parents(targetExpr))
                        || defaultIfEmptyArray($(targetExpr))
                        || $this;
                }

                renderTemplate(target, null, "async-begin");
            }
        });
        $(document).ajaxComplete(function (event, xhr, settings) {
            if (xhr.getResponseHeader("location")) {
                window.location.href = coco.combimeUrlIfPartial(window.location.toString(), xhr.getResponseHeader("location"));
            } else {
                var contentType = xhr.getResponseHeader("content-type");
                contentType = (contentType || "").toLowerCase();
                if (contentType.indexOf("json") < 0)
                    return;
                var data = xhr.responseJSON;
                if (data == undefined && data == null) {
                    if (xhr.responseText == undefined && xhr.responseText == null)
                        return;

                    data = jQuery.parseJSON(xhr.responseText);
                    xhr.responseJSON = data;
                }
                var $this = $(settings.target);
                var target = $this;
                if (!$this.is("[data-template]") || $this.attr("data-target")) {
                    var targetExpr = $this.attr("data-target") || "[data-template]:eq(0)";
                    target = defaultIfEmptyArray($this.parents(targetExpr))
                        || defaultIfEmptyArray($(targetExpr))
                        || $this;
                }
                renderTemplate(target, model, "async-end");


                var local = $.extend(coco.queryString(true, settings.url), settings.data || {});
                var model = { data: data, local: local, ajax: settings };
                if (contentType.indexOf("application/json") >= 0) {
                    renderTemplate(target, model, xhr.status.toString());
                }

            }
        });

        $(document).ajaxComplete(function (event, xhr, settings) {
            var contentType = xhr.getResponseHeader("content-type");
            contentType = (contentType || "").toLowerCase();
            if (contentType.indexOf("text/html") >= 0) {
                var $this = $(settings.target);
                var target = $($this.attr("target") || $this.attr("data-target"));
                if (!target.length)
                    target = $this.parents("[data-html-panel]:eq(0)");
                target.html(xhr.responseText);
            }
        });
        $("[data-template]").each(function () {
            var $panel = $(this);
            var templates = [];
            $("[type='text/template']", $panel).each(function () {
                var statusCode = $(this).attr("data-render-status");
                if (!statusCode) return this;
                try {
                    statusCode = $.parseJSON(statusCode);
                } catch (e) {
                }
                if (!$.isArray(statusCode))
                    statusCode = [statusCode.toString()];
                statusCode = statusCode.map(function (x) { return x.toString(); });
                templates.push({
                    statusCodes: statusCode,
                    container: $(this).parent(),
                    template: coco.template($(this).attr("data-referenceId")) || _.template($(this).html())
                });
            });
            $panel.data("templates", templates);
            renderTemplate(this, { data: {}, local: {}, ajax: {} }, "init");
        });
        $("[data-load]").each(function () {
            var $this = $(this);
            var dataLoad = $this.attr("data-load");
            try {
                dataLoad = $.parseJSON(dataLoad);
            } catch (e) {
            }
            if (!$.isArray(dataLoad))
                dataLoad = [dataLoad.toString()];
            dataLoad = dataLoad.map(function (x) { return x.toString(); });
            $this.data("data-load", dataLoad);
            if (dataLoad.indexOf("init") >= 0 || dataLoad.indexOf("*") >= 0)
                $.ajax({
                    target: $this,
                    url: $this.attr("href") || $this.attr("data-href") || $this.attr("action"),
                    type: "get"
                });
        });
        $(document).ajaxComplete(function (event, xhr, settings) {
            if (xhr.status != "200" && xhr.status != "201")
                return;
            if ((settings.type || "get").toLowerCase() == "get")
                return;
            $("[data-load]").each(function () {
                var $this = $(this);
                var dataLoad = $this.data("data-load");
                if (dataLoad.indexOf(xhr.status) >= 0 || dataLoad.indexOf("*") >= 0)
                    $.ajax({
                        target: $this,
                        url: $this.attr("href") || $this.attr("data-href") || $this.attr("action"),
                        type: "get"
                    });
            });
        });
        $("body").on("click.ajax-a-click", "a[data-ajax=true]", function (event) {
            var $this = $(this);
            event.preventDefault();
            var href = $this.attr("href") || $this.attr("data-href") || $this.attr("action");

            if (href.indexOf("&") == 0) {
                var form = $this.parents("form:eq(0)");
                href = coco.combimeUrlIfPartial(form.prop("action") || window.location.toString(), href);
            }
            $.ajax({
                target: $this,
                url: href,
                type: "get"
            });
        });
        $("body").on("click.ajax-button-click", "button[type=button][data-ajax=true]", function (event) {
            var $this = $(this);
            event.preventDefault();
            var href = $this.attr("href") || $this.attr("data-href") || $this.attr("action");
            var type = $this.attr("data-method") || "post";

            if (href.indexOf("&") == 0) {
                var form = $this.parents("form:eq(0)");
                href = coco.combimeUrlIfPartial(form.prop("action") || window.location.toString(), href);
            }
            $.ajax({
                target: $this,
                url: href,
                type: type
            });
        });

        $("body").on("submit.ajax-form-submit", "form[data-ajax=true]", function (event) {
            event.preventDefault();
            var form = $(this);

            form.trigger("submitting");
            var postData = form.serializeObject();

            var href = form.prop("action");
            var type = form.attr("data-ajax-method") || form.prop("method") || "post";

            if (type.toLowerCase() == "get") {
                for (var key in postData) {
                    href = coco.replaceQueryString(href, key, postData[key]);
                }
                postData = "";
            } else {
                postData = JSON.stringify(postData);
            }

            $.ajax({
                url: href,
                target: form,
                contentType: "application/json",
                type: type,
                data: postData,
                dataType: 'json'
            });
        });
    });

    if (window) window.coco = coco;
})(jQuery);