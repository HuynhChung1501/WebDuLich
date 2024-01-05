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

        //$(document).find(selector2).ready(function () {
            
        //});
    }

}

$(document).ready(function () {
    utils.toast();
});