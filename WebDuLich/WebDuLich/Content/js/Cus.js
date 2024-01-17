function handlerConfirm(e) {
    var result = confirm("Bạn có chắc chắn muốn xóa không?")
    if (result) {
        location.href = e
    }
}


$(function () {
    // turn the element to select2 select style
    $('.select2').on('change', function () {
        var data = $(".select2 option:selected").val();
        switch (data) {
            case "1":
                $("#Code").val("OT-")
                break;
            case "2":
                $("#Code").val("Xm-")
                break;
            case "3":
                $("#Code").val("MB-")
                break;
            case "4":
                $("#Code").val("TH-")
                break;
            default:
                break;
        }
        
    })
});