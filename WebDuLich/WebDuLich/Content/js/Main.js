var main = {
    init: function () {
        main.onEvent();
        main.upEvent();
    },
    onEvent: function () {
        
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
    }
}

$(document).ready(function () {
    main.init();

    $('.select2').select2()
});