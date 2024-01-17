var utils = {
    toast: function () {
        toastr.options = {
            "closebutton": true
        };

        $('.toastrdefaultsuccess').click(function () {
            toastr.success('đăng ký thành công.')
        });
        $('.toastrdefaulterror').click(function () {
            toastr.error('lỗi')
        });
        $('.toastrdefaultinfo').click(function () {
            toastr.info('lorem ipsum dolor sit amet, consetetur sadipscing elitr.')
        });
        $('.toastrdefaultwarning').click(function () {
            toastr.warning('cảnh báo.')
        });

    },

    isEmpty: function (val) {

        if (typeof val == "object")
            return false;
        if (typeof val == "function")
            return false;

        return val === undefined || jQuery.trim(val).length === 0;
    },

    getSerialize: function (form, event) {
        var keys = {};
        var buttons = {};
        var checkboxs = {};

        form.find("input, select, textarea,button").each(function () {
            var el = jQuery(this);
            var name = el.prop("name");
            if (!utils.isEmpty(name)) {
                var tagName = el.prop("tagName").toLowerCase();

                let val = el.val();
                if (el.hasClass("isDataValue") && el.data('value')) {
                    val = el.data('value');
                }

                if (tagName === "select" && el.attr("multiple")) {
                    val = $('option:selected', el).map(function () {
                        return this.value;
                    }).get();

                    if (Array.isArray(val)) {
                        val = val.join(',');
                    }
                }
                if (tagName == "input") {
                    var type = el.prop("type").toLowerCase();
                    if (type == "text" || type == "password" || type == "hidden" || type == "number") {
                        if (!keys.hasOwnProperty(name)) {
                            keys[name] = [];
                        }
                        keys[name].push(val);
                    } else if (type == "checkbox" || type == "radio") {
                        if (!keys.hasOwnProperty(name)) {
                            keys[name] = [];
                        }
                        if (el.prop("checked")) {
                            keys[name].push(val);
                        }
                        else if (type == "checkbox" && !el.is(':disabled')) {
                            keys[name].push(0);
                        }
                        if (!checkboxs.hasOwnProperty(name)) {
                            checkboxs[name] = 0;
                        }
                        checkboxs[name]++;
                    }
                } else {
                    if (!keys.hasOwnProperty(name)) {
                        keys[name] = [];
                    }
                    keys[name].push(val);
                }
            }
        });

        for (var k in keys) {
            var vals = keys[k];
            if (vals.length == 1 || buttons.hasOwnProperty(k)) { //|| !checkboxs.hasOwnProperty(k)
                keys[k] = vals.join(",");
            } else {
                keys[k] = JSON.stringify(vals);
            }
        }
        return keys;
    },

    FormatNumber: function () {
        var VND = $(".NumberVND");
        jQuery.each( VND, function() {
            var val = $(this).text()
            var x =  new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(val)
            $(this).text(x)

          });
    }
}

$(document).ready(function () {
    utils.toast();
    utils.FormatNumber();
});