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
            var data = {}
            var oj = $(this).closest(".formSubmit")
            var jsondata = {};
            var jsondata = utils.getSerialize(oj)

            console.log(jsondata);
            $.ajax({
                url: '/Admin/Tour/Timkiem',
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