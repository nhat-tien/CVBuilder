function showConfirm(msg, onOk, OnCancel) {
    Swal.fire({
        title: msg.title ?? "Bạn có chắc chắn?",
        text: msg.text ??  "Hành động này không thể khôi phục",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: msg.confirmText ?? "Đồng ý",
        cancelButtonText: msg.cancelText ?? "Hủy",
    }).then((result) => {
        if (result.isConfirmed) {
            onOk();
        } else if (
            result.dismiss === Swal.DismissReason.cancel
        ) {
            OnCancel();
        }
    });
}

function serviceCall(endpoint, onOk, opt) {
    const token = $('input[name="__RequestVerificationToken"]').val();
    fetch(endpoint,
    {
        method: opt.method ?? "POST",
        headers: {
            "RequestVerificationToken": token
        },
    }).then(e => {
        onOk(e)
    })
}
