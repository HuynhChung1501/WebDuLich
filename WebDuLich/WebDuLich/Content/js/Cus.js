function handlerConfirm(e) {
    var result = confirm("Bạn có chắc chắn muốn xóa không?")
    if (result) {
        location.href = e
    }
}
