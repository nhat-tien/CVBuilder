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

function showToast({ title, content, type }) {
    const toastContainer = document.getElementById("toast-container");
    const toastId = Date.now() + "-toast";

    const toastHtml = `
    <div id="${toastId}" class="toast" role="alert" aria-live="assertive" aria-atomic="true" data-bs-delay="3000">
        <div class="toast-header">
            <strong class="me-auto">${title}</strong>
            <small>11 mins ago</small>
            <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
        </div>
        <div class="toast-body">
            ${content}
        </div>
    </div>`;

    const parser = new DOMParser();
    const doc = parser.parseFromString(toastHtml, 'text/html');
    const newElement = doc.body.firstElementChild;

    toastContainer.appendChild(newElement);

    const toastEl = document.getElementById(toastId);

    const toast = new bootstrap.Toast(toastEl);
    toast.show();
}