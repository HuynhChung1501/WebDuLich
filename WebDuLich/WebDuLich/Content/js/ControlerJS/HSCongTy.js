var HSCongTy = {
    init: function() {
        HSCongTy.quickSubmit();
    },

    quickSubmit: function () {
        $(function () {
            $.validator.setDefaults({
                submitHandler: function () {
                    alert("Đăng ký thành công!");
                }
            });

            
        });
        $('.quickSubmit').click(function (e) {
            e.preventDefault();
            e.stopImmediatePropagation();
            $('#frmHSCongTy').validate({
                rules: {
                    Ten: {
                        required: true,
                        minlength: 6
                    },
                    Maso: {
                        required: true,
                        minlength: 4
                    },
                },
                messages: {
                    Ten: {
                        required: "Tên công ty không được để trống",
                        minlength: "Ký tự tối thiếu là 6"
                    },
                    Maso: {
                        required: "Mã số không được để trống",
                        minlength: "Ký tự tối thiếu là 4"
                    },
                },
                errorElement: 'span',
                errorPlacement: function (error, element) {
                    error.addClass('invalid-feedback');
                    element.closest('.form-group').append(error);
                },
                highlight: function (element, errorClass, validClass) {
                    $(element).addClass('is-invalid');
                },
                unhighlight: function (element, errorClass, validClass) {
                    $(element).removeClass('is-invalid');
                }
            });
            var oj = $(this).closest(".formSubmit")
            var url = oj.attr("action")
            var jsondata = {};
            var jsondata = utils.getSerialize(oj)
            if( $("#frmHSCongTy").valid()) {
                $.ajax({
                    url: url,
                    type: 'post',
                    data: jsondata,
                    datatype: 'json',
                    success: function (jsondata) {
                        window.location.href= jsondata.data
                    },
                });
            }
        });

    }
}