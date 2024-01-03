var Utils = {
    Toast: function () {
        toastr.options = {
            "closeButton": true
        };
        
        $('.toastrDefaultSuccess').click(function () {
            toastr.success('Đăng ký thành công.')
        });
        $('.toastrDefaultError').click(function () {
            toastr.error('Lỗi')
        });
        $('.toastrDefaultInfo').click(function () {
            toastr.info('Lorem ipsum dolor sit amet, consetetur sadipscing elitr.')
        });
        $('.toastrDefaultWarning').click(function () {
            toastr.warning('Cảnh báo.')
        });

        $(document).find(selector2).ready(function () {
            
        });
    }
}

$(document).ready(function () {
    Utils.Toast();
});