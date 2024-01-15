var main = {
    init: function () {
        main.onEvent();
        main.upEvent();
    },
    onEvent: function () {
        $(".menuLeft .nav-item .nav-link").click(function (e) {
            $(".menuLeft .nav-item").find(".nav-link").removeClass("active");
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
                    // alert(data.status)
                },
            });
        });
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
