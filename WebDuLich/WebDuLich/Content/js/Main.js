var main = {
    init: function () {
        main.onEvent();
        main.upEvent();
    },
    onEvent: function () {
        $(".menuLeft .nav-item .nav-link").click(function (e) {
            $(".menuLeft .nav-item").find(".nav-link").removeClass("active");
        });

        $(".onchangeActive").change(function (e) {
            //onchange checkbox bỏ tất cả lựa chọn và chỉ check 1 element
            e.preventDefault();

            var id = $(this).attr("data-id")
            var url = $(this).attr("data-url")
            var jsondata = {
                ID: id
            }

            $.ajax({
                url: url,
                type: 'post',
                data: jsondata,
                datatype: 'json',
                success: function (jsondata) {
                    
                },
            });

            var $box = $(this);
            if ($box.is(":checked")) {
                //để name checkbox cùng tên
                var group = "input:checkbox[name='" + $box.attr("name") + "']";
                $(group).prop("checked", false);
                $box.prop("checked", true);
            } else {
                $box.prop("checked", false);
            }
        });
    },
    upEvent: function () {
        $('.quickSubmit').click(function (e) {
            e.preventDefault();
            var oj = $(this).closest(".formSubmit")
            var url = oj.attr("action")
            var jsondata = {};
            var jsondata = utils.getSerialize(oj)

            $.ajax({
                url: url,
                type: 'post',
                data: jsondata,
                datatype: 'json',
                success: function (jsondata) {
                    $("#tableForm").html(jsondata);
                },
            });
        });


        $('.exportExcel').click(function (e) {
            e.preventDefault();
            var idItem = $(this).data('id')
            $.ajax({
                url: "/Admin/PhuongTien/ExportExcel",
                type: 'get',
                data: { id: idItem },
                success: function (resp) {
                    if (resp != "") {
                        location.href = "/Data/ExportExcel/" + resp
                    }
                },
            });

        })
    },

    afterLoad: function () {
        // Lấy danh sách các href từ các phần tử a trong ul li
        var hrefList = [];

        $('.menuLeft .nav-item').find('a').each(function () {
            $(this).removeClass("active")
            var href = $(this).attr('href');
            hrefList.push(href);
            hrefList.forEach(element => {
                if (window.location.pathname == href) {
                    $(this).addClass("active");
                }
            });
        });

    }
}

$(document).ready(function () {
    main.init();

    $('.select2').select2()
});


window.onload = function () {
    main.afterLoad();
};
